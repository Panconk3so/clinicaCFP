﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using clinicaCFP.Models;

namespace clinicaCFP.Data;


public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<RegistrarCita> DataRegistrarCita {get;set;}
    public DbSet<Pago> DataPago {get;set;}
}
