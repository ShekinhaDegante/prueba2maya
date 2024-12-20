using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba2maya.Migrations
{
    public partial class UpdateEmailColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasProductos",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCategoria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__A3C02A1025C0D3EE", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "ModosPago",
                columns: table => new
                {
                    IdModoPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ModosPag__8E18FA0717520843", x => x.IdModoPago);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProveedor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__E8B631AF2020BC4F", x => x.IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__2A49584C43E7FF9E", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    IdSucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSucursal = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sucursal__BFB6CD99B5C1F8FC", x => x.IdSucursal);
                });

            migrationBuilder.CreateTable(
                name: "PedidosProveedores",
                columns: table => new
                {
                    IdPedidoProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: true),
                    FechaPedido = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PedidosP__B39B8E6CFE017008", x => x.IdPedidoProveedor);
                    table.ForeignKey(
                        name: "FK__PedidosPr__IdPro__48CFD27E",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IdRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__5B65BF972B27845D", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK__Usuarios__IdRol__02084FDA",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: true),
                    IdCategoria = table.Column<int>(type: "int", nullable: true),
                    IdSucursal = table.Column<int>(type: "int", nullable: true),
                    IdProveedor = table.Column<int>(type: "int", nullable: true),
                    ImagenUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__09889210B1A16AC8", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK__Productos__IdCat__403A8C7D",
                        column: x => x.IdCategoria,
                        principalTable: "CategoriasProductos",
                        principalColumn: "IdCategoria");
                    table.ForeignKey(
                        name: "FK__Productos__IdPro__4222D4EF",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor");
                    table.ForeignKey(
                        name: "FK__Productos__IdSuc__412EB0B6",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "IdSucursal");
                });

            migrationBuilder.CreateTable(
                name: "CarritoVentas",
                columns: table => new
                {
                    IdCarrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarritoV__8B4A618C18C64FA9", x => x.IdCarrito);
                    table.ForeignKey(
                        name: "FK__CarritoVe__IdUsu__4F7CD00D",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ClientesFrecuentes",
                columns: table => new
                {
                    IdClienteFrecuente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    TotalCompras = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FechaUltimaCompra = table.Column<DateTime>(type: "datetime", nullable: true),
                    PuntosAcumulados = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__FF0AE4DDF4BE9117", x => x.IdClienteFrecuente);
                    table.ForeignKey(
                        name: "FK__ClientesF__IdCli__693CA210",
                        column: x => x.IdCliente,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaPedido = table.Column<DateTime>(type: "datetime", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MetodoEntrega = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DireccionEntrega = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    IdSucursal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pedidos__9D335DC3C4C6562F", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK__Pedidos__IdSucur__571DF1D5",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "IdSucursal");
                    table.ForeignKey(
                        name: "FK__Pedidos__IdUsuar__5629CD9C",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "AuditoriaInventario",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    CantidadAntes = table.Column<int>(type: "int", nullable: true),
                    CantidadDespues = table.Column<int>(type: "int", nullable: true),
                    FechaAuditoria = table.Column<DateTime>(type: "datetime", nullable: true),
                    RealizadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Auditori__7FD13FA0179052A6", x => x.IdAuditoria);
                    table.ForeignKey(
                        name: "FK__Auditoria__IdPro__6FE99F9F",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK__Auditoria__Reali__70DDC3D8",
                        column: x => x.RealizadoPor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidosProveedores",
                columns: table => new
                {
                    IdDetallePedidoProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedidoProveedor = table.Column<int>(type: "int", nullable: true),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    CantidadSolicitada = table.Column<int>(type: "int", nullable: true),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleP__57A1DC0E423A71BD", x => x.IdDetallePedidoProveedor);
                    table.ForeignKey(
                        name: "FK__DetallePe__IdPed__4BAC3F29",
                        column: x => x.IdPedidoProveedor,
                        principalTable: "PedidosProveedores",
                        principalColumn: "IdPedidoProveedor");
                    table.ForeignKey(
                        name: "FK__DetallePe__IdPro__4CA06362",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "HistorialPrecios",
                columns: table => new
                {
                    IdHistorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    PrecioAnterior = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PrecioNuevo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FechaCambio = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__9CC7DBB46DF94462", x => x.IdHistorial);
                    table.ForeignKey(
                        name: "FK__Historial__IdPro__6383C8BA",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "InventarioSucursal",
                columns: table => new
                {
                    IdInventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    IdSucursal = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventar__1927B20C63A0F4C9", x => x.IdInventario);
                    table.ForeignKey(
                        name: "FK__Inventari__IdPro__44FF419A",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK__Inventari__IdSuc__45F365D3",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "IdSucursal");
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    IdOferta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    PorcentajeDescuento = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ofertas__5420E1DA0345650F", x => x.IdOferta);
                    table.ForeignKey(
                        name: "FK__Ofertas__IdProdu__66603565",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "CarritoProductos",
                columns: table => new
                {
                    IdCarritoProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCarrito = table.Column<int>(type: "int", nullable: true),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarritoP__54956F9CABC715B4", x => x.IdCarritoProducto);
                    table.ForeignKey(
                        name: "FK__CarritoPr__IdCar__52593CB8",
                        column: x => x.IdCarrito,
                        principalTable: "CarritoVentas",
                        principalColumn: "IdCarrito");
                    table.ForeignKey(
                        name: "FK__CarritoPr__IdPro__534D60F1",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    IdDetallePedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: true),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleP__48AFFD959C37781C", x => x.IdDetallePedido);
                    table.ForeignKey(
                        name: "FK__DetallePe__IdPed__59FA5E80",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido");
                    table.ForeignKey(
                        name: "FK__DetallePe__IdPro__5AEE82B9",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "Envios",
                columns: table => new
                {
                    IdEnvio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: true),
                    DireccionEnvio = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    IdSucursalRetiro = table.Column<int>(type: "int", nullable: true),
                    EstadoEnvio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaEntrega = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Envios__B814A62E01D78071", x => x.IdEnvio);
                    table.ForeignKey(
                        name: "FK__Envios__IdPedido__6C190EBB",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido");
                    table.ForeignKey(
                        name: "FK__Envios__IdSucurs__6D0D32F4",
                        column: x => x.IdSucursalRetiro,
                        principalTable: "Sucursales",
                        principalColumn: "IdSucursal");
                });

            migrationBuilder.CreateTable(
                name: "HistorialPedidos",
                columns: table => new
                {
                    IdHistorialPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: true),
                    EstadoAnterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EstadoNuevo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaCambio = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActualizadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__A760BE96637C3350", x => x.IdHistorialPedido);
                    table.ForeignKey(
                        name: "FK__Historial__Actua__74AE54BC",
                        column: x => x.ActualizadoPor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK__Historial__IdPed__73BA3083",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido");
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesPagos",
                columns: table => new
                {
                    IdTransaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: true),
                    IdModoPago = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FechaTransaccion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transacc__334B1F77AA49818C", x => x.IdTransaccion);
                    table.ForeignKey(
                        name: "FK__Transacci__IdMod__60A75C0F",
                        column: x => x.IdModoPago,
                        principalTable: "ModosPago",
                        principalColumn: "IdModoPago");
                    table.ForeignKey(
                        name: "FK__Transacci__IdPed__5FB337D6",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaInventario_IdProducto",
                table: "AuditoriaInventario",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaInventario_RealizadoPor",
                table: "AuditoriaInventario",
                column: "RealizadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProductos_IdCarrito",
                table: "CarritoProductos",
                column: "IdCarrito");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProductos_IdProducto",
                table: "CarritoProductos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoVentas_IdUsuario",
                table: "CarritoVentas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesFrecuentes_IdCliente",
                table: "ClientesFrecuentes",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_IdPedido",
                table: "DetallePedidos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_IdProducto",
                table: "DetallePedidos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidosProveedores_IdPedidoProveedor",
                table: "DetallePedidosProveedores",
                column: "IdPedidoProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidosProveedores_IdProducto",
                table: "DetallePedidosProveedores",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_IdPedido",
                table: "Envios",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_IdSucursalRetiro",
                table: "Envios",
                column: "IdSucursalRetiro");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPedidos_ActualizadoPor",
                table: "HistorialPedidos",
                column: "ActualizadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPedidos_IdPedido",
                table: "HistorialPedidos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPrecios_IdProducto",
                table: "HistorialPrecios",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioSucursal_IdProducto",
                table: "InventarioSucursal",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioSucursal_IdSucursal",
                table: "InventarioSucursal",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_IdProducto",
                table: "Ofertas",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdSucursal",
                table: "Pedidos",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosProveedores_IdProveedor",
                table: "PedidosProveedores",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdCategoria",
                table: "Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdProveedor",
                table: "Productos",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdSucursal",
                table: "Productos",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesPagos_IdModoPago",
                table: "TransaccionesPagos",
                column: "IdModoPago");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesPagos_IdPedido",
                table: "TransaccionesPagos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__A9D10534F58AAA08",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditoriaInventario");

            migrationBuilder.DropTable(
                name: "CarritoProductos");

            migrationBuilder.DropTable(
                name: "ClientesFrecuentes");

            migrationBuilder.DropTable(
                name: "DetallePedidos");

            migrationBuilder.DropTable(
                name: "DetallePedidosProveedores");

            migrationBuilder.DropTable(
                name: "Envios");

            migrationBuilder.DropTable(
                name: "HistorialPedidos");

            migrationBuilder.DropTable(
                name: "HistorialPrecios");

            migrationBuilder.DropTable(
                name: "InventarioSucursal");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "TransaccionesPagos");

            migrationBuilder.DropTable(
                name: "CarritoVentas");

            migrationBuilder.DropTable(
                name: "PedidosProveedores");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "ModosPago");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "CategoriasProductos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
