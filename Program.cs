using System;
using System.Collections.Generic;
using System.IO;
using Spark;
using Spark.FileSystem;
using Spark.Web.Mvc;
using Spark.Web.Mvc.Descriptors;

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

                Console.WriteLine("Converting spark templates in {0}, moving to {1}", args[0], args[1]);

                var settings = new SparkSettings().SetPageBaseType(typeof(SparkView));
                var templates = new FileSystemViewFolder(args[0]);
                var factory = new SparkViewFactory(settings) { ViewFolder = templates };
                var files = Directory.GetFiles(args[0], "*.spark", SearchOption.AllDirectories);

                Console.WriteLine("Found {0} files to process", files.Length);

                foreach (string file in files)
                {
                    string sparkFile = file.Replace(args[0] + System.IO.Path.PathSeparator, string.Empty);
                    string fileName = sparkFile.Replace(".spark", string.Empty);

                    var descriptor = new SparkViewDescriptor();
                    descriptor.Language = LanguageType.Javascript;
                    descriptor.Templates.Add(sparkFile);

                    //Maybe this has to be done in order so the other templates exist?
                    var entry = factory.Engine.CreateEntry(descriptor);

                    string filePath = string.Format("{0}.js", Path.Combine(args[1], fileName));

                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    File.WriteAllText(filePath, entry.SourceCode);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.Message);
                Console.WriteLine(exception.StackTrace);
                throw exception;
            }
            Console.WriteLine("Finished Template Generation.");
        }
    }
}
