<template>
  <UHeader>
    <template #left>
      <NuxtLink
        :to="localePath('/')"
        class="flex items-center gap-2 bold-font font-600 text-lg logo"
      >
        <img src="/icon.png" />
        OpenRhynn
      </NuxtLink>
    </template>

    <UNavigationMenu :items="items" />

    <template #body>
      <UNavigationMenu
        :items="items"
        orientation="vertical"
        class="-mx-1"
        :ui="{
          link: 'p-3',
        }"
      />
      <div class="md:hidden">
        <USeparator class="mt-1 mb-2" />
        <div class="flex items-center gap-2">
          <ULocaleSelect
            :model-value="ilocale"
            :locales="knownLocales"
            class="w-32"
            @update:model-value="forceSetLocale"
          />
        </div>
      </div>
    </template>

    <template #right>
      <ULocaleSelect
        :model-value="ilocale"
        :locales="knownLocales"
        class="w-32 hidden md:inline-flex"
        @update:model-value="forceSetLocale"
      />
    </template>
  </UHeader>
</template>

<script lang="ts" setup>
import type { NavigationMenuItem } from "@nuxt/ui";
import * as nuxtLocales from "@nuxt/ui/locale";

const { locale, setLocale, locales, t } = useI18n();
const localePath = useLocalePath();
const switchLocalePath = useSwitchLocalePath();
const route = useRoute();
const url = useRequestURL();
const config = useRuntimeConfig();

const recreateLocale = (inp: any) => {
  if (inp == "zh") return "zh-CN";
  return inp;
};

const ilocale = ref(recreateLocale(locale.value));

const knownLocales = computed(() => {
  const vals = Object.values(nuxtLocales);
  const lval = locales.value.map((a) => a.name);
  const res = vals.filter((a) => lval.includes(a.code));
  return res;
});

const forceSetLocale = async (evt: any) => {
  const loc = evt as any;
  const newLocale = loc.split("-")[0];
  ilocale.value = recreateLocale(newLocale);
  await setLocale(newLocale);
  const path = switchLocalePath(newLocale);
  const localeObj = locales.value.find((a) => a.code == newLocale);
  if (localeObj?.domainDefault ?? false) {
    const target = url.protocol + "//" + localeObj?.domain + path;
    navigateTo(target, {
      external: true,
      redirectCode: 301,
    });
  }
};

const items = computed<NavigationMenuItem[]>(() => [
  {
    label: t("Core.Nav.Main"),
    to: localePath("/"),
    active: (route.meta.isNavHome ?? false) as boolean,
  },
  {
    label: t("Core.Nav.Downloads"),
    to: localePath("/downloads"),
    active: (route.meta.isNavDownloads ?? false) as boolean,
  },
  {
    label: t("Core.Nav.HowTo"),
    to: localePath("/howto"),
    active: (route.meta.isNavHowTo ?? false) as boolean,
  },
  {
    label: t("Core.Nav.SourceCode"),
    to: config.public.app.links.github,
    target: "_blank",
  },
  {
    label: t("Core.Nav.Banner"),
    to: config.public.app.links.elion.header,
    target: "_blank",
    class: "highlighted-link",
  },
]);
</script>

<style></style>
