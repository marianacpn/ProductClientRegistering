using Application.App.Interface;
using Application.ViewModel.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IAppClient _appClient;
        public ClientController(IAppClient appClient)
        {
            _appClient = appClient;
        }

        public IActionResult Index()
        {
            try
            {
                var clientsVM = new ClientListVM()
                {
                    Clients = _appClient.GetAllClients()
                };

                return View(clientsVM);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Client").WithDangerMessage(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View(new ClientCreateVM());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Client").WithDangerMessage(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientCreateVM clientVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Create", "Client").WithDangerMessage("Ocorreu um erro ao criar cliente");

                _appClient.CreateClient(clientVM);

                return RedirectToAction("Index", "Client").WithSuccessMessage("Cliente criado com sucesso!");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Client").WithDangerMessage(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(ClientDeleteVM clientDeleteVM)
        {
            try
            {
                return View(clientDeleteVM);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Client").WithDangerMessage(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(ClientDeleteVM clientDeleteVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Index", "Client").WithDangerMessage("Ocorreu um erro ao deletar cliente");

                _appClient.DeleteClientById(clientDeleteVM.ClientId);

                return RedirectToAction("Index","Client").WithSuccessMessage("Cliente deletado com sucesso");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Client").WithDangerMessage(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Details(int clientId)
        {
            try
            {
                ClientDetailsVM clientVM = _appClient.GetClientDetailsById(clientId);

                return View(clientVM);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Client").WithDangerMessage(ex.Message);
            }
        }
    }
}
