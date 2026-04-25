import type { ServersResponse } from "@/models/api/ServersResponse";

export default defineCachedEventHandler(
  async (event) => {
    try {
      const config = useRuntimeConfig(event);
      const targetServer = encodeURI(event.context.params?.server ?? config.public.app.apiBase);

      const knownServers = await $fetch<ServersResponse>(
        `http://${config.public.app.apiBase}:8181/api/v1/servers`,
      );

      if (knownServers?.servers?.find((a) => a.address == targetServer) == null) {
        throw new Error("server not found");
      }

      const externalData = await $fetch(`http://${targetServer}:8181/api/v1/online`);

      return externalData;
    } catch (error: any) {
      throw createError({
        statusCode: error?.response?.status || 500,
        statusMessage: "failed to fetch data",
        data: error?.data,
      });
    }
  },
  {
    name: "serverOnlineCache",
    maxAge: 15,
  },
);
