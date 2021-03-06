﻿using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace SharedLibrary.Interfaces
{
    public class Video : IVideo
    {
        public Video()
        {
            Dimensions = new Dimensions(0, 0);
            Extension = new FileExtension("");
        }
        public Video(string path, string name, FileExtension extension)
        {
            Path = path;
            Name = name;
            Extension = extension;
        }

        public Dimensions Dimensions { get; set; }
        public string Name { get; set; }
        public FileExtension Extension { get; set; }
        public string Path { get; set; }
        public string FullName => $"{Name}.{Extension?.Name ?? ""}";

        public void Delete()
        {
            try
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(Path, FullName)))
                {
                    System.IO.File.Delete(System.IO.Path.Combine(Path, FullName));
                }
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}