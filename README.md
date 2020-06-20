# Bifler
A Complete C# Project to capture Basler camera images from router encoder.

# Installation

### Main App
To run Bifler, you first need to install [pylon Software Suite](https://www.baslerweb.com/en/sales-support/downloads/software-downloads/)
There are no additional drivers needed for TriggerDevice and/or radio to work with the main desktop software, as the lastest version of Bifler works completely with TCP connections.
However, if you want to work with the old TriggerDevice, you need a working [Arduino](https://www.arduino.cc/) device with Serial/USB drivers installed on your computer.

Finally, Compile the main desktop app using `.NET Framework 4.5` using `x86_64` architecture and include `libBifler`, `Bifler` and `Pylon.NETSupportLibrary`. You may upgrade `Pylon.NETSupportLibrary` to the latest version provided by the official software suite, but that may need a lot of changes to work with the existing project version. Every configuration you need can be done through the app itself simply.

### TriggerDevice

To use the latest version with TCP connection, you need a RaspberryPi board with a linux distro, preferably [Ubuntu Server](https://ubuntu.com/download/raspberry-pi). Your board must be using Broadcom 2835 chip in order for dynamic library to work correctly.

First, install RaspberryPiDotNet and follow its installation. Then, compile the project with `AnyCPU` architecture. [Mono](https://www.mono-project.com/download/stable/) MUST be installed for TriggerDevice to work. To change a pin, you can create a `.gpio` file in the same folder as the project is running. The content in `.gpio` file must be the exact same as enum names defined in [RaspberryPiDotNet.GPIOPins.cs](https://github.com/cypherkey/RaspberryPi.Net/blob/master/RaspberryPiDotNet/GPIOPins.cs). Lastly, every configuration can be done through the windows app. Make sure your Raspberry is under `192.168.1.x` subnet, on the same local network as desktop app.

#### - Arduino TriggerDevice

To try the Arduino version of TriggerDevice (Really, not recommended as it is not real-time) you need to first have a working Arduino board, and Arduino drivers installed (including the Serial/USB) driver. [Have a look here](https://github.com/AliDeym/Bifler/blob/master/libBifler/TriggerDevice.cs#L408). A lot of modification is required to make it work again, since it was completely removed from the project but kept in comments as a backup.

Additionally, Arduino source code and required files can be found in [Trigger-source](https://github.com/AliDeym/Bifler/tree/master/Trigger-source).

### Radio Device

Radio needs `.NET Core` framework to get compiled. As for board, you need any device that supports [Windows 10 IoT](https://developer.microsoft.com/en-us/windows/iot/). Using VisualStudio, all you need is a stable internet connection in your IoT board only during the installation period. It is highly recommended to have a touch-screen module installed on your board for production phase.

# Contributors

Bifler © 2017-2018 by
Ali Deym: Developer
[Hossein Abedi Nashi (Biftor)](https://www.linkedin.com/in/hossein-abedi-nashi): Electronic Circuit Designer, Arduino Developer and Project Manager