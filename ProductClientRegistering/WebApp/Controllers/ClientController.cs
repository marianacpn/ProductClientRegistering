using Application.App.Interface;
using Application.ViewModel.Client;
using Microsoft.AspNetCore.Mvc;
using System;

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
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
