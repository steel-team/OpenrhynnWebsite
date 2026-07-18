<script lang="ts" setup>
import * as locales from "@nuxt/ui/locale";
import type { NuxtError } from "#app";

const props = defineProps({
  error: Object as () => NuxtError,
});
const { t, locale } = useI18n();

const handleError = () => clearError({ redirect: "/" });

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
  <NuxtLoadingIndicator :throttle="0" :height="2" :color="false" class="bg-orange-500" />
  <NuxtAnnouncer />
  <NuxtRouteAnnouncer />
  <UApp :locale="currentLocale">
    <SharedHeader />

    <UMain>
      <UContainer class="mb-32 mt-16 relative z-10">
        <div>
          <h1
            class="text-3xl sm:text-5xl text-pretty tracking-tight font-bold text-highlighted text-center w-full"
          >
            {{ t("Core.Errors.Title", { code: error?.status }) }}
          </h1>
          <div class="text-pretty text-center flex flex-col items-center mt-6 gap-6">
            <span>{{ t(`Core.Errors.Description${error?.status}`) }}</span>
            <UButton @click="handleError">
              {{ t("Core.Actions.Back") }}
            </UButton>
          </div>
        </div>
      </UContainer>
    </UMain>

    <SharedFooter />
  </UApp>
</template>
