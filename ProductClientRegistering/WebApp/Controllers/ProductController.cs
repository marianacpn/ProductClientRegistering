using Application.App.Interface;
using Application.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAppProduct _appProduct;
        private readonly IAppClient _appClient;

        public ProductController(IAppProduct appProduct,
            IAppClient appClient)
        {
            _appProduct = appProduct;
            _appClient = appClient;
        }
        public IActionResult Index()
        {
            try
            {
                var productList = new ProductListVM()
                {
                    Products = _appProduct.GetAvailableProducts()
                };

                return View(productList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home").WithDangerMessage(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var productVM = new ProductCreateVM()
                {
                    Clients = _appClient.GetAllActiveClients()
                };
                return View(productVM);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home").WithDangerMessage(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateVM productVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(productVM).WithDangerMessage("Ocorreu um erro ao criar produto");

                _appProduct.CreateProduct(productVM);

                return RedirectToAction("Index", "Product").WithSuccessMessage("Produto criado com sucesso");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home").WithDangerMessage(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(ProductDeleteVM productVM)
        {
            try
            {
                return View(productVM);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home").WithDangerMessage(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int productId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Index", "Product").WithDangerMessage("Ocorreu um erro ao deletar produto");

                _appProduct.DeleteProduct(productId);

                return RedirectToAction("Index", "Product").WithSuccessMessage("Produto criado com sucesso");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home").WithDangerMessage(ex.Message);
            }
        }
    }
}
