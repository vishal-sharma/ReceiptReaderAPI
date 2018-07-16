using Newtonsoft.Json;
using ReceiptReaderAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReceiptReaderAPI.Services
{
    public class ExtractLinesService
    {
        public static List<string> ProcessOCR(string json)
        {
            var data = JsonConvert.DeserializeObject<ImageAsOcrText>(json);

            return data.regions
                .SelectMany(r => r.lines)
                .Select(l => string.Join(" ", l.words.Select(w => w.text)))
                .ToList();
        }

        public static string ProcessOCRAsSingleLine(string json)
        {
            var data = JsonConvert.DeserializeObject<ImageAsOcrText>(json);

            return string.Join(" ", data.regions
                .SelectMany(r => r.lines)
                .Select(l => string.Join(" ", l.words.Select(w => w.text)))
                .ToList());
        }
    }
}
