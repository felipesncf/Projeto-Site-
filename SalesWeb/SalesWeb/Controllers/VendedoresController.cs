using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = _vendedorService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.FindAllAsync();
            var viewModel = new VendedorFormsViewModel { Departamentos = departamentos };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return View(vendedor);
            }
            await _vendedorService.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não foi fornecido" });
            }
            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vendedorService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não foi fornecido" });
            }
            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não encontrado" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não foi fornecido" }); ;
            }
            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não encontrado" });
            }

            List<Departamento> departamentos = await _departamentoService.FindAllAsync();
            VendedorFormsViewModel viewModel = new VendedorFormsViewModel { Vendedor = obj, Departamentos = departamentos };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormsViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não corresponde" });
            }
            try
            {
                await _vendedorService.UpdateAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { mensagem = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { mensagem = e.Message });
            }
        }

        public IActionResult Error(string mensagem)
        {
            var viewModel = new ErrorViewModel
            {
                Mensagem = mensagem,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}