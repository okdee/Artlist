using SharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;
using System.Reflection;

namespace FFmpegWrapperLibrary
{
    public class FFmpegWrapper : IFFmpegWrapper
    {
        // The following line returns the path to the main porject, it should contain the path to the FFmpegWrapper library
        public static readonly string runningPath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string workingDirectory = $@"{Path.GetFullPath(Path.Combine(runningPath, @"..\..\..\"))}Resources\ffmpeg\bin";
        public static readonly string fileName = $@"{Path.GetFullPath(Path.Combine(runningPath, @"..\..\..\"))}Resources\ffmpeg\bin\ffmpeg.exe";
        public static readonly IFile ffmpegFile = new SharedLibrary.File(new FileExtension("exe")) { Name = "ffmpeg" };

        static void HandleFFmpegProcessExited(object sender, EventArgs e)
        {

        }

        public async Task<IImage> CombineTwoImages(IImage image1, IImage image2)
        {
            var fileNames = new StringBuilder();

            fileNames.Append($"-i \"{Path.Combine(image1.Path, image1.FullName)}\"" +
                $" -i \"{Path.Combine(image2.Path, image2.FullName)}\"");

            var thumbnailName = $"{image1.Name}_{image2.Name}_{DateTime.Now.Date.ToString("dd-MM-yyyy")}.png";

            var ffmpegCombineImagesCommand = $"-y {fileNames} -filter_complex hstack" +
                $" \"{Path.Combine(image1.Path, thumbnailName)}\"";

            var _ffmpegProcess = FFmpegProcessFactory.GetFFmpegProcess(ffmpegFile, ffmpegCombineImagesCommand);

            try
            {
                _ffmpegProcess.Start();
                _ffmpegProcess.WaitForExit();

                image1.Delete();
                image2.Delete();

                return new Image(image1.Path, thumbnailName, new FileExtension("png"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IVideo> ScaleAndEncodeInH264(IVideo video, Dimensions dimensions)
        {
            var newVideoName = $"{video.Name}_720pH264";

            var ffmpegEncodeAndConvertToH264HDCommandArgs =
                $"-y -i \"{Path.Combine(video.Path, $"{video.Name}.{video.Extension.Name}")}\"" +
                $" -vf scale={dimensions.Width}:{dimensions.Height} -vcodec libx264 -crf 23" +
                $" \"{Path.Combine(video.Path, $"{newVideoName}.{video.Extension.Name}")}\"";

            var _ffmpegProcess = FFmpegProcessFactory.GetFFmpegProcess(ffmpegFile, ffmpegEncodeAndConvertToH264HDCommandArgs);

            try
            {
                _ffmpegProcess.Start();
                _ffmpegProcess.WaitForExit();

                return new Video(video.Path, newVideoName, video.Extension);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<IImage>> GetSnapshots(IVideo video, IList<int> seconds)
        {
            var _ffmpegProcess = FFmpegProcessFactory.GetFFmpegProcess(ffmpegFile, string.Empty);
            var listToReturn = new List<IImage>();

            foreach (var second in seconds)
            {
                var imageName = $"thumbnail_{second}";

                var ffmpegGetThumbnailCommandArgs =
                    $"-y -i \"{Path.Combine(video.Path, $"{video.Name}.{video.Extension.Name}")}\"" +
                    $" -vframes 1 -an -s 640x480 -ss {second}" +
                    $" \"{Path.Combine(video.Path, $"{imageName}.png")}\"";

                _ffmpegProcess.StartInfo.Arguments = ffmpegGetThumbnailCommandArgs;

                try
                {
                    _ffmpegProcess.Start();
                    _ffmpegProcess.WaitForExit();

                    listToReturn.Add(new Image(video.Path, imageName, new FileExtension("png")));
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return listToReturn;
        }

        public Task<string> Scale(IVideo video, Dimensions newScale)
        {
            throw new NotImplementedException();
        }
    }
}
