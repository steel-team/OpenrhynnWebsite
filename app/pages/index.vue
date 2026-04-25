<template>
  <div>
    <HomePlay v-if="playActive" @close="playActive = false"></HomePlay>
    <UPageHero
      :title="t('Home.H1')"
      :description="t('Home.H1Description')"
      :links="[
        {
          label: t('Home.Actions.Download'),
          to: localePath('/downloads'),
          icon: 'i-lucide-cloud-download',
          size: 'xl',
        },
        {
          label: t('Home.Actions.Play'),
          onClick: () => {
            playActive = true;
          },
          icon: 'i-lucide-play',
          size: 'xl',
          color: 'neutral',
          variant: 'subtle',
        },
        {
          label: 'Discord',
          to: config.public.app.links.discord,
          target: '_blank',
          icon: 'i-simple-icons-discord',
          size: 'xl',
          color: 'neutral',
          variant: 'subtle',
        },
      ]"
    >
      <template #headline>
        <div class="flex justify-center mt-10">
          <img src="/images/logo_cut.png" alt="OpenRhynn" />
        </div>
      </template>
    </UPageHero>

    <UPageSection :title="t('Home.ServerStatus')">
      <template #description>
        <HomeServerStatus />
      </template>
    </UPageSection>

    <UContainer>
      <div class="grid gap-2 grid-cols-2 sm:grid-cols-4">
        <img
          v-for="i in 4"
          :key="'scrn-or-' + i"
          :src="`/images/screenshots/s${i}.gif`"
          width="634"
          height="210"
          :alt="t('Home.Screenshot')"
          class="w-full rounded-lg"
          loading="lazy"
        />
      </div>
    </UContainer>

    <UPageSection
      id="support"
      :title="t('Home.Elion.Title')"
      :description="t('Home.Elion.Description')"
      :features="[
        {
          icon: 'i-lucide-wand-sparkles',
          title: t('Home.Elion.Feature1.Title'),
          description: t('Home.Elion.Feature1.Description'),
        },
        {
          icon: 'i-lucide-earth',
          title: t('Home.Elion.Feature2.Title'),
          description: t('Home.Elion.Feature2.Description'),
        },
        {
          icon: 'i-lucide-castle',
          title: t('Home.Elion.Feature3.Title'),
          description: t('Home.Elion.Feature3.Description'),
        },
      ]"
    />

    <UPageSection>
      <UPageCTA
        :title="t('Home.Elion.CTA.Title')"
        :description="t('Home.Elion.CTA.Description')"
        variant="subtle"
        :links="[
          {
            label: t('Home.Elion.CTA.PlayAction'),
            to: config.public.app.links.elion.cta,
            target: '_blank',
            trailingIcon: 'i-lucide-arrow-right',
            color: 'neutral',
          },
        ]"
      >
        <div class="flex justify-center">
          <img
            src="/images/elion/elion_logo.gif"
            width="634"
            height="210"
            :alt="t('Home.Elion.CTA.ImageAlt')"
            class="w-full rounded-lg max-w-[450px]"
            loading="lazy"
          />
        </div>
        <div class="grid gap-2 grid-cols-2 sm:grid-cols-4">
          <img
            v-for="i in 4"
            :key="'scrn-' + i"
            :src="`/images/elion/elion_${i}.png`"
            width="634"
            height="210"
            :alt="t('Home.Elion.Screenshot')"
            class="w-full rounded-lg"
            loading="lazy"
          />
        </div>
      </UPageCTA>
    </UPageSection>
  </div>
</template>

<script setup lang="ts">
const { t } = useI18n();
const localePath = useLocalePath();
const config = useRuntimeConfig();

const playActive = ref(false);

definePageMeta({
  isNavHome: true,
});

useSeoMeta({
  title: t("Home.SEO.Title"),
  ogTitle: t("Home.SEO.Title"),
  description: t("Home.SEO.Description"),
  ogDescription: t("Home.SEO.Description"),
});
</script>
