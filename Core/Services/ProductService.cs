using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
        //private readonly IUnitOfWork _unitOfWork = unitOfWork; 

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            //Get All Products Throught ProductRepository
            var product = await unitOfWork.GetRepository<Product,int>().GetAllAsync();

            //Mapping <IEnumerable<Product>> to <IEnumerable<ProductResultDto>>
            var result =  mapper.Map<IEnumerable<ProductResultDto>>(product);
            return result;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product,int>().GetAsync(id);
            if (product == null) return null;

            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brand = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brand);
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var type = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(type);
            return result;
        }        
    }
}
