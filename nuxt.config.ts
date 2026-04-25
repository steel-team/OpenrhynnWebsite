// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  modules: [
    "@nuxt/eslint",
    "@nuxt/ui",
    "@nuxtjs/i18n",
    "@nuxtjs/seo",
    "nuxt-security",
    "@nuxt/scripts",
  ],

  devtools: {
    enabled: true,

    timeline: {
      enabled: true,
    },
  },

  app: {
    head: {
      titleTemplate: "%s - %siteName", //  %separator %siteName
    },
  },

  css: ["~/assets/css/main.css"],

  site: {
    // url and name taken from i18n module
  },

  colorMode: {
    preference: "dark", // or 'light', 'system'
  },

  runtimeConfig: {
    public: {
      app: {
        apiBase: "server2.openrhynn.net",
        playBase: "http://192.168.1.20:46575/run?app=OpenRhynn",
        links: {
          github: "https://github.com/steel-team/OpenrhynnJavaServer",
          discord: "https://discord.gg/r8sqNfNh3B",
          elion: {
            header:
              "https://elion.world/?utm_source=openrhynn&utm_medium=cta_button&utm_campaign=cross&utm_content=header",
            cta: "https://elion.world/?utm_source=openrhynn&utm_medium=cta_button&utm_campaign=cross&utm_content=cta",
          },
        },
      },
      i18n: {
        baseUrl: process.env.NUXT_I18N_BASE_URL || "http://127.0.0.1:3000",
      },
    },
  },

  /* routeRules: {
    "/": { prerender: true },
  }, */

  future: {
    compatibilityVersion: 5,
  },
  experimental: {
    payloadExtraction: false,
    renderJsonPayloads: false, // fix for fucking devalue don't work with Error/FetchError types!
    nitroAutoImports: true, // fix for nuxt/ui etc auto imports shit: https://github.com/nuxt/nuxt/issues/34142
    defaults: {
      nuxtLink: {
        prefetchOn: {
          visibility: false,
          interaction: true,
        },
      },
    },
  },

  compatibilityDate: "2025-01-15",

  eslint: {
    config: {
      stylistic: {
        commaDangle: "never",
        braceStyle: "1tbs",
      },
    },
  },

  i18n: {
    defaultLocale: "en",
    strategy: "prefix",
    /* experimental: {
      alternateLinkCanonicalQueries: true,
    }, */
    detectBrowserLanguage: {
      useCookie: true,
      cookieKey: "i18n_redirected",
      redirectOn: "root", // recommended
    },
    locales: [
      { code: "en", name: "en", file: "en.ts", language: "en" },
      { code: "ru", name: "ru", file: "ru.ts", language: "ru" },
    ],
  },

  ogImage: {
    zeroRuntime: true,
  },

  scripts: {
    registry: {
      googleTagManager: {
        id: "GTM-KM8FVM48",
        trigger: "onNuxtReady",
      },
    },
  },

  security: {
    hidePoweredBy: true,
    corsHandler: false,
    headers: {
      contentSecurityPolicy: false,
      xXSSProtection: "1; mode=block",
      crossOriginOpenerPolicy: false,
      crossOriginEmbedderPolicy: false,
      crossOriginResourcePolicy: false,
      referrerPolicy: "strict-origin-when-cross-origin",
    },
    nonce: false,
    removeLoggers:
      process.env.NODE_ENV !== "development"
        ? {
            consoleType: ["error", "warn", "debug"],
          }
        : false,
    ssg: {
      meta: true,
      hashScripts: false,
      hashStyles: false,
      nitroHeaders: true,
      exportToPresets: true,
    },
    sri: false,
  },
});
