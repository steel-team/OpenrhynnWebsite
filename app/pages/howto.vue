<template>
  <div></div>
</template>

<script lang="ts" setup>
const { t } = useI18n();
const localePath = useLocalePath();
const config = useRuntimeConfig();

definePageMeta({
  isNavHowTo: true,
});

useSeoMeta({
  title: t("HowTo.SEO.Title"),
  ogTitle: t("HowTo.SEO.Title"),
  description: t("HowTo.SEO.Description"),
  ogDescription: t("HowTo.SEO.Description"),
});
</script>

<style></style>
