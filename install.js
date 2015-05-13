var os = require('os');
var path = require('path');
var process = require('child_process');

var isWin = /^win/.test(os.platform());

var cb = function (error, stdout, stderr) {
  console.log(stdout);
  console.log(stderr);
  if (error !== null) {
    console.log('exec error: ' + error);
  }
};

var gruntfilePath = path.join(__dirname, 'Gruntfile.js');
var gruntPath = path.join('node_modules', 'grunt-cli', 'bin', 'grunt');

if (isWin) {
  process.exec(gruntPath + ' --gruntfile ' + gruntfilePath + ' build-win', cb);
} else {
  process.exec(gruntPath + ' --gruntfile ' + gruntfilePath + ' build', cb);
}
