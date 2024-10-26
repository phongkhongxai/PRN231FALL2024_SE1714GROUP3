using BusinessObjects.DTO;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using RazorLight;
using Services;
using Services.Impl;
using System;
using System.Net.Mime;
using System.Reflection;
using System.Security.Claims;


namespace Recuitment_Group3.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class ResumesController : ODataController
    {
        private readonly IConverter _pdfConverter;
        private readonly string _pdfSavePath;
        private readonly IRazorLightEngine _razorEngine;
        private readonly IResumeService _resumeService;
        private readonly IUserService _userService;
        public ResumesController(IConverter pdfConverter, IWebHostEnvironment environment, IResumeService resumeService, IUserService userService)
        {
            _userService = userService;
            _resumeService = resumeService;
            _pdfConverter = pdfConverter;
            _pdfSavePath = Path.Combine(environment.ContentRootPath, "PDFs");
            // Initialize RazorLight engine with the correct path
            _razorEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(environment.ContentRootPath, "Views"))
                .UseMemoryCachingProvider()
                .Build();
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ResponseResumeDTO>>> GetAllResumes()
        {
            var resumes = await _resumeService.GetAllResumesAsync();
            return Ok(resumes);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<ResponseResumeDTO>> GetResumeById([FromRoute] long id)
        {
            var resumes = await _resumeService.GetResumeByIdAsync(id);
            if (resumes == null)
            {
                return NotFound();
            }
            return Ok(resumes);
        }

        [HttpGet("user/{idUser}")]
        [EnableQuery]
        public async Task<ActionResult<ResponseResumeDTO>> GetResumeByUser([FromRoute] long idUser)
        {
            var resumes = await _resumeService.GetAllResumesByUserAsync(idUser);
            if (resumes == null)
            {
                return NotFound();
            }
            return Ok(resumes);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResume([FromRoute] long id)
        {
            var success = await _resumeService.DeleteResumeAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        //Hàm này dùng để tải file pdf --- test trên web browser
        [HttpGet("pdf/{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPdfFile(string fileName)
        {
            try
            {
                // Construct the full file path
                var filePath = Path.Combine(_pdfSavePath, fileName);

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                // Read the PDF file as byte array
                var pdfBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                // Return the PDF file as a FileContentResult
                return File(pdfBytes, MediaTypeNames.Application.Pdf, fileName);
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("")]
        [Authorize(Roles = "1,3")]
        public async Task<ActionResult<ResponseResumeDTO>> CreateCV([FromBody] ResumeDTO resume)
        {
            try
            {
                if (resume == null)
                {
                    return BadRequest("Invalid Resume data.");
                }
                string? email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (email == null)
                {
                    throw new Exception("Không tìm thấy người dùng");
                }
                var user = await _userService.FindByEmail(email);

                // 1. Render the Razor HTML template using the provided ResumeDTO data
                var htmlContent = await RenderHtmlFromTemplateAsync(resume);
                // 2. Convert HTML to PDF using DinkToPdf
                var pdfFile = ConvertHtmlToPdf(htmlContent);
                // 3. Generate a unique filename
                string fileName = $"Resume_{Guid.NewGuid().ToString("N")}.pdf";

                // 4. Ensure the directory exists
                Directory.CreateDirectory(_pdfSavePath);

                // 5. Save the PDF file
                string filePath = Path.Combine(_pdfSavePath, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, pdfFile);

                // 6. Return the relative file path and filename
                var relativePath = Path.Combine("PDFs", fileName);

                var savedResume = await _resumeService.CreateResumeAsync(fileName, user.Id, resume);

                return Ok(savedResume);
            }
            catch (IOException ex) when (ex.Message == "resume creation failed.")
            {

                return StatusCode(400, ex.Message);
            }
            catch (IOException ex)
            {

                return StatusCode(500, "An error occurred while processing your request");
            }
            catch (Exception ex) when (ex.Message == "Không tìm thấy người dùng")
            {

                return StatusCode(400, ex.Message);
            }
        }

        private async Task<string> RenderHtmlFromTemplateAsync(ResumeDTO resume)
        {
            // Assuming your template is named "ResumeTemplate.cshtml" and is an embedded resource
            string templateKey = "ResumeTemplate.cshtml";
            string result = await _razorEngine.CompileRenderAsync(templateKey, resume);
            return result;
        }

        private byte[] ConvertHtmlToPdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                    }
                }
            };

            // Convert to PDF using DinkToPdf
            var pdf = _pdfConverter.Convert(doc);
            return pdf;
        }
    



}
}
