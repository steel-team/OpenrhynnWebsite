export default async (_locale: any) => {
  return {
    SEO: {
      Title: "Download OpenRhynn. Client for Windows, Linux, Android (j2me)",
      Description:
        "Download the OpenRhynn game client — a fan-made open-source version of the classic j2me game Rhynn. Available for PC, Android, and emulators.",
    },
    H1: "Download OpenRhynn",
    H1Description:
      "Choose the client version for your platform. OpenRhynn is an open-source implementation of the classic j2me game Rhynn, available for Windows, Linux, Android, and emulators.",
    Actions: {
      Download: "Download {file}",
      DownloadAll: "Download {file} (any device)",
      DownloadNokia: "Download {file} (Nokia)",
      DownloadTouch: "Download {file} (for touch screens)",
      DownloadAndroid: "Download {file} (Android legacy)",
    },
    Notices: {
      JavaRequired: "* Java is required to run game",
      Emulators:
        "* To play on modern devices, you will need an emulator. For PC, we recommend KEmulator.",
      TouchScreens:
        '*The controls are quite specific. See the screenshot below for an example. We also recommend reading the guide in the "How to Play" section. Please note, native android version is outdated and might not work on modern devices, consider using emulators.',
    },
  };
};
