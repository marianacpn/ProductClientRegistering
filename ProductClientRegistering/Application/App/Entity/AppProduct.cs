using Application.App.Interface;
using Application.ViewModel.Product;
using AutoMapper;
using Domain.Entity;
using Domain.Repository.Interface.Entity;
using System;
using System.Collections.Generic;

namespace Application.App.Entity
{
    public class AppProduct : Core.AppBase, Interface.IAppProduct
    {
        private readonly IProductRepository _productRepository;
        private readonly IClientValidation _clientValidation; 
        public AppProduct(IProductRepository productRepository,
            IClientValidation clientValidation,
            IMapper mapper) : base(mapper)
        {
            _productRepository = productRepository;
            _clientValidation = clientValidation;
        }

        public void CreateProduct(ProductCreateVM productVM)
        {
            Product product = new Product(productVM.Nome, double.Parse(productVM.Valor), true, productVM.ClientId);

            _productRepository.Add(product);

            _productRepository.SaveChanges();

            _clientValidation.ValidateClientStatus(productVM.ClientId);
        }

        public void DeleteProduct(int productId)
        {
            Product product = _productRepository.GetProductByIdWithTracking(productId);

            product.ChangeDisponivel(false);

            _productRepository.SaveChanges();

            _clientValidation.ValidateClientStatus(product.ClientId);
        }

        public IEnumerable<ProductListVM> GetAvailableProducts() => _mapper.Map<IEnumerable<ProductListVM>>(_productRepository.GetAvailableProducts());
    }
}
