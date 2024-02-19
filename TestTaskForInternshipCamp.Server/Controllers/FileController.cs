
using learningBlob;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace TestTaskForInternshipCamp.Server.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class FilesController : ControllerBase
        {
            private readonly BlobStorageService _fileService;

            public FilesController(BlobStorageService fileService)
            {
                _fileService = fileService;
            }                  

            [HttpPost]
            public virtual async Task<IActionResult> Upload(FileUploadModel model)
            {
                var result = await _fileService.UpLoadAsync(model);
                return Ok(result);
            }           
        }
    }


