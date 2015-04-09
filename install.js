var os = require('os');
var process = require('child_process');

var isWin = /^win/.test(os.platform());

var cb = function (error, stdout, stderr) {
  console.log(stdout);
  console.log(stderr);
  if (error !== null) {
    console.log('exec error: ' + error);
  }
};

if (isWin) {
  process.exec('grunt build-win', cb);
} else {
  process.exec('grunt build', cb);
}
