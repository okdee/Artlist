using SharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharedLibrary
{
    public class File : IFile
    {
        public File(FileExtension extension)
        {
            Extension = extension;
        }
        public string Path { get; set; }
        public string Name { get; set; }
        public FileExtension Extension { get; set; }
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
