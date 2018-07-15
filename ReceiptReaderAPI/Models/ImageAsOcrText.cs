using System.Collections.Generic;

namespace ReceiptReaderAPI.Models
{
    public class ImageAsOcrText
    {
        public string language { get; set; }
        public string orientation { get; set; }

        public List<Region> regions { get; set; }

    }

    public class Region
    {
        public List<Line> lines { get; set; }
    }

    public class Line
    {
        public List<Word> words { get; set; }
    }

    public class Word
    {
        public string text { get; set; }
    }
}
