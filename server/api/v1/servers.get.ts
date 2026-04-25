export default defineCachedEventHandler(
  async (event) => {
    try {
      const config = useRuntimeConfig(event);
      const externalData = await $fetch(`http://${config.public.app.apiBase}:8181/api/v1/servers`);

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
    name: "serversCache",
    maxAge: 15,
  },
);
