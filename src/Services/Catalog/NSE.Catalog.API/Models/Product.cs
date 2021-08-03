using NSE.Core.DomainObjects;

namespace NSE.Catalog.API.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public string PictureUrl { get; set; }
    }
}
