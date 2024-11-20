﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ConceptorUI.Classes;
using ConceptorUI.Utils;


namespace ConceptorUI
{
    public partial class SplashView
    {
        public SplashView()
        {
            InitializeComponent();
            DoWork();
        }

        private void DoWork()
        {
            var projects = new List<Project>();
            var sc = SynchronizationContext.Current;

            ThreadPool.QueueUserWorkItem(delegate
            {
                var dirBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!new DirectoryInfo(dirBase + @"\UIConceptor").Exists)
                {
                    Directory.CreateDirectory(dirBase + @"\UIConceptor");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Configs");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Projects");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Medias");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Icons");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Fonts");
                    File.Create(dirBase + @"\UIConceptor\Configs\config.json").Dispose();

                    sc!.Post(delegate
                    {
                        _createImage();
                        File.WriteAllText(
                            dirBase + @"\UIConceptor\Configs\config.json",
                            JsonSerializer.Serialize(projects)
                        );
                    }, null);
                }
                else
                {
                    var dircf = new DirectoryInfo(dirBase + @"\UIConceptor\Configs");
                    var dirpj = new DirectoryInfo(dirBase + @"\UIConceptor\Projects");
                    var dirmd = new DirectoryInfo(dirBase + @"\UIConceptor\Medias");

                    if (!dircf.Exists)
                    {
                        Directory.CreateDirectory(dirBase + @"\UIConceptor\Configs");
                    }

                    if (!dirpj.Exists)
                    {
                        Directory.CreateDirectory(dirBase + @"\UIConceptor\Projects");
                    }

                    if (!dirmd.Exists)
                    {
                        Directory.CreateDirectory(dirBase + @"\UIConceptor\Medias");
                    }
                }

                sc!.Post(delegate
                {
                    _createImage();
                    var dirs = Directory.GetDirectories(Path.Combine(dirBase, @"UIConceptor\Projects"), "*",
                        SearchOption.TopDirectoryOnly);
                    
                    for (var i = 0; i < dirs.Length; i++)
                    {
                        projects.Add(new Project
                        {
                            ID = $"ID{i}",
                            Name = $"Application {i}",
                            Password = "",
                            Color = "transparent",
                            Created = DateTime.Now,
                            Updated = DateTime.Now,
                            FolderPath = Path.Combine(dirBase, dirs[i]),
                            Image = ""
                        });
                    }

                    PreviewPage.Instance.Show(projects);
                    Close();
                }, null);
            });
        }

        private void _createImage()
        {
            var dirBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filePath = Path.Combine(dirBase, $"UIConceptor/Medias/mobile.png");

            if (File.Exists(filePath)) return;

            AppImage.SizeChanged += (_, _) =>
            {
                Console.WriteLine(@"Création de l'Image  par défaut.");
                Helper.SaveBitmapImage(AppImage, filePath);
            };

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/Assets/mobile.png", UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            AppImage.Source = bitmap;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}