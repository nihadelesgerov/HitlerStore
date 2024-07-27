using System.ComponentModel.DataAnnotations;

namespace HitlerStore.Models
{
    public class Product
    {

        public int Id { get; set; }
        public long SpeacialId { get; set; }
        public string ProductName { get; set; }  = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public string ProductBio {  get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public string ProductImageName { get; set; } = string.Empty;
        public DateTime ProductUploadedAt { get; set; }
    }
}
