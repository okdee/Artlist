namespace SharedLibrary.Interfaces
{
    public interface IImage : IFile
    {
        public Dimensions Dimensions { get; set; }

    }
}