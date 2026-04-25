<template>
  <div>
    <iframe
      :src="playLink"
      title="OpenRhynn"
      frameborder="0"
      width="100%"
      height="500"
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
      class="fixed top-0 left-0 h-screen w-screen z-100"
    ></iframe>
    <div class="fixed right-0 top-0 z-101">
      <UButton class="m-2" variant="link" icon="i-lucide-x" @click="emit('close')"> </UButton>
    </div>
  </div>
</template>

<script lang="ts" setup>
const emit = defineEmits(["close"]);

const config = useRuntimeConfig();
const playLink = computed(() => {
  return config.public.app.playBase;
});
</script>

<style></style>
