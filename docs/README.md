# GitHub Pages

The **docs** folder contains an HTML version of [README.md](https://github.com/Strappazzon/GRW-GHOST-Buster#readme).

If you are looking for *GHOST Buster* source code, please see the ["src" folder](https://github.com/Strappazzon/GRW-GHOST-Buster/tree/-/src).

## Getting Started

### Dependencies

The GHOST Buster website is built with [Jekyll](https://jekyllrb.com/) so you will need [Ruby](https://www.ruby-lang.org) installed.  
I recommend you install it using [rbenv](https://github.com/rbenv/rbenv), [asdf](https://github.com/asdf-vm/asdf), [frum](https://github.com/TaKO8Ki/frum)
or other packaging system, before attempting to install the dependencies.

Clone the repo and run `bundle install` to install the required dependencies.

```sh
git clone https://github.com/Strappazzon/GRW-GHOST-Buster.git GRW-GHOST-Buster
cd docs/
bundle install
```

### Local Previews

To view your changes locally use the following command:

```sh
bundle exec jekyll serve --force_polling --livereload
```

Then navigate to <http://localhost:4000/GRW-GHOST-Buster/> in your web browser.

Press <kbd>CTRL</kbd>+<kbd>C</kbd> to stop the web server.
