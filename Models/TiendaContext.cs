using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prueba2maya.Models
{
    public partial class TiendaContext : DbContext
    {
        public TiendaContext()
        {
        }

        public TiendaContext(DbContextOptions<TiendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditoriaInventario> AuditoriaInventarios { get; set; } = null!;
        public virtual DbSet<CarritoProducto> CarritoProductos { get; set; } = null!;
        public virtual DbSet<CarritoVenta> CarritoVentas { get; set; } = null!;
        public virtual DbSet<CategoriasProducto> CategoriasProductos { get; set; } = null!;
        public virtual DbSet<ClientesFrecuente> ClientesFrecuentes { get; set; } = null!;
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
        public virtual DbSet<DetallePedidosProveedore> DetallePedidosProveedores { get; set; } = null!;
        public virtual DbSet<Envio> Envios { get; set; } = null!;
        public virtual DbSet<HistorialPedido> HistorialPedidos { get; set; } = null!;
        public virtual DbSet<HistorialPrecio> HistorialPrecios { get; set; } = null!;
        public virtual DbSet<InventarioSucursal> InventarioSucursals { get; set; } = null!;
        public virtual DbSet<ModosPago> ModosPagos { get; set; } = null!;
        public virtual DbSet<Oferta> Ofertas { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<PedidosProveedore> PedidosProveedores { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Sucursale> Sucursales { get; set; } = null!;
        public virtual DbSet<TransaccionesPago> TransaccionesPagos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=LAPTOP-Q1PBGT9D\\MSSQLSERVER2; Database=TiendaAbarrotes; User Id=sa;Password=anzures123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditoriaInventario>(entity =>
            {
                entity.HasKey(e => e.IdAuditoria)
                    .HasName("PK__Auditori__7FD13FA0179052A6");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.AuditoriaInventarios)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Auditoria__IdPro__6FE99F9F");

                entity.HasOne(d => d.RealizadoPorNavigation)
                    .WithMany(p => p.AuditoriaInventarios)
                    .HasForeignKey(d => d.RealizadoPor)
                    .HasConstraintName("FK__Auditoria__Reali__70DDC3D8");
            });

            modelBuilder.Entity<CarritoProducto>(entity =>
            {
                entity.HasKey(e => e.IdCarritoProducto)
                    .HasName("PK__CarritoP__54956F9CABC715B4");

                entity.HasOne(d => d.IdCarritoNavigation)
                    .WithMany(p => p.CarritoProductos)
                    .HasForeignKey(d => d.IdCarrito)
                    .HasConstraintName("FK__CarritoPr__IdCar__52593CB8");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.CarritoProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__CarritoPr__IdPro__534D60F1");
            });

            modelBuilder.Entity<CarritoVenta>(entity =>
            {
                entity.HasKey(e => e.IdCarrito)
                    .HasName("PK__CarritoV__8B4A618C18C64FA9");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CarritoVenta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__CarritoVe__IdUsu__4F7CD00D");
            });

            modelBuilder.Entity<CategoriasProducto>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A1025C0D3EE");
            });

            modelBuilder.Entity<ClientesFrecuente>(entity =>
            {
                entity.HasKey(e => e.IdClienteFrecuente)
                    .HasName("PK__Clientes__FF0AE4DDF4BE9117");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClientesFrecuentes)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__ClientesF__IdCli__693CA210");
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(e => e.IdDetallePedido)
                    .HasName("PK__DetalleP__48AFFD959C37781C");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__DetallePe__IdPed__59FA5E80");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetallePe__IdPro__5AEE82B9");
            });

            modelBuilder.Entity<DetallePedidosProveedore>(entity =>
            {
                entity.HasKey(e => e.IdDetallePedidoProveedor)
                    .HasName("PK__DetalleP__57A1DC0E423A71BD");

                entity.HasOne(d => d.IdPedidoProveedorNavigation)
                    .WithMany(p => p.DetallePedidosProveedores)
                    .HasForeignKey(d => d.IdPedidoProveedor)
                    .HasConstraintName("FK__DetallePe__IdPed__4BAC3F29");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetallePedidosProveedores)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetallePe__IdPro__4CA06362");
            });

            modelBuilder.Entity<Envio>(entity =>
            {
                entity.HasKey(e => e.IdEnvio)
                    .HasName("PK__Envios__B814A62E01D78071");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Envios)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__Envios__IdPedido__6C190EBB");

                entity.HasOne(d => d.IdSucursalRetiroNavigation)
                    .WithMany(p => p.Envios)
                    .HasForeignKey(d => d.IdSucursalRetiro)
                    .HasConstraintName("FK__Envios__IdSucurs__6D0D32F4");
            });

            modelBuilder.Entity<HistorialPedido>(entity =>
            {
                entity.HasKey(e => e.IdHistorialPedido)
                    .HasName("PK__Historia__A760BE96637C3350");

                entity.HasOne(d => d.ActualizadoPorNavigation)
                    .WithMany(p => p.HistorialPedidos)
                    .HasForeignKey(d => d.ActualizadoPor)
                    .HasConstraintName("FK__Historial__Actua__74AE54BC");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.HistorialPedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__Historial__IdPed__73BA3083");
            });

            modelBuilder.Entity<HistorialPrecio>(entity =>
            {
                entity.HasKey(e => e.IdHistorial)
                    .HasName("PK__Historia__9CC7DBB46DF94462");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.HistorialPrecios)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Historial__IdPro__6383C8BA");
            });

            modelBuilder.Entity<InventarioSucursal>(entity =>
            {
                entity.HasKey(e => e.IdInventario)
                    .HasName("PK__Inventar__1927B20C63A0F4C9");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.InventarioSucursals)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Inventari__IdPro__44FF419A");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.InventarioSucursals)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Inventari__IdSuc__45F365D3");
            });

            modelBuilder.Entity<ModosPago>(entity =>
            {
                entity.HasKey(e => e.IdModoPago)
                    .HasName("PK__ModosPag__8E18FA0717520843");
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.HasKey(e => e.IdOferta)
                    .HasName("PK__Ofertas__5420E1DA0345650F");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Ofertas__IdProdu__66603565");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("PK__Pedidos__9D335DC3C4C6562F");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Pedidos__IdSucur__571DF1D5");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Pedidos__IdUsuar__5629CD9C");
            });

            modelBuilder.Entity<PedidosProveedore>(entity =>
            {
                entity.HasKey(e => e.IdPedidoProveedor)
                    .HasName("PK__PedidosP__B39B8E6CFE017008");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.PedidosProveedores)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__PedidosPr__IdPro__48CFD27E");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__09889210B1A16AC8");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Productos__IdCat__403A8C7D");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__Productos__IdPro__4222D4EF");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Productos__IdSuc__412EB0B6");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__E8B631AF2020BC4F");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Roles__2A49584C43E7FF9E");
            });

            modelBuilder.Entity<Sucursale>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("PK__Sucursal__BFB6CD99B5C1F8FC");
            });

            modelBuilder.Entity<TransaccionesPago>(entity =>
            {
                entity.HasKey(e => e.IdTransaccion)
                    .HasName("PK__Transacc__334B1F77AA49818C");

                entity.HasOne(d => d.IdModoPagoNavigation)
                    .WithMany(p => p.TransaccionesPagos)
                    .HasForeignKey(d => d.IdModoPago)
                    .HasConstraintName("FK__Transacci__IdMod__60A75C0F");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.TransaccionesPagos)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__Transacci__IdPed__5FB337D6");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__5B65BF972B27845D");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuarios__IdRol__02084FDA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
