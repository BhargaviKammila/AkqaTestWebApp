using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TestWebApp
{
    public static class ServiceLocator
    {
        private static CompositionContainer _container;

        private static AggregateCatalog StrongNameCatalog(string path)
        {
            AggregateCatalog _aggregateCatalog = new AggregateCatalog();

            foreach (var file in Directory.GetFiles(path, "NumToText.*.dll"))
            {
                AssemblyName assemblyName = null;
                try
                {
                    assemblyName = AssemblyName.GetAssemblyName(file);
                }
                catch (ArgumentException) { }
                catch (BadImageFormatException) { }
                catch (Exception) { }

                if (assemblyName != null)
                {
                    _aggregateCatalog.Catalogs.Add(new AssemblyCatalog(file));
                }
            }

            return _aggregateCatalog;
        }

        public static void Initialize()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (Directory.Exists(path + "\\bin\\"))
            {
                path = path + "\\bin\\";
            }

            var catalog = StrongNameCatalog(path);

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog, true);
        }

        static ServiceLocator()
        {
            Initialize();
        }

        public static T GetInstance<T>()
        {
            return _container.GetExportedValue<T>();
        }

        public static void Compose<T>(T part)
        {
            _container.ComposeParts(part);
        }
    }
}