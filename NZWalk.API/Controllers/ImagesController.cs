using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        //post : /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            // Check validation and update model state 
            this.ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                // User repository to upload image 

            }

            // if Modelstate invalid than return error message 
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            // Check allowed extensions
            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extensions");
            }

            // Size of File
            var bytesOf10MB = 10 * 1024 * 1024;  // covert 10MB into bytes 
            if (request.File.Length > bytesOf10MB)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file");
            }
        }
    }
}
