<template>
  <USeparator
    :avatar="{
      src: '/icon.png',
      loading: 'lazy',
    }"
  />
  <UFooter>
    <template #left>
      <p class="text-sm text-muted">
        {{ t("Core.Copyright", { date: new Date().getFullYear() }) }}
      </p>
    </template>

    <template #right>
      <UButton
        :to="config.public.app.links.discord"
        target="_blank"
        icon="i-simple-icons-discord"
        aria-label="Discord"
        color="neutral"
        variant="ghost"
      />
      <UButton
        :to="config.public.app.links.github"
        target="_blank"
        icon="i-simple-icons-github"
        aria-label="GitHub"
        color="neutral"
        variant="ghost"
      />
    </template>
  </UFooter>
</template>

<script lang="ts" setup>
const { t } = useI18n();
const config = useRuntimeConfig();
</script>

<style></style>
