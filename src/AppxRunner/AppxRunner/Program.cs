using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Management.Deployment;

namespace AppxRunner
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var followingArgs = new string[args.Length - 1];
            Array.Copy(args, 1, followingArgs, 0, followingArgs.Length);

            await Run(
                appxPath: args[0],
                appxArgs: followingArgs).ConfigureAwait(false);
        }

        public static async Task Run(string appxPath, IReadOnlyList<string> appxArgs)
        {
            var manager = new PackageManager();

            await Install(manager, appxPath).ConfigureAwait(false);
            try
            {
                
            }
            finally
            {
                //await manager.RemovePackageAsync(packageFullName);
            }
        }

        private static async Task Install(PackageManager manager, string appxPath)
        {
            var appxFileName = Path.GetFileNameWithoutExtension(appxPath);
            var architecture = appxFileName.Substring(appxFileName.LastIndexOf('_') + 1);

            var dependencies = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(appxPath), "Dependencies", architecture), "*.appx");
            
            await manager.AddPackageAsync(
                new Uri(appxPath),
                Array.ConvertAll(dependencies, path => new Uri(path)),
                DeploymentOptions.None);
        }
    }
}
