using OvejaNegra.Data.Entities;
using OvejaNegra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("Pablo", "Dorado","pablo@ovejanegra.com","Admin");
            var customer = await CheckUserAsync("Mesero", "Mesera","mesero@ovejanegra.com","Customer");
            await CheckManagerAsync(manager);
            await CheckCustomerAsync(customer);
            await CheckProductosAsync();
           // await CheckPedidosAsync();
           // await CheckComandaAsync();
            await CheckInsumoAsync();
           // await CheckCompraAsync();
            //await CheckCajaAsync();
           // await CheckCierreAsync();

        }

        private async Task CheckCustomerAsync(User customer)
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer { User = customer });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User manager)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = manager });
                await _context.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email,string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user==null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    Email = email
                };
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

            }
            return user;
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }



        private async Task CheckCierreAsync()
        {
            if (!_context.Cierre.Any())
            {
                _context.Cierre.Add(new Cierre
                {
                    Fecha = DateTimeOffset.Now,
                    CajaAyer=100,
                    CajaHoy=100,
                    Compras=100,
                    Ventas=100,
                    Balance=100
                });

                await _context.SaveChangesAsync();
            }
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

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Carne",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Pan",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Lechuga",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Pepinillo",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Salsa Golf",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Mayonesa",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Mostaza",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Servilleta",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Papa",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Cebolla",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Aceite",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Sal",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Pimienta",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Paprika",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Leche",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Crema de leche",
                    Categoria = "Hamburguesa"
                });

                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Limon",
                    Categoria = "Hamburguesa"
                });
                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Cilantro",
                    Categoria = "Hamburguesa"
                });
                _context.Insumos.Add(new Insumo
                {
                    Nombre = "Soda",
                    Categoria = "Soda"
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
                    Nombre = "PAPA",
                    PrecioLocal = 9,
                    PrecioDelivery = 9,
                    Carne=0,
                    Papa=1,
                    Bono=0.25,
                    Categoria = "a"
                });

                #region Hamburguesas

                #region gringa
                _context.Productos.Add(new Producto
                {
                    Nombre = "GRI SS",
                    PrecioLocal = 19,
                    PrecioDelivery = 23,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "GRI CS",
                    PrecioLocal = 28,
                    PrecioDelivery = 32,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "GRI SD",
                    PrecioLocal = 29,
                    PrecioDelivery = 33,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "GRI CD",
                    PrecioLocal = 38,
                    PrecioDelivery = 42,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                });
                #endregion
                #region Gaucha
                _context.Productos.Add(new Producto
                {
                    Nombre = "GAU SS",
                    PrecioLocal = 25,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "GAU CS",
                    PrecioLocal = 34,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "GAU SD",
                    PrecioLocal = 35,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "GAU CD",
                    PrecioLocal = 44,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                });

                #endregion
                #region BBQ

                _context.Productos.Add(new Producto
                {
                    Nombre = "BBQ SS",
                    PrecioLocal = 27,
                    PrecioDelivery = 30,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "BBQ CS",
                    PrecioLocal = 36,
                    PrecioDelivery = 39,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "BBQ SD",
                    PrecioLocal = 37,
                    PrecioDelivery = 40,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "BBQ CD",
                    PrecioLocal = 46,
                    PrecioDelivery = 49,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                });
                #endregion
                #region Fort
                _context.Productos.Add(new Producto
                {
                    Nombre = "FORT SS",
                    PrecioLocal = 27,
                    PrecioDelivery = 30,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "FORT CS",
                    PrecioLocal = 36,
                    PrecioDelivery = 39,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "FORT SD",
                    PrecioLocal = 37,
                    PrecioDelivery = 40,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "FORT CD",
                    PrecioLocal = 46,
                    PrecioDelivery = 49,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                });
                #endregion
                #region Austriaca
                _context.Productos.Add(new Producto
                {
                    Nombre = "AUS SS",
                    PrecioLocal = 29,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "AUS CS",
                    PrecioLocal = 38,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "AUS SD",
                    PrecioLocal = 39,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "AUS CD",
                    PrecioLocal = 48,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                });
                #endregion
                #region Alemana
                _context.Productos.Add(new Producto
                {
                    Nombre = "ALE SS",
                    PrecioLocal = 35,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "ALE CS",
                    PrecioLocal = 44,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "ALE SD",
                    PrecioLocal = 45,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "ALE CD",
                    PrecioLocal = 54,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                });
                #endregion
                #region Mexicana
                _context.Productos.Add(new Producto
                {
                    Nombre = "MEX SS",
                    PrecioLocal = 32,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "MEX CS",
                    PrecioLocal = 41,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "MEX SD",
                    PrecioLocal = 42,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "a"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "MEX CD",
                    PrecioLocal = 51,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "a"
                });
                #endregion


                #endregion

                #region Soda
                _context.Productos.Add(new Producto
                {
                    Nombre = "GUARANA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "b"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "NARANJA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "b"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "COCA COLA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "b"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "COCA COLA ZERO",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "b"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "SPRITE",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "b"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "AGUA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "b"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "AGUA C GAS",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "b"
                });

                #endregion
                #region Cervezas
                _context.Productos.Add(new Producto
                {
                    Nombre = "RED",
                    PrecioLocal = 25,
                    PrecioDelivery = 25,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "c"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "PORTER",
                    PrecioLocal = 25,
                    PrecioDelivery = 25,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "c"
                });
                #endregion
                #region Extra
                _context.Productos.Add(new Producto
                {
                    Nombre = "CARNE",
                    PrecioLocal = 10,
                    PrecioDelivery = 10,
                    Carne=1,
                    Papa=1,
                    Bono=0,
                    Categoria = "d"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "QUESO CHEDAR/PROVO",
                    PrecioLocal = 4,
                    PrecioDelivery = 4,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0,
                    Categoria = "d"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "QUESO FORT/SUI",
                    PrecioLocal = 7,
                    PrecioDelivery = 7,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0,
                    Categoria = "d"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "TOCINO",
                    PrecioLocal = 6,
                    PrecioDelivery = 6,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0,
                    Categoria = "d"
                });


                await _context.SaveChangesAsync();
                #endregion
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
