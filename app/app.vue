<script lang="ts" setup>
import * as locales from "@nuxt/ui/locale";

const { t, locale } = useI18n();
const config = useRuntimeConfig();

if (import.meta.client) {
  useScriptGoogleTagManager({
    id: config.public.app.analytics.gtag,
  });
}

const currentLocale = computed(() =>
  Object.values(locales).find((item) => item.code === locale.value),
);
const i18nHead = useLocaleHead({
  dir: true,
  seo: true,
  lang: true,
});

// @ts-expect-error t123adsf asdf
useHead(() => ({
  htmlAttrs: {
    lang: i18nHead.value.htmlAttrs.lang,
    dir: i18nHead.value.htmlAttrs.dir,
  },
  link: [{ rel: "icon", href: "/favicon.ico" }, ...(i18nHead.value.link || [])],
  meta: [
    { name: "viewport", content: "width=device-width, initial-scale=1" },
    ...(i18nHead.value.meta || []),
  ],
}));

useHead({
  meta: [{ name: "viewport", content: "width=device-width, initial-scale=1" }],
  link: [{ rel: "icon", href: "/favicon.ico" }],
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
  <NuxtAnnouncer />
  <NuxtRouteAnnouncer />
  <UApp :locale="currentLocale">
    <SharedHeader />

    <UMain>
      <NuxtPage />
    </UMain>

    <SharedFooter />
  </UApp>
</template>
