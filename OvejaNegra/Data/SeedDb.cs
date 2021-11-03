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
            await CheckPedidosAsync();
            await CheckComandaAsync();
            await CheckInsumoAsync();
            await CheckCompraAsync();
            await CheckCajaAsync();
        }

        private async Task CheckCajaAsync()
        {
            if (!_context.Caja.Any())
            {
                _context.Caja.Add(new Caja
                {
                    Fecha = DateTimeOffset.Now,
                    diezc = 1,
                    veintec = 1,
                    cincuentac = 1,
                    unb = 1,
                    dosb = 1,
                    cincob = 1,
                    diezb = 1,
                    veinteb = 1,
                    cincuentab = 1,
                    cienb = 1,
                    doscientosb = 1
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCompraAsync()
        {
            var insumo = _context.Insumos.FirstOrDefault();
            if (!_context.Compras.Any())
            {
                _context.Compras.Add(new Compra
                {
                    Fecha = DateTimeOffset.Now,
                    Insumo=insumo,
                    Total=15
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckInsumoAsync()
        {
            if (!_context.Insumos.Any())
            {
                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Tomate",
                    Categoria = "Hamburguesa"
                });



                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProductosAsync()
        {
            if (!_context.Productos.Any())
            {
                _context.Productos.Add(new Producto
                {
                    Nombre = "Gri CS",
                    PrecioLocal = 19,
                    PrecioDelivery = 22,
                    Carne=1,
                    Papa=1,
                    Bono=0.5,
                    Categoria = "Hamburguesa"
                });

                _context.Productos.Add(new Producto
                {
                    Nombre = "Bbq CD",
                    PrecioLocal = 36,
                    PrecioDelivery = 39,
                    Carne=2,
                    Papa=1,
                    Bono=0.5,
                    Categoria = "Hamburguesa"
                });;

                _context.Productos.Add(new Producto
                {
                    Nombre = "Gau SS",
                    PrecioLocal = 22,
                    PrecioDelivery = 25,
                    Carne=1,
                    Papa=0,
                    Bono=0.25,
                    Categoria = "Hamburguesa"
                });

                _context.Productos.Add(new Producto
                {
                    Nombre = "Papa",
                    PrecioLocal = 9,
                    PrecioDelivery = 12,
                    Carne=0,
                    Papa=1,
                    Bono=0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "Guarana",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne=0,
                    Papa=0,
                    Bono=0,
                    Categoria = "Soda"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "Carne",
                    PrecioLocal = 10,
                    PrecioDelivery = 10,
                    Carne=1,
                    Papa=1,
                    Bono=0,
                    Categoria = "Hamburguesa"
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
                    Mesa = "mesa 1",
                    Fecha = DateTimeOffset.Now,
                    Pago = true,
                    Cerrado=false,
                    Delivery=true,
                    Preparando=false

                });



                await _context.SaveChangesAsync();
            }
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
                    Total=38
                });



                await _context.SaveChangesAsync();
            }
        }



    }
}
