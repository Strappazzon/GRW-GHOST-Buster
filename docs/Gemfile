# Lock Bundler version
# https://stackoverflow.com/a/51788614
if (version = Gem::Version.new(Bundler::VERSION)) < Gem::Version.new('2.4.5')
  abort "Bundler version >= 2.4.5 is required. You are running #{version}."
end

source 'https://rubygems.org'

gem 'jekyll', '~> 4.4'
gem 'mini_racer', '~> 0.18'
# Avoid updating to version >= 1.80.0 due to import deprecation
# silence_deprecations does not seem to work
gem 'sass-embedded', '~> 1.79.0'
