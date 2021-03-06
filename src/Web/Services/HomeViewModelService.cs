using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Constants;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Brand> _brandRepo;
        private readonly IRepository<Category> _categoryRepo;

        public HomeViewModelService(IRepository<Product> productRepo, IRepository<Brand> brandRepo, IRepository<Category> categoryRepo)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
        }
        public async Task<HomeViewModel> GetHomeViewModelAsync(int? brandId, int? categoryId, int pageId)
        {
            int skip = (pageId - 1) * Constant.ITEMS_PER_PAGE;
            int take = Constant.ITEMS_PER_PAGE;
            var specProducts = new ProductsFilterSpecification(brandId, categoryId, skip, take);
            var specAllProducts = new ProductsFilterSpecification(brandId, categoryId);
            var allProductsCount = await _productRepo.CountAsync(specAllProducts);
            var products = await _productRepo.GetAllAsync(specProducts);

            var viewModel = new HomeViewModel()
            {
                Products = products.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                    PictureUri = x.PictureUri,
                    Price = x.Price
                }).ToList(),
                Brands = await GetBrandsAsync(),
                Categories = await GetCategoriesAsync(),
                BrandId = brandId,
                CategoryId = categoryId,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    CurrentPage = pageId,
                    TotalItems = allProductsCount,
                    ItemsOnPage = products.Count
                }
            };

            return viewModel;
        }

        private async Task<List<SelectListItem>> GetCategoriesAsync()
        {
            var list = (await _categoryRepo.GetAllAsync()).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem("All", ""));

            return list;
        }

        private async Task<List<SelectListItem>> GetBrandsAsync()
        {
            var list = (await _brandRepo.GetAllAsync()).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem("All", ""));

            return list;
        }
    }
}
