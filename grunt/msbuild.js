var task = {
  templateGenerator: {
    src: ['TemplateGenerator.sln'],
    options: {
      projectConfiguration: 'Release',
      targets: ['Clean', 'Rebuild'],
      stdout: true,
      stderr: true,
      maxCpuCount: 4,
      verbosity: 'normal',
      execOptions: { maxBuffer: 1000 * 1024 }
    }
  }
};

module.exports = task;
