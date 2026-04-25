<template>
  <div>
    <iframe
      :src="playLink"
      title="OpenRhynn"
      frameborder="0"
      allow="
        accelerometer;
        autoplay;
        clipboard-write;
        encrypted-media;
        gyroscope;
        picture-in-picture;
        web-share;
      "
      allowfullscreen
      class="fixed top-0 left-0 z-100 h-svh w-svw"
    ></iframe>
    <div class="fixed right-0 top-0 z-101">
      <UButton class="m-2" variant="link" icon="i-lucide-x" @click="emit('close')"> </UButton>
    </div>
  </div>
</template>

<script lang="ts" setup>
// h-lvh w-lvw (we can't use l and d because of bottom url input on safari)
const emit = defineEmits(["close"]);

const config = useRuntimeConfig();
const playLink = computed(() => {
  return config.public.app.playBase;
});
</script>

<style></style>
