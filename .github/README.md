<!-- markdownlint-disable-next-line MD041 -->
<div align="center">
  <img width="96" src="https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/favicon.png" alt="Logo">
</div>

<div align="center">
  <strong>GHOST Buster</strong>
</div>

<p align="center">
  <em>Backup and restore Ghost Recon Wildlands save games</em>
</p>

## Getting Started

- [Download the latest version](https://github.com/Strappazzon/GRW-GHOST-Buster/releases/latest) of GHOST Buster

- Launch **GHOST Buster.exe**
  - Every time you launch it, the program will check the System Registry to see if Ghost Recon Wildlands and Ubisoft Connect are installed
  - When you close it, a file containing the program's settings will be created (or updated if it already exists) inside `%LOCALAPPDATA%\GHOSTbackup`

- Click the **Browse...** button under "**Folders** -> **Wildlands save games folder**" to select Wildlands save games folder
  - Refer to the [PC Gaming Wiki](https://www.pcgamingwiki.com/wiki/Tom_Clancy%27s_Ghost_Recon_Wildlands#Save_game_data_location)
    if you don't know where save games are located

- Click the **Browse...** button under "**Folders** -> **Backup location**" to select where Wildlands save games will be backed up to
  - Each backup will be stored inside a `yyyyMMdd HHmm` sub-folder

- Specify the backup frequency and click the **Start Backup** button. To interrupt the process, click **Stop Backup**
  - You can specify a value between 1 and 180

### Restoring a Backup

> [!WARNING]
>
> Disable cloud synchronization before restoring a backup, otherwise Ubisoft Connect will redownload the old save games rendering the restore process useless.
> Also note that restoring a backup will **OVERWRITE** your old save games and it **CANNOT BE UNDONE**.

#### From Tasks screen

- Close the game
- Choose which backup you want to restore
  - *Latest*
  - *Second-to-last*
    - If it doesn't exist, you'll be prompted to restore the latest backup instead
- Click the **Restore Backup** button

#### From Manage Backups screen

- Close the game
- Right click on the backup you want to restore
- Click **Restore**

## Settings

- **Interface language**
  - *Default: English*

- **Confirm exit when backup is running**
  - *Enabled by default*
  - The program will show a confirmation dialog before quitting if the backup process is running

- **Confirm backup interruption**
  - *Disabled by default*
  - The program will show a confirmation dialog before interrupting the backup process

- **Display notifications about backups**
  - *Disabled by default*
  - The program will display a toast notification every time a backup is performed

- **Disable Ubisoft Connect cloud save synchronization on restore**
  - *Disabled by default*
  - The program will disable Ubisoft Connect cloud save synchronization before restoring a backup
  - Keep in mind that the Ubisoft Connect settings file may change at any time so this setting may not be reliable

- **Enable Ubisoft Connect cloud save synchronization on exit**
  - *Enabled by default*
  - *Works only if the previous setting is enabled*
  - The program will re-enable Ubisoft Connect cloud save synchronization before quitting

- **Check for updates on startup**
  - *Disabled by default*
  - The program will connect to GitHub servers to check if the current version is up to date

- **Remember window position**
  - *Disabled by default*
  - The program will remember the window position the last time GHOST Buster was used

- **Manually locate installed game**
  - *Disabled by default*
  - You can specify a different location for the Wildlands executable

- **Write events to a log file**
  - *Disabled by default*
  - The program will log all events (such as Errors and Warnings) to a file
  - The default log file location is `%LOCALAPPDATA%\GHOSTbackup\event.log`

All settings are stored inside `%LOCALAPPDATA%\GHOSTbackup\ghostbackup.cfg`.

## Screenshots

| Main screen                                                                                      | Backups                                                                                               | Settings                                                                                                 | Notification                                                                                              |
|:------------------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------------------:|:--------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------:|
| ![Main screen](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/main.png) | ![Backups screen](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/manage.png) | ![Settings screen](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/settings.png) | ![Notification](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/notification.png) |

| Backup restore                                                                                                | Backup deletion                                                                                               | Logs screen                                                                                      | About screen                                                                                       |
|:-------------------------------------------------------------------------------------------------------------:|:-------------------------------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------:|:--------------------------------------------------------------------------------------------------:|
| ![Backup restore prompt](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/restore.png) | ![Backup deletion prompt](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/delete.png) | ![Logs screen](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/logs.png) | ![About screen](https://strappazzon.xyz/projects/grw-ghost-buster/assets/img/screenshot/about.png) |
