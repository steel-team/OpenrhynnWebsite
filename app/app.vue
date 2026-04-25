<script setup>
const { t } = useI18n();

useHead({
  meta: [{ name: "viewport", content: "width=device-width, initial-scale=1" }],
  link: [{ rel: "icon", href: "/favicon.ico" }],
  htmlAttrs: {
    lang: "en",
  },
});

useSeoMeta({
  title: t("Home.SEO.Title"),
  ogTitle: t("Home.SEO.Title"),
  description: t("Home.SEO.Description"),
  ogDescription: t("Home.SEO.Description"),
  ogImage: "/images/logo.png",
});
</script>

<template>
  <UApp>
    <SharedHeader />

    <UMain>
      <NuxtPage />
    </UMain>

    <SharedFooter />
  </UApp>
</template>
