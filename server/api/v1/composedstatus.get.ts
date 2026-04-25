import type { ServersResponse } from "@/models/api/ServersResponse";
import type { OnlineResponse } from "@/models/api/OnlineResponse";

export default defineCachedEventHandler(
  async (event) => {
    try {
      const config = useRuntimeConfig(event);
      const serversData = await $fetch<ServersResponse>(
        `http://${config.public.app.apiBase}:8181/api/v1/servers`,
      );

      const onlineData: Record<string, OnlineResponse> = {};

      for (const server of serversData.servers ?? []) {
        // fetch online
        if (!server.public) continue;
        try {
          const data = await $fetch<OnlineResponse>(`http://${server.address}:8181/api/v1/online`);
          onlineData[server.address ?? "unknown"] = data;
        } catch {}
      }

      return {
        masterServer: serversData,
        online: onlineData,
      };
    } catch (error: any) {
      throw createError({
        statusCode: error?.response?.status || 500,
        statusMessage: "failed to fetch data",
        data: error?.data,
      });
    }
  },
  {
    name: "composedStatusCache",
    maxAge: 15,
  },
);
