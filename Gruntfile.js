/* jshint maxstatements: 20 */

'use strict';

module.exports = function(grunt) {
  // Automatically load in all Grunt npm tasks
  require('load-grunt-config')(grunt);

  grunt.registerTask('build', [
    'shell:nuGet', 'shell:xbuild'
  ]);

  grunt.registerTask('build-win', [
    'shell:nuGet-win', 'msbuild:templateGenerator'
  ]);
};
