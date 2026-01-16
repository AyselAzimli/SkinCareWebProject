using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.Repositories.Contracts;
using ECommerce.BLL.Constants;

namespace ECommerce.BLL.Services
{
    public class ProductManager : CrudManager<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly FileService _fileService;

        public ProductManager(IRepository<Product> respository, IMapper mapper, ICategoryService categoryService, IBrandService brandService, FileService fileService) : base(respository, mapper)
        {
            _categoryService = categoryService;
            _brandService = brandService;
            _fileService = fileService;
        }

        // Override GetByIdAsync to include related entities
        public override async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var product = await Repository.GetAsync(
                predicate: p => p.Id == id,
                include: source => source
                    .Include(p => p.Category!)
                    .Include(p => p.Brand!)
                    .Include(p => p.Images!)
                    .Include(p => p.Variants!));

            if (product == null)
                return null!;

            return Mapper.Map<ProductViewModel>(product);
        }



        public async Task<CreateProductViewModel> GetCreateViewModelAsync()
        {
            var createProductViewModel = new CreateProductViewModel();
            createProductViewModel.CategorySelectListItems = await _categoryService.GetCategorySelectListItemsAsync();
            createProductViewModel.BrandSelectListItems = await _brandService.GetBrandSelectListItemsAsync();
            return createProductViewModel;
        }

        public async Task<UpdateProductViewModel> GetUpdateViewModelAsync(int id)
        {
            var product = await Repository.GetAsync(
                predicate: p => p.Id == id,
                include: source => source
                    .Include(p => p.Category!)
                    .Include(p => p.Brand!)
                    .Include(p => p.Images!)
                    .Include(p => p.Variants!));

            if (product == null)
                return null!;

            var updateProductViewModel = Mapper.Map<UpdateProductViewModel>(product);
            updateProductViewModel.CategorySelectListItems = await _categoryService.GetCategorySelectListItemsAsync();
            updateProductViewModel.BrandSelectListItems = await _brandService.GetBrandSelectListItemsAsync();

            return updateProductViewModel;
        }

        public override async Task CreateAsync(CreateProductViewModel model)
        {
            var product = Mapper.Map<Product>(model);

            if (model.CoverImageFile != null)
            {
                if (!_fileService.IsImageFile(model.CoverImageFile))
                    throw new ArgumentException("The file is not a valid image.", nameof(model.CoverImageFile));

                product.CoverImageName = await _fileService.GenerateFile(model.CoverImageFile, FilePathConstants.ProductImagePath);
            }

            if (model.ImageFiles != null && model.ImageFiles.Any())
            {
                product.Images = new List<ProductImage>();
                foreach (var imageFile in model.ImageFiles)
                {
                    if (!_fileService.IsImageFile(imageFile))
                        throw new ArgumentException("One of the files is not a valid image.", nameof(model.ImageFiles));
                }

                foreach (var imageFile in model.ImageFiles)
                {
                    var imageName = await _fileService.GenerateFile(imageFile, FilePathConstants.ProductImagePath);
                    product.Images.Add(new ProductImage
                    {
                        ImageName = imageName
                    });
                }
            }

            await Repository.CreateAsync(product);
        }

        public override async Task<bool> UpdateAsync(int id, UpdateProductViewModel model)
        {
            var existingProduct = await Repository.GetAsync(
                predicate: p => p.Id == id,
                include: source => source.Include(p => p.Images!));

            if (existingProduct == null)
                return false;

            existingProduct = Mapper.Map(model, existingProduct);

            if (model.CoverImageFile != null)
            {
                if (!_fileService.IsImageFile(model.CoverImageFile))
                    throw new ArgumentException("The file is not a valid image.", nameof(model.CoverImageFile));

                var oldCoverImageName = existingProduct.CoverImageName;
                existingProduct.CoverImageName = await _fileService.GenerateFile(model.CoverImageFile, FilePathConstants.ProductImagePath);

                if (!string.IsNullOrEmpty(oldCoverImageName))
                {
                    var oldFilePath = Path.Combine(FilePathConstants.ProductImagePath, oldCoverImageName);
                    if (File.Exists(oldFilePath))
                        File.Delete(oldFilePath);
                }
            }

            if (model.ImageFiles != null && model.ImageFiles.Any())
            {
                existingProduct.Images = new List<ProductImage>();
                foreach (var imageFile in model.ImageFiles)
                {
                    if (!_fileService.IsImageFile(imageFile))
                        throw new ArgumentException("One of the files is not a valid image.", nameof(model.ImageFiles));
                }

                foreach (var imageFile in model.ImageFiles)
                {
                    var imageName = await _fileService.GenerateFile(imageFile, FilePathConstants.ProductImagePath);
                    existingProduct.Images.Add(new ProductImage
                    {
                        ImageName = imageName
                    });
                }
            }

            await Repository.UpdateAsync(existingProduct);

            return true;
        }

        //public Task<List<ProductViewModel>> GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}