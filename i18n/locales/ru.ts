import Core from "./ru/core";
import Home from "./ru/home";
import Server from "./ru/server";
import Downloads from "./ru/downloads";
import HowTo from "./ru/howto";

export default defineI18nLocale(async (locale) => {
  return {
    nuxtSiteConfig: {
      name: "OpenRhynn",
      description: "",
    },
    Core: await Core(locale),
    Home: await Home(locale),
    Server: await Server(locale),
    Downloads: await Downloads(locale),
    HowTo: await HowTo(locale),
  };
});
