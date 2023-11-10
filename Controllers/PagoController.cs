using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using clinicaCFP.Data;
using clinicaCFP.Models;
using Microsoft.AspNetCore.Identity;

namespace clinicaCFP.Controllers

   {
    public class PagoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ServicioPago _servicioPago;

        public PagoController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ServicioPago servicioPago)
        {
            _context = context;
            _userManager = userManager;
            _servicioPago = servicioPago;
        }
        public IActionResult PagarCita(int citaId)
        {
            var cita = _context.DataRegistrarCita.Find(citaId);

            if (cita == null)
            {
                return NotFound();
            }

            // Aquí deberías pasar la información de la cita a la vista de pago
            return View(cita);
        }

        [HttpPost]
       public IActionResult ProcesarPago(int citaId)
        {
            var cita = _context.DataRegistrarCita.Find(citaId);

            if (cita == null)
            {
                return NotFound();
            }

            // Lógica de procesamiento de pago
            bool pagoExitoso = _servicioPago.ProcesarPago(cita.Precio);

            if (pagoExitoso)
            {
                // Registra el pago en la base de datos o realiza otras acciones necesarias
                cita.PagoRealizado = true;
                _context.SaveChanges();

                TempData["Message"] = "Pago exitoso";
                return RedirectToAction("ConfirmarPago");
            }
            else
            {
                TempData["Message"] = "Error durante el procesamiento del pago";
                return RedirectToAction("ErrorPago");
            }
        }

        public IActionResult ConfirmarPago()
        {
            return View();
        }

        public IActionResult ErrorPago()
        {
            return View();
        }
    }
}