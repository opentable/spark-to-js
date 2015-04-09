var os = require('os');
var process = require('child_process');

var isWin = /^win/.test(os.platform());

if (isWin) {
  process.execFileSync('grunt', ['build-win']);
} else {
  process.execFileSync('grunt', ['build']);
}
