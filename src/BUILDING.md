# Building GHOST Buster from its source code

This file should assist you in compiling and running GHOST Buster.

## Building requirements

- [.NET Framework 4.6.1 or newer](https://dotnet.microsoft.com/download/dotnet-framework).
- Visual Studio. You can [download the community edition](https://visualstudio.microsoft.com/vs/community/) for free.
  - The ".NET desktop development" Workload is required.

## Getting the source code

Clone the repository via Git:

```cmd
git clone https://github.com/Strappazzon/GRW-GHOST-Buster.git GRW-GHOST-Buster
cd GRW-GHOST-Buster
```

Alternatively, you can clone the repository via any Git client, or download a zip archive of the repository [from here](https://github.com/Strappazzon/GRW-GHOST-Buster/archive/master.zip).

## Building with Visual Studio

1. Start Visual Studio.
2. Select **Open a project or solution** and open the `GHOSTbackup.sln` solution file located in your cloned repository (In this case: `.\GRW-GHOST-Buster\src\GHOSTbackup.sln`).
3. Compile using **Build** -> **Build Solution**.

The compiled binary will be inside the `.\GHOSTbackup\bin\Debug` folder

## Building with build script

1. Run `build.bat` located in your cloned repository (In this case: `.\GRW-GHOST-Buster\script\build.bat`).

The compiled binary will be inside the `.\GHOSTbackup\bin\Release` folder.
