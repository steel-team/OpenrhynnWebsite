export default async (_locale: any) => {
  return {
    SEO: {
      Title: "Скачать OpenRhynn. Клиент для Windows, Linux, MacOS, Android (j2me)",
      Description:
        "Скачать игровой клиент OpenRhynn — фанатскую open-source версию классической j2me игры Rhynn. Доступно для ПК, Android и эмуляторов.",
    },
    H1: "Скачать OpenRhynn",
    H1Description:
      "Выберите версию клиента для своей платформы. OpenRhynn — это открытая реализация классической j2me игры Rhynn, доступная для Windows, Linux, Android и запуска в эмуляторах.",
    Actions: {
      Download: "Скачать {file}",
      DownloadAll: "Скачать {file} (любое устройство)",
      DownloadNokia: "Скачать {file} (Nokia)",
      DownloadTouch: "Скачать {file} (для сенсорных экранов)",
      DownloadAndroid: "Скачать {file} (Android legacy)",
    },
    GuideAlt: "Скриншот гайд для сенсорных устройств",
    Notices: {
      JavaRequired: "* Java необходима для запуска игры",
      Emulators:
        "* Для игры на современных устройствах потребуется эмулятор, для ПК рекомендуем KEmulator",
      TouchScreens:
        '* Управление достаточно специфичное, смотрите скришот ниже для примера. Так же рекомендуем прочитать гайд в разделе "Как играть". Android версия устарела, вероятнее не запустится нативно на новых устройствах, используйте эмуляторы.',
    },
  };
};
