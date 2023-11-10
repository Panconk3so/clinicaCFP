using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using clinicaCFP.Models;
using clinicaCFP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace clinicaCFP.Controllers
{
    public class RegistrarCitaController : Controller
    {
        private readonly ILogger<RegistrarCitaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegistrarCitaController(ILogger<RegistrarCitaController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public IActionResult Index()
{
    if (User.Identity.IsAuthenticated)
    {
        var userId = _userManager.GetUserId(User);
        var citas = _context.DataRegistrarCita.Where(c => c.UserId == userId).ToList();
        return View(citas);
    }
    else
    {
        // El usuario no está autenticado, manejar de acuerdo a tus necesidades
        TempData["Message"] = "Debe iniciar sesión para ver sus citas.";
        return RedirectToAction("Login", "Account"); // Redirigir al inicio de sesión
    }
}

        public IActionResult Cliente()
        {
            var citas = _context.DataRegistrarCita.ToList();
            return View(citas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
public IActionResult Crear(RegistrarCita registrarcita)
{
    if (User.Identity.IsAuthenticated)
    {
        var userId = _userManager.GetUserId(User);
        registrarcita.UserId = userId;
if (!string.IsNullOrEmpty(registrarcita.Especialidad))
{
    decimal precioBase = 0; // Define el precio base
    switch (registrarcita.Especialidad)
    {
        case "Trastorno":
            precioBase = 100; // Precio para Trastorno
            break;
        case "Estres":
            precioBase = 80; // Precio para Estrés
            break;
        case "Terapia":
            precioBase = 120; // Precio para Terapia
            break;
        // Agrega más casos según tus especialidades
    }

    // Asigna el precio calculado
    registrarcita.Precio = precioBase;
}

        if (ModelState.IsValid)
        {
            // Asegura que la fecha de nacimiento esté en formato UTC
            if (registrarcita.FechaNacimiento.Kind != DateTimeKind.Utc)
            {
                registrarcita.FechaNacimiento = registrarcita.FechaNacimiento.ToUniversalTime();
            }

            _context.DataRegistrarCita.Add(registrarcita);
            _context.SaveChanges();
            return RedirectToAction("Crear");
        }
        return View(registrarcita);
    }
    else
    {
        // El usuario no está autenticado, muestra una notificación
        TempData["Message"] = "Debe iniciar sesión para registrar una cita.";
        return RedirectToAction("Crear");
    }

}


  public IActionResult Editar(int id)
    {
        var cita = _context.DataRegistrarCita.Find(id);

        if (cita == null)
        {
            return NotFound();
        }

        return View(cita);
    }

    [HttpPost]
public IActionResult Editar(RegistrarCita registrarcita)
{
    if (ModelState.IsValid)
    {
        try
        {
            // Asegura que la fecha de nacimiento esté en formato UTC
            if (registrarcita.FechaNacimiento.Kind != DateTimeKind.Utc)
            {
                registrarcita.FechaNacimiento = registrarcita.FechaNacimiento.ToUniversalTime();
            }

            // Verifica si la cita existe antes de intentar modificarla
            var existingCita = _context.DataRegistrarCita.Find(registrarcita.Id);

            if (existingCita == null)
            {
                return NotFound();
            }

            // Adjunta la entidad al contexto
            _context.Attach(existingCita);

            // Actualiza solo las propiedades escalares
            _context.Entry(existingCita).CurrentValues.SetValues(registrarcita);

            // Recalcula el precio según la especialidad
            if (!string.IsNullOrEmpty(existingCita.Especialidad))
            {
                decimal precioBase = 0;

                switch (existingCita.Especialidad)
                {
                    case "Trastorno":
                        precioBase = 100;
                        break;
                    case "Estres":
                        precioBase = 80;
                        break;
                    case "Terapia":
                        precioBase = 120;
                        break;
                    // Agrega más casos según tus especialidades
                }

                // Asigna el precio calculado
                existingCita.Precio = precioBase;
            }

            // Guarda los cambios en la base de datos
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        catch (DbUpdateConcurrencyException)
        {
            // Manejo de concurrencia si es necesario
            ModelState.AddModelError("", "Hubo un error al intentar guardar los cambios. Por favor, intenta nuevamente.");
            return View(registrarcita);
        }
    }

    // Si el modelo no es válido, vuelve a la vista con el modelo actual
    return View(registrarcita);
}

    public IActionResult Eliminar(int id)
    {
        var cita = _context.DataRegistrarCita.Find(id);

        if (cita == null)
        {
            return NotFound();
        }

        return View(cita);
    }

    [HttpPost]
    public IActionResult ConfirmarEliminar(int id)
    {
        Console.WriteLine("ConfirmarEliminar Action is being executed.");
        var cita = _context.DataRegistrarCita.Find(id);

        if (cita == null)
        {
            return NotFound();
        }

        _context.DataRegistrarCita.Remove(cita);
        _context.SaveChanges();
        return RedirectToAction("Index");
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
    }}