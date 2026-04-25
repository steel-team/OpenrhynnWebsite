import Core from "./en/core";
import Home from "./en/home";
import Server from "./en/server";
import Downloads from "./en/downloads";
import HowTo from "./en/howto";

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
