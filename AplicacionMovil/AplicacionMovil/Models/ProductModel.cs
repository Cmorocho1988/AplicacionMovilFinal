using System;
using System.Collections.Generic;
using System.Text;

namespace CristianMorocho.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public CategoryModel category { get; set; } 
    }

    public class CategoryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
