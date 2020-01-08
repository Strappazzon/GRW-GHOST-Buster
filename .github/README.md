<h1 align="center">
  <br>
  <img src="./Preview.png" alt="GHOST Buster"></a>
  <br>
  GHOST Buster
  <br>
</h1>

<h4 align="center">Backup and restore Ghost Recon Wildlands save games.</h4>

<p align="center">
  <a href="#about">About</a> •
  <a href="#getting-started">Getting Started</a> •
  <a href="#settings">Settings</a> •
  <a href="#download">Download</a> •
  <a href="#license">License</a>
</p>

## About

**GHOST Buster** will automatically backup your Wildlands save games while the game is running.

I created this software to backup [Ghost Mode](https://web.archive.org/web/20190108052618/https://ghost-recon.ubisoft.com/wildlands/en-us/news/152-328968-16/special-operation-2-is-coming) save games, hence the name "GHOST Buster".

Some friends of mine also requested it so I released it here on GitHub.

## Getting Started

* Launch **GHOST Buster.exe**.
  * Every time you launch it, the program will check the System Registry to see if Ghost Recon Wildlands and Uplay are installed.
  * When you close it, a file containing the program's settings will be created (or updated if it already exists) inside `%LOCALAPPDATA%\GHOSTbackup`

* Click the first **Browse...** button to select Wildlands save games folder.
  * They are usually located inside `C:\Program Files (x86)\Ubisoft\Ubisoft Game Launcher\savegames\y0ur-r4nd0m-us3r-1dent1f13r\1771`

* Click the second **Browse...** button to select where Wildlands save games will be backed up to.
  * A `yyyyMMdd HHMM` sub-folder will be created with every backup

* Finally, specify the backup interval and click the **Start The Backup** button. To interrupt the process, click **Stop The Backup**.

### Dead?

* Close the game and interrupt the backup process.
* Click the **Restore save games** button.
  * By default, GHOST Buster will restore the latest backup.

> ### ⚠️ **WARNING**
>
> Disable cloud synchronization before restoring a backup, otherwise Uplay will redownload the old save games rendering the restore process useless.
>
> Also note that restoring a backup will **OVERWRITE** your old save games and it **CANNOT BE UNDONE**.

## Settings

* **Confirm exit (if backup is active)**
  * _Enabled by default_
  * The program will warn you before closing it if the backup process is still running

* **Confirm backup interruption**
  * _Disabled by default_
  * The program will ask you if you're sure you want to interrupt the running backup process

* **Check for updates**
  * _Disabled by default_
  * The program will connect to GitHub servers to check if the current version is up to date

* **Remember window position**
  * _Disabled by default_
  * The program will remember the window position the last time GHOST Buster was used

### Advanced Settings

* **Write events to a log file**
  * _Disabled by default_
  * The program will log all events (such as Errors and Warnings) to a file

* **Let GHOST Buster disable cloud save synchronization**
  * _Disabled by default_
  * The program will disable Uplay cloud save synchronization prior restoring save games
  * Keep in mind that the Uplay settings file may change at any time so this setting may not be reliable

* **Choose which backup will be restored**
  * _Default setting: "Latest"_
    * When restoring a backup, GHOST Buster will restore the latest one
  * _"Second-to-last"_
    * When restoring a backup, GHOST Buster will restore the second-to-last one
    * If the second-to-last backup doesn't exist, you'll be prompted to restore the latest one instead
  * _"Let me decide"_
    * You will have to select a `yyyyMMdd HHmm` backup folder manually before starting the restore process and switch back to its parent folder manually after restoring the backup

All settings are stored inside `%LOCALAPPDATA%\GHOSTbackup`.

## Download

You can [download](https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest) the latest version of GHOST Buster from the Releases page.

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
