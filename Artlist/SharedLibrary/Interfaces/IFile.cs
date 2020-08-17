using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace SharedLibrary.Interfaces
{
    public interface IFile
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public FileExtension Extension { get; set; }
        public string FullName { get; }

        public void Delete();
    }
}
