using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class ProfileViewComponent : ViewComponent
{
    private readonly TicketsContext _context;

    public ProfileViewComponent(TicketsContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var user = HttpContext.User;
        var userName = user.Identity.Name;

        var usuario = _context.Usuario.Include(u => u.Rol).FirstOrDefault(u=>u.NombreUsuario == userName);

        // Determinar qué entidad usar
        if (usuario != null)
        {

            return View(usuario);
        }

        return Content(string.Empty); // Usuario no encontrado
    }
}