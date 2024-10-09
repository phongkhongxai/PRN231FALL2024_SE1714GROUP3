using BusinessObjects.DTO;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorLight;
using System;
using System.Reflection;


namespace Recuitment_Group3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IConverter _pdfConverter;
        private readonly string _pdfSavePath;
        private readonly IRazorLightEngine _razorEngine;
        public ResumeController(IConverter pdfConverter, IWebHostEnvironment environment)
        {
            _pdfConverter = pdfConverter;
            _pdfSavePath = Path.Combine(environment.ContentRootPath, "PDFs");
            // Initialize RazorLight engine with the correct path
            _razorEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(environment.ContentRootPath, "Views"))
                .UseMemoryCachingProvider()
                .Build();
        }

        [HttpPost("generate-pdf")]
        public async Task<IActionResult> GeneratePdf([FromBody] ResumeDTO resume)
        {
            if (resume == null)
            {
                return BadRequest("Invalid Resume data.");
            }
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
            return Ok(new { FilePath = relativePath, FileName = fileName });

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
