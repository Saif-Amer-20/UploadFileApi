using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace base64.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
     
        private IHostingEnvironment _hostingEnvironment;

        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok("Upload Successful.");
            }
            catch (System.Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }


        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile1([FromBody] Test model)
        {
            var imageDataStream = new MemoryStream(model.ImgData);
            imageDataStream.Position = 0;

            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(model.ImgNam)))
            {

                using (Stream input = imageDataStream)
                {
                  
                        await imageDataStream.CopyToAsync(output);                    
                }
            }

            return Ok();
        }

     
        private string GetPathAndFilename(string filename )
        {
            string path = this._hostingEnvironment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;
        }
        //public IFormFile ReturnFormFile(FileStreamResult result)
        //{
        //    var ms = new MemoryStream();
        //    string path = this._hostingEnvironment.WebRootPath + "\\uploads\\";

        //    try
        //    {
        //        result.FileStream.CopyTo(ms);
        //        return new FormFile(ms, 0, ms.Length, "hh", path);
        //    }
        //    finally
        //    {
        //        ms.Dispose();
        //    }
        //}
    }

}

    


