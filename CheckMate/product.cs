using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate
{
    internal class Product
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> Tags { get; set; } = new List<string>();
        public string Category { get; set; } = "";
        public List<string> Images { get; set; } = new List<string>();
        public List<Variant> Variants { get; set; } = new List<Variant>();

        public class Variant
        {
            public string Color { get; set; } = ""; // Color of the variant
            public string Size { get; set; } = "";  // Size of the variant
            public decimal Price { get; set; }      // Price of the variant
        }
    }
}
