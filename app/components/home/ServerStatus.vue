<template>
  <div>
    <UTabs :items="items" color="neutral">
      <template #trailing="{ item }">
        <div class="flex items-center justify-center gap-2">
          <div class="dot mt-[2px]" :class="item.online ? 'bg-green-500' : 'bg-red-500'"></div>
          <span v-if="item.online">{{ item.count }}</span>
        </div>
      </template>
      <template #content="{ item }">
        <ServerInfo :server="item.serverInfo" />
      </template>
    </UTabs>
  </div>
</template>

<script lang="ts" setup>
import ServerInfo from "~/components/home/ServerInfo.vue";

const { data: serversOnlineStatus } = await useFetch("/api/v1/composedstatus");

const items = computed(() => {
  if (!isMounted.value && import.meta.server) {
    return [];
  }

  const result = [];

  for (const srv of serversOnlineStatus.value?.masterServer?.servers ?? []) {
    if (!srv.public) continue;
    const onlineEntry = serversOnlineStatus.value?.online[srv.address ?? "unknown"];
    const res = {
      label: srv.name,
      icon: "i-lucide-server",
      online: true,
      count: 0,
      serverInfo: onlineEntry,
    };

    if (onlineEntry === undefined) {
      res.online = false;
      res.icon = "i-lucide-server-off";
    } else {
      res.online = true;
      res.count = onlineEntry.online ?? 0;
    }

    result.push(res);
  }

  return result;
});

const isMounted = ref(false);

onMounted(() => {
  isMounted.value = true;
});
</script>

<style></style>
