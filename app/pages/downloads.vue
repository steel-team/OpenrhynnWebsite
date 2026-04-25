<template>
  <UContainer>
    <UPage>
      <UPageHeader :title="t('Downloads.H1')" :description="t('Downloads.H1Description')" />

      <UPageBody>
        <UTabs :items="items">
          <template #pc>
            <div class="mb-4 flex flex-col gap-2">
              <UButton
                variant="link"
                class="underline underline-offset-8 text-white"
                to="/downloads/OpenRhynn.jar"
                target="blank"
              >
                {{ t("Downloads.Actions.Download", { file: "OpenRhynn.jar" }) }}
              </UButton>
            </div>
            <span class="text-red-400">{{ t("Downloads.Notices.JavaRequired") }}</span>
          </template>
          <template #j2me>
            <div class="mb-4 flex flex-col gap-2">
              <div class="flex gap-1 items-center">
                <UButton
                  variant="link"
                  class="underline underline-offset-8 text-white"
                  to="/downloads/OpenRhynn_ALL.jar"
                  target="blank"
                >
                  {{ t("Downloads.Actions.DownloadAll", { file: "OpenRhynn_ALL.jar" }) }}
                </UButton>
                |
                <UButton
                  variant="link"
                  class="underline underline-offset-8 text-white"
                  to="/downloads/OpenRhynn_ALL.jad"
                  target="blank"
                >
                  .jad
                </UButton>
              </div>
              <div class="flex gap-1 items-center">
                <UButton
                  variant="link"
                  class="underline underline-offset-8 text-white"
                  to="/downloads/OpenRhynn_NK.jar"
                  target="blank"
                >
                  {{ t("Downloads.Actions.DownloadNokia", { file: "OpenRhynn_NK.jar" }) }}
                </UButton>
                |
                <UButton
                  variant="link"
                  class="underline underline-offset-8 text-white"
                  to="/downloads/OpenRhynn_NK.jad"
                  target="blank"
                >
                  .jad
                </UButton>
              </div>
            </div>
            <span class="text-orange-500">{{ t("Downloads.Notices.Emulators") }}</span>
          </template>
          <template #j2metouch>
            <div class="mb-4 flex flex-col gap-2">
              <div class="flex gap-1 items-center">
                <UButton
                  variant="link"
                  class="underline underline-offset-8 text-white"
                  to="/downloads/OpenRhynn_TOUCH.jar"
                  target="blank"
                >
                  {{ t("Downloads.Actions.DownloadTouch", { file: "OpenRhynn_TOUCH.jar" }) }}
                </UButton>
                |
                <UButton
                  variant="link"
                  class="underline underline-offset-8 text-white"
                  to="/downloads/OpenRhynn_TOUCH.jad"
                  target="blank"
                >
                  .jad
                </UButton>
              </div>
              <div class="mb-4 flex flex-col gap-2">
                <div class="flex gap-1 items-center">
                  <UButton
                    variant="link"
                    class="underline underline-offset-8 text-white"
                    to="/downloads/legacy/OpenRhynn-signed.apk"
                    target="blank"
                  >
                    {{ t("Downloads.Actions.DownloadAndroid", { file: "OpenRhynn-signed.apk" }) }}
                  </UButton>
                </div>
              </div>
              <span class="text-orange-500">{{ t("Downloads.Notices.TouchScreens") }}</span>
              <div>
                <img src="/images/guides/screen_guide.png" :alt="t('Downloads.GuideAlt')" />
              </div>
            </div>
          </template>
        </UTabs>
      </UPageBody>
    </UPage>
  </UContainer>
</template>

<script lang="ts" setup>
const { t } = useI18n();

const items = [
  {
    label: "PC (Windows / Linux / MacOS)",
    icon: "i-lucide-computer",
    slot: "pc",
  },
  {
    label: "J2ME - Multi + Nokia",
    icon: "i-simple-icons-openjdk",
    slot: "j2me",
  },
  {
    label: "J2ME Touch (Web / Android)",
    icon: "i-simple-icons-android",
    slot: "j2metouch",
  },
];

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
