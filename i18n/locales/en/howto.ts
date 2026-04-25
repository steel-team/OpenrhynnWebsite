export default async (_locale: any) => {
  return {
    SEO: {
      Title: "How to Play OpenRhynn. Beginner's Guide & Controls",
      Description:
        "A simple guide to OpenRhynn (the j2me classic Rhynn). Controls, mechanics, and tips — everything you need to start playing right away.",
    },
    H1: "How to Play OpenRhynn",
    H1Description:
      "A simple beginner's guide. Here you will learn the basic controls, key mechanics, and helpful tips for OpenRhynn - the fan-made reconstruction of the j2me game Rhynn.",
    Guides: {
      Guide1: {
        Title: "Where to start? PC",
        Text: "If you have a PC (Windows/Linux/MacOS) - we recommend installing Java and using the standard PC client. The control scheme differs from J2ME versions (including touch). The main changes are the availability of keyboard input; the left and right soft keys are mapped to the F1 and F2 keys. Basic controls are performed using the arrow keys and the Enter key.",
      },
      Guide2: {
        Title: "Where to start? Android (and browser)",
        Text: "There is a native Android client for OpenRhynn. However, it was built a long time ago and likely will not run on modern devices. We recommend using an emulator and the J2ME (Touch) client. This client features simplified text input: select a text field and press Enter (for the browser) or the center of the screen (for Android).",
      },
      Guide3: {
        Title: "Control scheme (touch screens)",
        PreText:
          "The web client and TOUCH versions use a specific control scheme. The markers in the screenshot indicate the button mapping:",
        Arrows: {
          Top: "up arrow",
          Down: "down arrow",
          Left: "left arrow",
          Right: "right arrow",
          Enter: "enter",
        },
        PostText1:
          "The two bottom buttons on the left and right are responsible for the soft keys (same as F1 and F2 on the PC version of the client).",
        PostText2:
          "After selecting an input field, press FIRE (Enter) to open the text input menu (this is necessary when logging in/registering, as well as for chat). In the web version, only the Latin alphabet works for input. For other versions, it depends on the emulator.",
      },
    },
  };
};
