using AutoMapper;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.Repositories.Contracts;


namespace ECommerce.BLL.Services
{
    public class ProductManager : CrudManager<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>, IProductService
    {
        private readonly ICategoryService _categoryService;
        //private readonly FileService _fileService;

        public ProductManager(IRepository<Product> respository, IMapper mapper, ICategoryService categoryService/* FileService fileService*/) : base(respository, mapper)
        {
            _categoryService = categoryService;
            //_fileService = fileService;
        }

        public Task<CreateProductViewModel> GetCreateViewModelAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UpdateProductViewModel> GetUpdateViewModelAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<CreateProductViewModel> GetCreateViewModelAsync()
        //{
        //    var createProductViewModel = new CreateProductViewModel();

        //    createProductViewModel.CategorySelectListItems = await _categoryService.GetCategorySelectListItemsAsync();
        //    createProductViewModel.TagSelectListItems = await _tagService.GetTagSelectListItemsAsync();

        //    return createProductViewModel;
        //}

        //public async Task<UpdateProductViewModel> GetUpdateViewModelAsync(int id)
        //{
        //    var product = await Repository.GetAsync(
        //        predicate: p => p.Id == id,
        //        include: source => source
        //            .Include(p => p.Images!)
        //            .Include(p => p.ProductTags!)
        //            .ThenInclude(pt => pt.Tag!));

        //    if (product == null)
        //        return null!;

        //    var updateProductViewModel = Mapper.Map<UpdateProductViewModel>(product);
        //    updateProductViewModel.CategorySelectListItems = await _categoryService.GetCategorySelectListItemsAsync();

        //    var tagSelectListItems = await _tagService.GetTagSelectListItemsAsync();
        //    var newTagSelectListItems = new List<SelectListItem>();
        //    if (tagSelectListItems != null && tagSelectListItems.Any())
        //    {
        //        foreach (var tagSelectListItem in tagSelectListItems)
        //        {
        //            if (!product.ProductTags.Any(pt => pt.TagId == int.Parse(tagSelectListItem.Value)))
        //            {
        //                newTagSelectListItems.Add(tagSelectListItem);
        //            }
        //        }
        //    }
        //    updateProductViewModel.TagSelectListItems = newTagSelectListItems;

        //    return updateProductViewModel;
        //}

        //public override async Task CreateAsync(CreateProductViewModel model)
        //{
        //    var product = Mapper.Map<Product>(model);

        //    if (model.TagIds != null && model.TagIds.Any())
        //    {
        //        product.ProductTags = model.TagIds.Select(tagId => new ProductTag
        //        {
        //            TagId = tagId
        //        }).ToList();
        //    }

        //    if (model.CoverImageFile != null)
        //    {
        //        if (!_fileService.IsImageFile(model.CoverImageFile))
        //            throw new ArgumentException("The file is not a valid image.", nameof(model.CoverImageFile));

        //        product.CoverImageName = await _fileService.GenerateFile(model.CoverImageFile, FilePathConstants.ProductImagePath);
        //    }

        //    if (model.ImageFiles != null && model.ImageFiles.Any())
        //    {
        //        product.Images = new List<ProductImage>();
        //        foreach (var imageFile in model.ImageFiles)
        //        {
        //            if (!_fileService.IsImageFile(imageFile))
        //                throw new ArgumentException("One of the files is not a valid image.", nameof(model.ImageFiles));
        //        }

        //        foreach (var imageFile in model.ImageFiles)
        //        {
        //            var imageName = await _fileService.GenerateFile(imageFile, FilePathConstants.ProductImagePath);
        //            product.Images.Add(new ProductImage
        //            {
        //                ImageName = imageName
        //            });
        //        }
        //    }

        //    await Repository.CreateAsync(product);
        //}

        //public override async Task<bool> UpdateAsync(int id, UpdateProductViewModel model)
        //{
        //    var existingProduct = await Repository.GetByIdAsync(id);

        //    if (existingProduct == null)
        //        return false;

        //    //exception occurs here
        //    //existingProduct = Mapper.Map<Product>(model);
        //    existingProduct = Mapper.Map(model, existingProduct);

        //    if (model.TagIds != null && model.TagIds.Any())
        //    {
        //        existingProduct.ProductTags = model.TagIds.Select(tagId => new ProductTag
        //        {
        //            TagId = tagId
        //        }).ToList();
        //    }

        //    if (model.CoverImageFile != null)
        //    {
        //        if (!_fileService.IsImageFile(model.CoverImageFile))
        //            throw new ArgumentException("The file is not a valid image.", nameof(model.CoverImageFile));

        //        var oldCoverImageName = existingProduct.CoverImageName;
        //        existingProduct.CoverImageName = await _fileService.GenerateFile(model.CoverImageFile, FilePathConstants.ProductImagePath);

        //        if (!string.IsNullOrEmpty(oldCoverImageName))
        //        {
        //            var oldFilePath = Path.Combine(FilePathConstants.ProductImagePath, oldCoverImageName);
        //            if (File.Exists(oldFilePath))
        //                File.Delete(oldFilePath);
        //        }
        //    }

        //    if (model.ImageFiles != null && model.ImageFiles.Any())
        //    {
        //        existingProduct.Images = new List<ProductImage>();
        //        foreach (var imageFile in model.ImageFiles)
        //        {
        //            if (!_fileService.IsImageFile(imageFile))
        //                throw new ArgumentException("One of the files is not a valid image.", nameof(model.ImageFiles));
        //        }

        //        foreach (var imageFile in model.ImageFiles)
        //        {
        //            var imageName = await _fileService.GenerateFile(imageFile, FilePathConstants.ProductImagePath);
        //            existingProduct.Images.Add(new ProductImage
        //            {
        //                ImageName = imageName
        //            });
        //        }
        //    }

        //    await Repository.UpdateAsync(existingProduct);

        //    return true;
        //}
    }
}
