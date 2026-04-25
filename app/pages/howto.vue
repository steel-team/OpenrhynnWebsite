<template>
  <UContainer>
    <UPage>
      <UPageHeader :title="t('HowTo.H1')" :description="t('HowTo.H1Description')" />
      <div class="mt-6">
        <h2 class="text-3xl sm:text-4xl text-pretty tracking-tight font-bold text-highlighted">
          {{ t("HowTo.Guides.Guide1.Title") }}
        </h2>
        <div class="text-base sm:text-lg text-balance text-toned mt-6">
          {{ t("HowTo.Guides.Guide1.Text") }}
        </div>
      </div>
      <div class="mt-6">
        <h2 class="text-3xl sm:text-4xl text-pretty tracking-tight font-bold text-highlighted">
          {{ t("HowTo.Guides.Guide2.Title") }}
        </h2>
        <div class="text-base sm:text-lg text-balance text-toned mt-6">
          {{ t("HowTo.Guides.Guide2.Text") }}
        </div>
      </div>
      <div class="mt-6">
        <h2 class="text-3xl sm:text-4xl text-pretty tracking-tight font-bold text-highlighted">
          {{ t("HowTo.Guides.Guide3.Title") }}
        </h2>
        <div class="text-base sm:text-lg text-balance text-toned mt-6">
          <div>
            {{ t("HowTo.Guides.Guide3.PreText") }}
          </div>
          <div>
            - <span class="text-orange-500">UP</span> - {{ t("HowTo.Guides.Guide3.Arrows.Top") }}
          </div>
          <div>
            - <span class="text-orange-500">DOWN</span> - {{ t("HowTo.Guides.Guide3.Arrows.Down") }}
          </div>
          <div>
            - <span class="text-orange-500">LEFT</span> - {{ t("HowTo.Guides.Guide3.Arrows.Left") }}
          </div>
          <div>
            - <span class="text-orange-500">RIGHT</span> -
            {{ t("HowTo.Guides.Guide3.Arrows.Right") }}
          </div>
          <div>
            - <span class="text-orange-500">FIRE</span> -
            {{ t("HowTo.Guides.Guide3.Arrows.Enter") }}
          </div>
          <div>
            {{ t("HowTo.Guides.Guide3.PostText1") }}
          </div>
          <div>
            {{ t("HowTo.Guides.Guide3.PostText2") }}
          </div>
          <div class="my-4">
            <img src="/images/guides/screen_guide.png" :alt="t('Downloads.GuideAlt')" />
          </div>
        </div>
      </div>
    </UPage>
  </UContainer>
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
