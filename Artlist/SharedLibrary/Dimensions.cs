namespace SharedLibrary.Interfaces
{
    public class Dimensions
    {
        public Dimensions(int width, int height)
        {
            Height = height;
            Width = width;
        }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}