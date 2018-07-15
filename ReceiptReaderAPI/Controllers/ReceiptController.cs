using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ReceiptReaderAPI.Models;
using ReceiptReaderAPI.Services;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReceiptReaderAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Receipt")]
    public class ReceiptController : Controller
    {        
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<Expense> ReadExpense()
        {
            // Request body. Posts a locally stored JPEG image.
            string extractedImageData = ReadCameraData(Request.Body);
            var byteData = Convert.FromBase64String(extractedImageData);
            var jsonResponse = await CognitiveServicesOCR.MakeOCRRequest(byteData);

            var linesOfText = ExtractLines.ProcessOCR(jsonResponse);

            return new Expense
            {
                amount = 28.00M,
                date = DateTime.Now
            };
        }

        private string ReadCameraData(Stream input)
        {
            var cameraData = ReadAsBytes(input);
            var dataUrl = System.Text.Encoding.Default.GetString(cameraData);
            var imageData = Regex.Match(dataUrl, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            return imageData;
        }
        private static byte[] ReadAsBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
