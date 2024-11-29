﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Application.Project;
using Microsoft.Win32;
using Syncfusion.Licensing;


namespace ConceptorUI
{
    public partial class App
    {
        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense(
                "Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXpec3VXRmJdUEN1XEo=");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");

            CreateFileType();
            RefreshIconCache();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Length > 0)
            {
                var filePath = e.Args[0];
                var filename = Path.GetFileName(filePath).Replace(".uix", "");
                ProjectInfoUiDto projectInfoUiDto;
                
                var metaDataResult = await new GetProjectMetaDataQueryHandler().Handle(new GetProjectMetaDataQuery
                {
                    ZipPath = filePath
                });

                if (metaDataResult.IsSuccess)
                {
                    projectInfoUiDto = metaDataResult.Value;
                    Console.WriteLine($@"Meta données trouvées");
                }
                else
                {
                    var projectNaturalInfosResult = await new GetProjectNaturalInfosQueryHandler()
                        .Handle(new GetProjectNaturalInfosQuery { ZipPath = filePath });
                    
                    if(projectNaturalInfosResult.IsFailure) return;
                    var projectNaturalInfos = projectNaturalInfosResult.Value;
                    
                    projectInfoUiDto = new ProjectInfoUiDto
                    {
                        Id = projectNaturalInfos.OriginalName,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        ZipPath = filePath,
                        Image = projectNaturalInfos.Image
                    };
                    
                    await new SaveProjectMetaDataCommandHandler().Handle(new SaveProjectMetaDataCommand
                    {
                        Id = projectNaturalInfos.OriginalName,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        ZipPath = filePath,
                        Image = projectNaturalInfos.Image
                    });
                    Console.WriteLine($@"Enregistrement des Meta données");
                }

                new MainWindow().Show(projectInfoUiDto);
                return;
            }

            var splashView = new SplashView();
            splashView.Show();
        }

        private static void RefreshIconCache()
        {
            Shell32Interop.SHChangeNotify(Shell32Interop.SHCNE_ASSOCCHANGED, Shell32Interop.SHCNF_FLUSH, IntPtr.Zero,
                IntPtr.Zero);
        }

        public class Shell32Interop
        {
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

            public const uint SHCNE_ASSOCCHANGED = 0x08000000;
            public const uint SHCNF_FLUSH = 0x1000;
        }

        private static void CreateFileType()
        {
            try
            {
                // Nom de l'extension de fichier et du chemin de l'exécutable
                const string extension = ".uix";
                var appPath = Assembly.GetExecutingAssembly().Location;
                appPath = appPath.Replace("dll", "exe");
                var iconPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @$"UIConceptor\icon.png");
                //Console.WriteLine($@"iconPath: {iconPath}");

                // Clé pour l'extension
                using (var key = Registry.ClassesRoot.CreateSubKey(extension))
                {
                    if (key != null!)
                    {
                        key.SetValue("", "ConceptorUix");
                    }
                }

                // Clé pour le type de fichier
                using (var key = Registry.ClassesRoot.CreateSubKey("ConceptorUix"))
                {
                    if (key == null!) return;

                    key.SetValue("", "Fichier de mon application WPF");
                    using var subKey = key.CreateSubKey("shell\\open\\command");
                    if (subKey != null!)
                        subKey.SetValue("", "\"" + appPath + "\" \"%1\"");

                    using var iconKey = key.CreateSubKey("DefaultIcon");
                    if (iconKey != null!)
                        iconKey.SetValue("", iconPath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@$"Echec d'ajout de l'extension de fichier. Message: {e.Message}");
            }
        }
    }
}