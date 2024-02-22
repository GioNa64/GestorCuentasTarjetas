using GestorCuentasTarjetas.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;


public class DbContexto : DbContext
{
    public DbContexto(DbContextOptions<DbContexto> options) : base(options)
    {
    }
        public DbSet<Tarjeta> TarjetasCredito { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        [NotMapped]
        public DbSet<Transaccion> Transacciones { get; set; }
        [NotMapped]
        public DbSet<EstadoCuentaDetallado> EstadodeCuenta { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaccion>().HasNoKey();
        modelBuilder.Entity<EstadoCuentaDetallado>().HasNoKey();
      
      

        base.OnModelCreating(modelBuilder);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {

    }
}


