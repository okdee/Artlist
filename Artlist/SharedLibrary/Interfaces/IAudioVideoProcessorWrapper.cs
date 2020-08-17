using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Interfaces
{
    public interface IAudioVideoProcessorWrapper
    {
        public Task<string> Scale(IVideo video, Dimensions newScale);
        public Task<IVideo> ScaleAndEncodeInH264(IVideo video, Dimensions dimensions);
        public Task<IImage> CombineTwoImages(IImage image1, IImage image2);
        public Task<IList<IImage>> GetSnapshots(IVideo video, IList<int> seconds);
    }
}
