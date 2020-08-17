using SharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLibrary.Interfaces;
using SharedLibrary;

namespace ArtlistConverter.DAL
{
    public class ProcessVideoDAL
    {
        IAudioVideoProcessorWrapper AVWrapper;
        public ProcessVideoDAL(IAudioVideoProcessorWrapper wrapper)
        {
            AVWrapper = wrapper;
        }

        public async Task<IVideo> ScaleAndEncodeInH264(IVideo video, Dimensions dimensions)
        {
            return await AVWrapper.ScaleAndEncodeInH264(video, dimensions);
        }

        public async Task<IImage> CreateVideoThumbnail(IVideo video)
        {
            var snapshotsPaths = await AVWrapper.GetSnapshots(video, new List<int>() { 1, 3 });

            if (snapshotsPaths != null)
            {
                return await AVWrapper.CombineTwoImages(snapshotsPaths[0], snapshotsPaths[1]);
            }

            return null;
        }
    }
}
