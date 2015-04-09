var task = {
  'nuGet-win': {
    command: 'Nuget.exe restore',
  },
  'nuGet': {
    command: 'mono \"./NuGet.exe\" \"restore\"'
  },
  'xbuild': {
    command: 'xbuild'
  }
};

module.exports = task;
