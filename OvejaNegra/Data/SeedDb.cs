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
                    PrecioDelivery = 0,
                    Carne=0,
                    Papa=1,
                    Bono=0.25,
                    Categoria = "Hamburguesa"
                });

                #region Hamburguesas

                #region gringa
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGRI SS",
                    PrecioLocal = 19,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGRI CS",
                    PrecioLocal = 28,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGRI SD",
                    PrecioLocal = 29,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGRI CD",
                    PrecioLocal = 38,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                });
                #endregion
                #region Gaucha
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGAU SS",
                    PrecioLocal = 25,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGAU CS",
                    PrecioLocal = 34,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGAU SD",
                    PrecioLocal = 35,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aGAU CD",
                    PrecioLocal = 44,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                });

                #endregion
                #region BBQ

                _context.Productos.Add(new Producto
                {
                    Nombre = "aBBQ SS",
                    PrecioLocal = 27,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aBBQ CS",
                    PrecioLocal = 36,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "aBBQ SD",
                    PrecioLocal = 37,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aBBQ CD",
                    PrecioLocal = 46,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                });
                #endregion
                #region Fort
                _context.Productos.Add(new Producto
                {
                    Nombre = "aFORT SS",
                    PrecioLocal = 27,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aFORT CS",
                    PrecioLocal = 36,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "aFORT SD",
                    PrecioLocal = 37,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aFORT CD",
                    PrecioLocal = 46,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                });
                #endregion
                #region Austriaca
                _context.Productos.Add(new Producto
                {
                    Nombre = "aAUS SS",
                    PrecioLocal = 29,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aAUS CS",
                    PrecioLocal = 38,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "aAUS SD",
                    PrecioLocal = 39,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aAUS CD",
                    PrecioLocal = 48,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                });
                #endregion
                #region Alemana
                _context.Productos.Add(new Producto
                {
                    Nombre = "aALE SS",
                    PrecioLocal = 35,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aALE CS",
                    PrecioLocal = 44,
                    PrecioDelivery = 0,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                }); ;
                _context.Productos.Add(new Producto
                {
                    Nombre = "aALE SD",
                    PrecioLocal = 45,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 0,
                    Bono = 0.25,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "aALE CD",
                    PrecioLocal = 54,
                    PrecioDelivery = 0,
                    Carne = 2,
                    Papa = 1,
                    Bono = 0.5,
                    Categoria = "Hamburguesa"
                });
                #endregion


                #endregion

                #region Soda
                _context.Productos.Add(new Producto
                {
                    Nombre = "bGUARANA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "Soda"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "bNARANJA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "Soda"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "bCOCA COLA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "Soda"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "bSPRITE",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "Soda"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "bAGUA",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "Soda"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "bAGUA C GAS",
                    PrecioLocal = 8,
                    PrecioDelivery = 8,
                    Carne = 0,
                    Papa = 0,
                    Bono = 0,
                    Categoria = "Soda"
                });

                #endregion

                #region Extra
                _context.Productos.Add(new Producto
                {
                    Nombre = "eCARNE",
                    PrecioLocal = 10,
                    PrecioDelivery = 10,
                    Carne=1,
                    Papa=1,
                    Bono=0,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "eCHEDAR/PROVO",
                    PrecioLocal = 4,
                    PrecioDelivery = 4,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "eFORT/SUI",
                    PrecioLocal = 7,
                    PrecioDelivery = 7,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0,
                    Categoria = "Hamburguesa"
                });
                _context.Productos.Add(new Producto
                {
                    Nombre = "eTOCINO",
                    PrecioLocal = 6,
                    PrecioDelivery = 6,
                    Carne = 1,
                    Papa = 1,
                    Bono = 0,
                    Categoria = "Hamburguesa"
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
