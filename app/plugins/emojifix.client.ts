import { polyfillCountryFlagEmojis } from "country-flag-emoji-polyfill";

export default defineNuxtPlugin((_nuxtApp) => {
  polyfillCountryFlagEmojis();
});
