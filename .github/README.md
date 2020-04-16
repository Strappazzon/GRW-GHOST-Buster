<h1 align="center">
  <br>
  <img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/favicon.png" width="128px" alt="GHOST Buster Icon"></a>
  <br>
  GHOST Buster
  <br>
</h1>

<h4 align="center">Backup and restore Ghost Recon Wildlands save games.</h4>

<p align="center">
  <a href="https://strappazzon.xyz/GRW-GHOST-Buster"><img alt="HTML Version" src="https://img.shields.io/badge/HTML%20Version-%23ff5f2f?style=flat-square&logo=HTML5&logoColor=%23ffffff"></a>
  <a href="https://github.com/Strappazzon/GRW-GHOST-Buster/releases"><img alt="GitHub All Releases" src="https://img.shields.io/github/downloads/Strappazzon/GRW-GHOST-Buster/total?color=%23dd3333&label=Downloads&logo=DocuSign&logoColor=%23ffffff&style=flat-square"></a>
  <a href="https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest"><img alt="Latest Release" src="https://img.shields.io/github/v/release/Strappazzon/GRW-GHOST-Buster?color=%230077ee&include_prereleases&label=Latest%20Release&style=flat-square"></a>
  <a href="https://travis-ci.org/Strappazzon/GRW-GHOST-Buster/builds"><img alt="Build Status" src="https://img.shields.io/travis/Strappazzon/GRW-GHOST-Buster/ci?color=%23724cdb&label=Build&logo=travis-ci&logoColor=%23ffffff&style=flat-square"></a>
</p>

<p align="center">
  <a href="#about">About</a> •
  <a href="#getting-started">Getting Started</a> •
  <a href="#settings">Settings</a> •
  <a href="#screenshots">Screenshots</a> •
  <a href="#download">Download</a> •
  <a href="#license">License</a>
</p>

## About

**GHOST Buster** will automatically backup your Wildlands save games while the game is running.

I created this software to backup [Ghost Mode](https://web.archive.org/web/20190108052618/https://ghost-recon.ubisoft.com/wildlands/en-us/news/152-328968-16/special-operation-2-is-coming) save games, hence the name "GHOST Buster".

## Getting Started

* Launch **GHOST Buster.exe**
  * Every time you launch it, the program will check the System Registry to see if Ghost Recon Wildlands and Uplay are installed
  * When you close it, a file containing the program's settings will be created (or updated if it already exists) inside `%LOCALAPPDATA%\GHOSTbackup`

* Click the **Browse...** button under "**Folders** -> **Wildlands save games folder**" to select Wildlands save games folder
  * Refer to the [PC Gaming Wiki](https://www.pcgamingwiki.com/wiki/Tom_Clancy%27s_Ghost_Recon_Wildlands#Save_game_data_location) if you don't know where save games are located

* Click the **Browse...** button under "**Folders** -> **Backup location**" to select where Wildlands save games will be backed up to
  * Each backup will be stored inside a `yyyyMMdd HHmm` sub-folder

* Specify the backup frequency and click the **Start Backup** button. To interrupt the process, click **Stop Backup**
  * You can specify a value between 1 and 180

### Restoring a Backup

> ### ⚠️ **WARNING**
>
> Disable cloud synchronization before restoring a backup, otherwise Uplay will redownload the old save games rendering the restore process useless.
>
> Also note that restoring a backup will **OVERWRITE** your old save games and it **CANNOT BE UNDONE**.

#### From Tasks screen

* Close the game
* Choose which backup you want to restore
  * _Latest_
  * _Second-to-last_
    * If it doesn't exist, you'll be prompted to restore the latest backup instead
* Click the **Restore Backup** button

#### From Manage Backups screen

* Close the game
* Right click on the backup you want to restore
* Click **Restore**

## Settings

* **Interface language**
  * *Default: English*

* **Confirm exit when backup is running**
  * _Enabled by default_
  * The program will show a confirmation dialog before quitting if the backup process is running

* **Confirm backup interruption**
  * _Disabled by default_
  * The program will show a confirmation dialog before interrupting the backup process

* **Display notifications about backups**
  * *Disabled by default*
  * The program will display a notification at the edge of the screen every time a backup is performed
  * Enable this option only if you play Wildlands in borderless fullscreen or windowed, otherwise the game will lose focus every time the notification is displayed

* **Disable Uplay cloud save synchronization on restore**
  * _Disabled by default_
  * The program will disable Uplay cloud save synchronization before restoring a backup
  * Keep in mind that the Uplay settings file may change at any time so this setting may not be reliable

* **Enable Uplay cloud save synchronization on exit**
  * *Enabled by default*
  * *Works only if the previous setting is enabled*
  * The program will re-enable Uplay cloud save synchronization before quitting

* **Check for updates on startup**
  * _Disabled by default_
  * The program will connect to GitHub servers to check if the current version is up to date

* **Remember window position**
  * _Disabled by default_
  * The program will remember the window position the last time GHOST Buster was used

* **I'm not using the Uplay version of Wildlands**
  * _Disabled by default_
  * You can specify a different location for the Wildlands executable

* **Write events to a log file**
  * _Disabled by default_
  * The program will log all events (such as Errors and Warnings) to a file
  * The default log file location is `%LOCALAPPDATA%\GHOSTbackup\event.log`

All settings are stored inside `%LOCALAPPDATA%\GHOSTbackup\ghostbackup.cfg`.

## Screenshots

_Click an image to enlarge it_

<table>
  <tr>
    <th align="center">Main screen</th>
    <th align="center">Backups</th>
    <th align="center">Settings</th>
    <th align="center">Notification</th>
  </tr>
  <tr>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot.png"></td>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot_manage.png"></td>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot_settings.png"></td>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot_notification.jpg"></td>
  </tr>
</table>

<table>
  <tr>
    <th align="center">Backup restore</th>
    <th align="center">Backup deletion</th>
    <th align="center">Logs screen</th>
    <th align="center">About screen</th>
  </tr>
  <tr>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot_restore.png"></td>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot_delete.png"></td>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot_logs.png"></td>
    <td><img src="https://strappazzon.xyz/GRW-GHOST-Buster/assets/img/screenshot_about.png"></td>
  </tr>
</table>

## Download

You can [download](https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest) the latest version of GHOST Buster from the Releases page.

## Contributing

If you are interested in fixing issues and contributing directly to the code base, please see:

* [Contribution Guidelines](./CONTRIBUTING.md)
* [Code of Conduct](./CODE_OF_CONDUCT.md)
* [Building GHOST Buster from its source code](https://github.com/Strappazzon/GRW-GHOST-Buster/blob/master/src/BUILDING.md)

## License

```
Copyright (c) 2019 - 2020 Alberto Strappazzon

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
