using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
        //private readonly IUnitOfWork _unitOfWork = unitOfWork; 

        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters specParams)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(specParams); 
            //Get All Products Throught ProductRepository
            var product = await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);

            var specCount = new ProductWithCountSpecifications(specParams);

            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount);

            //Mapping <IEnumerable<Product>> to <IEnumerable<ProductResultDto>>
            var result =  mapper.Map<IEnumerable<ProductResultDto>>(product);
            return new PaginationResponse<ProductResultDto>(specParams.PageIndex, specParams.PageSize,count,result);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);

            var product = await unitOfWork.GetRepository<Product,int>().GetAsync(spec);
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
