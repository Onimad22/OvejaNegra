using OvejaNegra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckProductosAsync();
            await CheckExtrasAsync();
            await CheckPedidosAsync();
            await CheckComandaAsync();
        }

        private async Task CheckComandaAsync()
        {
            var pedido = _context.Pedidos.FirstOrDefault();
            var producto = _context.Productos.FirstOrDefault();
            if (!_context.Comandas.Any())
            {
                _context.Comandas.Add(new Comanda
                {
                    Pedido=pedido,
                    Cantidad=1,
                    Producto=producto,
                    Carne=2,
                    Queso=1,
                    Papa=1,
                    Total=38
                });



                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPedidosAsync()
        {
            
            if (!_context.Pedidos.Any())
            {
                _context.Pedidos.Add(new Pedido
                {
                    Pago=true,
                    Mesa = "mesa 1",
                    Fecha=DateTime.Today,
                    Hora=DateTime.Now,
                    
                });

              

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckExtrasAsync()
        {
            if (!_context.Extras.Any())
            {
                _context.Extras.Add(new Extra
                {
                    Nombre = "Carne",
                    Precio = 10
                });

                _context.Extras.Add(new Extra
                {
                    Nombre = "Queso",
                    Precio = 3
                });

                _context.Extras.Add(new Extra
                {
                    Nombre = "Papa",
                    Precio = 9
                });
                _context.Extras.Add(new Extra
                {
                    Nombre = "Bono",
                    Precio = 2.5
                });

                await _context.SaveChangesAsync();

            }
            
        }

        private async Task CheckProductosAsync()
        {
            if (!_context.Productos.Any())
            {
                _context.Productos.Add(new Producto { 
                    Nombre = "Gringa", 
                    PrecioLocal = 19, 
                    PrecioDelivery = 22,
                Categoria="Hamburguesa"});

                _context.Productos.Add(new Producto
                {
                    Nombre = "Gaucha",
                    PrecioLocal = 19,
                    PrecioDelivery = 22,
                Categoria = "Hamburguesa"
                });

                _context.Productos.Add(new Producto
                {
                    Nombre = "BBQ",
                    PrecioLocal = 19,
                    PrecioDelivery = 22,
                Categoria = "Hamburguesa"
                });

                _context.Productos.Add(new Producto
                {
                    Nombre = "Fort",
                    PrecioLocal = 19,
                    PrecioDelivery = 22,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "Coca-cola",
                    PrecioLocal = 19,
                    PrecioDelivery = 22,
                    Categoria = "Soda"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "Lager",
                    PrecioLocal = 19,
                    PrecioDelivery = 22,
                    Categoria = "Cerveza"
                });

                await _context.SaveChangesAsync();
            }
            
        }
    }
}
