<template>
  <div></div>
</template>

<script lang="ts" setup>
const { t } = useI18n();
const localePath = useLocalePath();
const config = useRuntimeConfig();

definePageMeta({
  isNavDownloads: true,
});

useSeoMeta({
  title: t("Downloads.SEO.Title"),
  ogTitle: t("Downloads.SEO.Title"),
  description: t("Downloads.SEO.Description"),
  ogDescription: t("Downloads.SEO.Description"),
});
</script>

<style></style>
