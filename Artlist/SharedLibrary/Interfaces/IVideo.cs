using System.IO;

namespace SharedLibrary.Interfaces
{
    public interface IVideo : IFile
    {
        public Dimensions Dimensions { get; set; }
    }
}