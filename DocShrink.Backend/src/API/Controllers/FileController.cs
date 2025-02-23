using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/files")]
[ApiVersion("1.0")]
public class FileController : ControllerBase
{
    private readonly IFileProcessingService fileService;

    public FileController(IFileProcessingService fileService)
    {
        this.fileService = fileService;
    }
    [HttpPost("compress")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("No file was uploaded.");
        }

        await using var fileStream = file.OpenReadStream();

        var compressionResult = await fileService.ProcessFileAsync(
            Path.GetExtension(file.FileName).TrimStart('.'),
            file.FileName,
            fileStream);

        return Ok(compressionResult);
    }
}
