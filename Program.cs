using System;
using System.Collections.Generic;
using System.IO;
using Spark;
using Spark.FileSystem;
using Spark.Web.Mvc;

namespace TemplateGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("You must supply the view folder path and output path.");
                    return;
                }

                Console.WriteLine("Converting {0} folder", args[0]);

                var settings = new SparkSettings();
                var factory = new SparkViewFactory(settings);
                var files = Directory.GetFiles(args[0], "Shared/*.spark", SearchOption.AllDirectories);

                factory.ViewFolder = new FileSystemViewFolder(args[0]);

                foreach (string file in files)
                {
                    string parsedFile = file.Replace(string.Format("{0}\\Shared\\", args[0]), string.Empty);
                    parsedFile = parsedFile.Replace(".spark", string.Empty);

                    var buildDescriptorParams = new BuildDescriptorParams(string.Empty, string.Empty, parsedFile,
                                                                          string.Empty, false,
                                                                          new Dictionary<string, object>());

                    var descriptor = factory.DescriptorBuilder.BuildDescriptor(buildDescriptorParams, new List<string>());
                    descriptor.Language = LanguageType.Javascript;

                    var entry = factory.Engine.CreateEntry(descriptor);

                    string filePath = string.Format("{0}.js", Path.Combine(args[1], parsedFile));

                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    File.WriteAllText(filePath, entry.SourceCode);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: "+exception.Message);
                Console.WriteLine(exception.StackTrace);
                throw exception;
            }
            Console.WriteLine("Finished Template Generation.");
        }
    }
}
