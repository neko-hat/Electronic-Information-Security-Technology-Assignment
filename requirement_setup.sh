#!/bin/bash

#install nodejs npm
sudo apt-get update && sudo apt-get install nodejs npm

#install nvm
curl -s -o- https://raw.githubusercontent.com/creationix/nvm/master/install.sh | bash


#downgrade nodejs version
nvm install 11.15.0
nvm use 11.15.0

#install gulp-cli
npm install -g gulp-cli

#install bower
npm install -g bower

#install bundler
gem install bundler
