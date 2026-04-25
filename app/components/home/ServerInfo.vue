<template>
  <div v-if="server == null || server.online == 0">
    <div class="text-center text-xl">
      {{ t("Server.Empty") }}
    </div>
  </div>
  <div
    v-else
    class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-2 max-h-102 p-4 overflow-y-auto"
  >
    <UPageCard
      v-for="chr in characters"
      :key="chr.id ?? 0"
      :ui="{ description: 'w-full', body: 'w-full' }"
    >
      <template #title>
        <div class="flex justify-between">
          <div class="flex items-end gap-2">
            <img
              :src="`/images/classes/${chr.charClass}.png`"
              :alt="t('Server.Classes.Class_' + chr.charClass)"
              class="rounded-sm"
            />
            <span :class="chr.userType == 'GM' ? 'text-orange-500' : ''">{{ chr.name }}</span>
          </div>
          <div>{{ chr.level }} {{ t("Server.LevelShort") }}</div>
        </div>
      </template>
      <template #description>
        <div class="flex flex-col gap-1 mt-2">
          <div class="bg-gray-800 w-full h-1 rounded-sm">
            <div
              class="bg-red-500 h-full rounded-sm"
              :style="{
                width: `${Math.round(((chr.healthCurrent ?? 0) / (chr.healthMax ?? 0)) * 100)}%`,
              }"
            ></div>
          </div>
          <div class="bg-gray-800 w-full h-1 rounded-sm">
            <div
              class="bg-blue-400 h-full rounded-sm"
              :style="{
                width: `${Math.round(((chr.manaCurrent ?? 0) / (chr.manaMax ?? 0)) * 100)}%`,
              }"
            ></div>
          </div>
        </div>
        <div class="flex gap-2 items-end mt-2">
          <UIcon name="i-lucide-map" class="size-5" />
          <span>{{ chr.playfieldName }}</span>
        </div>
      </template>
    </UPageCard>
  </div>
</template>

<script lang="ts" setup>
import type { OnlineResponse } from "~/models/api/OnlineResponse";

const props = defineProps<{ server?: OnlineResponse }>();

const { t } = useI18n();

const characters = computed(() => {
  return props.server?.characters ?? [];
});

// for test
/*
const charactersFake = computed(() => {
  const chrs = props.server?.characters ?? [];
  const xchr = chrs[0];
  const res = [];
  for (let i = 0; i < 100; i++) {
    res.push(xchr);
  }
  return res;
}); */
</script>

<style></style>
