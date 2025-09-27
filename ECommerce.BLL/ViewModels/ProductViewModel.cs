namespace ECommerce.BLL.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverImageName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;
        public string BrandName { get; set; } = null!;

        public List<ProductImageViewModel> Images { get; set; } = new();
        public List<ProductVariantViewModel> Variants { get; set; } = new();
    }

    public class CreateProductViewModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        //public IFormFile CoverImageFile { get; set; } = null!;

        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        //public List<IFormFile> ImageFiles { get; set; } = new();
        public List<CreateProductVariantViewModel> Variants { get; set; } = new();
    }

    public class UpdateProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        //public IFormFile? CoverImageFile { get; set; }
        public string? CoverImageName { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        //public List<IFormFile> ImageFiles { get; set; } = new();
        public List<ProductImageViewModel> Images { get; set; } = new();

        public List<UpdateProductVariantViewModel> Variants { get; set; } = new();
    }

}
