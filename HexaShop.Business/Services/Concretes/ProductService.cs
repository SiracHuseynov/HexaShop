using HexaShop.Business.Exceptions;
using HexaShop.Business.Extensions;
using HexaShop.Business.Services.Abstracts;
using HexaShop.Core.Models;
using HexaShop.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HexaShop.Business.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _env;
        public ProductService(IProductRepository productRepository, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _env = env;
        }

        public async Task AddAsyncProduct(Product product)
        {
            if (product.ImageFile == null)
                throw new ImageFileNotFoundException("Image olmalidir!");

            product.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\products", product.ImageFile);

            await _productRepository.AddAsync(product);
            await _productRepository.CommitAsync();
        }

        public void DeleteProduct(int id)
        {
            var existProduct = _productRepository.Get(x => x.Id == id);

            if (existProduct == null)
                throw new EntityNotFoundException("Product tapilmadi!");

            Helper.DeleteFile(_env.WebRootPath, @"uploads\products", existProduct.ImageUrl);

            _productRepository.Delete(existProduct);
            _productRepository.Commit();
        }

        public List<Product> GetAllProducts(Func<Product, bool>? func = null)
        {
            return _productRepository.GetAll(func);
        }

        public async Task<IPagedList<Product>> GetPaginatedProductAsync(int pageIndex, int pageSize)
        {
            return await _productRepository.GetPaginatedProductsAsync(pageIndex, pageSize);
        }

        public Product GetProduct(Func<Product, bool>? func = null)
        {
            return _productRepository.Get(func);
        }

        public void UpdateProduct(int id, Product newProduct)
        {
            var existProduct = _productRepository.Get(x => x.Id == id);

            if (existProduct == null)
                throw new EntityNotFoundException("Product tapilmadi!");

            if(newProduct.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\products", existProduct.ImageUrl);

                existProduct.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\products", newProduct.ImageFile);
            }

            existProduct.Title = newProduct.Title;
            existProduct.Price = newProduct.Price;

            _productRepository.Commit();

        }
    }
}
