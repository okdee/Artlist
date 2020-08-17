using System;
using System.Threading.Tasks;
using ArtlistConverter.DAL;
using FFmpegWrapperLibrary;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Interfaces;

namespace ArtlistConverter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var uploadedFile = await UploadFilesDAL.Upload(Request.Form.Files[0]);

                var video = new Video(uploadedFile.Path, uploadedFile.Name, uploadedFile.Extension);

                var videoProcessor = new ProcessVideoDAL(new FFmpegWrapper());

                var processedVideo = await videoProcessor.ScaleAndEncodeInH264(video, new Dimensions(1280, 720));

                var videoThumbnail = await videoProcessor.CreateVideoThumbnail(video);

                return Ok("Finished Processing the Video.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
