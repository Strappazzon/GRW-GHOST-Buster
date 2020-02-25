# GHOST Buster Contribution Guidelines

:tada: **Thanks for taking the time and effort to make GHOST Buster better!**

## Code of Conduct

This project and everyone participating in it is governed by the [GHOST Buster Code of Conduct](./CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code. Please report unacceptable behavior to `strappazzon [AT] protonmail [DOT] com`.

## Crash, Error, Issue reporting/Feature requests

* Take a look at the [issues](https://github.com/Strappazzon/GRW-GHOST-Buster/issues) first to make sure your issue/feature hasn't been reported/requested before. If so, engage in the already existing discussion.
* Check whether your issue/feature is already fixed/implemented.
* Check if the issue still exists in the latest release.
* Issues in languages other than English will be closed and ignored.
* If you are a Visual Basic .Net developer, you are always welcome to fix/implement an issue/feature yourself. PRs are welcome!
* Add one issue at a time. Do not put multiple issues into one thread.
* When reporting a bug please describe the steps which reproduce the problem.
* When reporting an error include the event log from GHOST Buster (**Logs** tab or **event.log** file). Include the logs in the issue in a [file attachment](https://help.github.com/en/github/managing-your-work-on-github/file-attachments-on-issues-and-pull-requests) or put it in a [PrivateBin](https://privatebin.net/) and provide the link to that paste.
* When reporting a crash include the stack trace from GHOST Buster (click the "**▼ Details**" button in the crash window). Include the stack trace in the issue in a [file attachment](https://help.github.com/en/github/managing-your-work-on-github/file-attachments-on-issues-and-pull-requests), or put it in a [PrivateBin](https://privatebin.net/) and provide the link to that paste.
* All issues must be properly formatted with Markdown. If you don't know what that is, read [Mastering Markdown](https://guides.github.com/features/mastering-markdown/) before submitting an issue.

### DO NOT post "+1" comments

**DO NOT post "+1", "me too", or similar comments - they just add noise to an issue.**

If you don't have any additional info/context to add but would like to indicate that you're affected by the issue, upvote the original issue by clicking its [+:smiley:] button and hitting :thumbsup: (+1) icon.

## Code Contribution

* Make changes on a separate branch, not on the master branch, then send your changes as a pull request.
* When submitting changes, you confirm that your code is licensed under the terms of the [MIT License](https://opensource.org/licenses/MIT).
* Please test (compile and run) your code before you submit changes. Untested code will **not** be merged!
* Make sure your PR is up-to-date with the rest of the code.

## Travis CI Build

[Travis CI](https://travis-ci.org/) is a continuous integration service used to build/test software hosted on GitHub.

All Pull Requests must pass the Travis CI checks before it can be merged. If a PR breaks the build (a Travis CI check fails and shows a red "X") you will need to fix the issue.

## Website Development

GHOST Buster website is inside `/docs`, so it’s easy to keep docs and code in sync.

The website is built with [Jekyll](https://jekyllrb.com/), and automatically deployed onto GitHub Pages.

Even though bundled in this repo, the website is regarded as a separate project.

### Local Previews

See [docs/README.md](https://github.com/Strappazzon/GRW-GHOST-Buster/blob/master/docs/README.md).

## Git Commit Messages

* Use the present tense ("Add feature" not "Added feature").
* Use the imperative mood ("Move cursor to..." not "Moves cursor to...").
* Reference issues and pull requests after the first line.
* When changing the website, README, documentation, etc., start the commit message with `docs:` ("docs: Update index.html").
* When changing files outside the `src` folder, end the commit message with `[travis skip]` [to skip the Travis CI build](https://docs.travis-ci.com/user/customizing-the-build/#skipping-a-build) ("docs: Update README.md [travis skip]").

## Project Structure

### Repository Structure

This is a brief description on how the repository files and folders are structured and what each one contains. It only contains the most relevant files and folders as most of them are brief and self-explanatory.

```
GRW-GHOST-Buster/
 ├─ .gitignore               # Ignored files, directories and paths
 ├─ .travis.yml              # Travis CI configuration
 ├─ version                  # File used by GHOST Buster updater
 │   
 ├─ .github                  # GitHub files
 │   ├─ CODE_OF_CONDUCT.md   # Code of Conduct
 │   ├─ CONTRIBUTING.md      # Contributing guidelines
 │   ├─ README.md            # Repository Readme file
 │   └─ ISSUE_TEMPLATE       # Issue templates
 │   
 ├─ .vscode                  # Visual Studio Code-related files
 │   └─ tasks.json           # Visual Studio Code tasks
 │   
 ├─ script                   # Scripts
 │   ├─ build.bat            # Build GHOST Buster from source
 │   └─ jekyll-serve.bat     # Preview the website locally
 │   
 ├─ docs
 │   ├─ _config.yml          # Jekyll configuration
 │   └─ assets               # Website assets (images, CSS, ...)
 │   
 └─ src                      # GHOST Buster source code
     ├─ BUILDING.md          # Building instructions
     ├─ GHOSTbackup.sln      # Visual Studio solution
     │
     └─ GHOSTbackup
         └─ res              # GHOST Buster assets used in the UI
             └─ LICENSES.txt # Assets licenses
```
