using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public class SolyLunaDbContext : IdentityDbContext<ApplicationUser>
    {
        public SolyLunaDbContext(DbContextOptions<SolyLunaDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<TbAccesibilidad> TbAccesibilidads { get; set; }
        public virtual DbSet<TbAcceso> TbAccesos { get; set; }
        public virtual DbSet<TbActividad> TbActividads { get; set; }
        public virtual DbSet<TbAgendaOficina> TbAgendaOficinas { get; set; }
        public virtual DbSet<TbAgendaPropiedad> TbAgendaPropiedads { get; set; }
        public virtual DbSet<TbAgendum> TbAgenda { get; set; }
        public virtual DbSet<TbAvaluo> TbAvaluos { get; set; }
        public virtual DbSet<TbBitacora> TbBitacoras { get; set; }
        public virtual DbSet<TbCaractRequeridasCompradorPropiedad> TbCaractRequeridasCompradorPropiedads { get; set; }
        public virtual DbSet<TbCaracteristica> TbCaracteristicas { get; set; }
        public virtual DbSet<TbClienteComprador> TbClienteCompradors { get; set; }
        public virtual DbSet<TbClienteVendedor> TbClienteVendedors { get; set; }
        public virtual DbSet<TbComisione> TbComisiones { get; set; }
        public virtual DbSet<TbCompradorPropiedade> TbCompradorPropiedades { get; set; }
        public virtual DbSet<TbConstruccion> TbConstruccions { get; set; }
        public virtual DbSet<TbConstruccionCableado> TbConstruccionCableados { get; set; }
        public virtual DbSet<TbConstruccionDivicione> TbConstruccionDiviciones { get; set; }
        public virtual DbSet<TbConstruccionEquipamiento> TbConstruccionEquipamientos { get; set; }
        public virtual DbSet<TbContacto> TbContactos { get; set; }
        public virtual DbSet<TbContratoDetalle> TbContratoDetalles { get; set; }
        public virtual DbSet<TbContruccionCaracteristica> TbContruccionCaracteristicas { get; set; }
        public virtual DbSet<TbDivisionMateriale> TbDivisionMateriales { get; set; }
        public virtual DbSet<TbDivisionesObra> TbDivisionesObras { get; set; }
        public virtual DbSet<TbDocumento> TbDocumentos { get; set; }
        public virtual DbSet<TbDocumentosComprador> TbDocumentosCompradors { get; set; }
        public virtual DbSet<TbDocumentosPropiedad> TbDocumentosPropiedads { get; set; }
        public virtual DbSet<TbEquipamiento> TbEquipamientos { get; set; }
        public virtual DbSet<TbEstadosPozo> TbEstadosPozos { get; set; }
        public virtual DbSet<TbGestionesCompra> TbGestionesCompras { get; set; }
        public virtual DbSet<TbIntermediario> TbIntermediarios { get; set; }
        public virtual DbSet<TbIntermediarioPropiedad> TbIntermediarioPropiedads { get; set; }
        public virtual DbSet<TbLegalPropiedad> TbLegalPropiedads { get; set; }
        public virtual DbSet<TbMaterialesObra> TbMaterialesObras { get; set; }
        public virtual DbSet<TbMediaPropiedade> TbMediaPropiedades { get; set; }
        public virtual DbSet<TbMediaWeb> TbMediaWebs { get; set; }
        public virtual DbSet<TbMedidaContruccion> TbMedidaContruccions { get; set; }
        public virtual DbSet<TbMedidaPropiedad> TbMedidaPropiedads { get; set; }
        public virtual DbSet<TbMedium> TbMedia { get; set; }
        public virtual DbSet<TbOperacionesBitacora> TbOperacionesBitacoras { get; set; }
        public virtual DbSet<TbPersVisual> TbPersVisuals { get; set; }
        public virtual DbSet<TbPersona> TbPersonas { get; set; }
        public virtual DbSet<TbPersonaJuridica> TbPersonaJuridicas { get; set; }
        public virtual DbSet<TbPropiedadCaracteristica> TbPropiedadCaracteristicas { get; set; }
        public virtual DbSet<TbPropiedadConstruccion> TbPropiedadConstruccions { get; set; }
        public virtual DbSet<TbPropiedade> TbPropiedades { get; set; }
        public virtual DbSet<TbRecorrido> TbRecorridos { get; set; }
        public virtual DbSet<TbReferenciasComprador> TbReferenciasCompradors { get; set; }
        public virtual DbSet<TbReferenciasVendedor> TbReferenciasVendedors { get; set; }
        public virtual DbSet<TbResultadoSolicitante> TbResultadoSolicitantes { get; set; }
        public virtual DbSet<TbResultadoSugef> TbResultadoSugefs { get; set; }
        public virtual DbSet<TbRutaImgconst> TbRutaImgconsts { get; set; }
        public virtual DbSet<TbRutaImgprop> TbRutaImgprops { get; set; }
        public virtual DbSet<TbServiciosMunicipale> TbServiciosMunicipales { get; set; }
        public virtual DbSet<TbServiciosPub> TbServiciosPubs { get; set; }
        public virtual DbSet<TbServiciosPubPropiedad> TbServiciosPubPropiedads { get; set; }
        public virtual DbSet<TbTipoCuota> TbTipoCuotas { get; set; }
        public virtual DbSet<TbTipoIdentificacion> TbTipoIdentificacions { get; set; }
        public virtual DbSet<TbTipoIntermediario> TbTipoIntermediarios { get; set; }
        public virtual DbSet<TbTipoMedida> TbTipoMedidas { get; set; }
        public virtual DbSet<TbTipoPropiedade> TbTipoPropiedades { get; set; }
        public virtual DbSet<TbTipoSituacion> TbTipoSituacions { get; set; }
        public virtual DbSet<TbTipocableado> TbTipocableados { get; set; }
        public virtual DbSet<TbTiposerMunicipal> TbTiposerMunicipals { get; set; }
        public virtual DbSet<TbTopPropiedade> TbTopPropiedades { get; set; }
        public virtual DbSet<TbUsoPropiedad> TbUsoPropiedads { get; set; }
        public virtual DbSet<TbUsoSuelo> TbUsoSuelos { get; set; }
        public virtual DbSet<TbUsoTipopropiedade> TbUsoTipopropiedades { get; set; }
        public virtual DbSet<TbUsuariosIn> TbUsuariosIns { get; set; }
        public virtual DbSet<TbVideosRutum> TbVideosRuta { get; set; }
        public virtual DbSet<Ubicacion> Ubicacions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.AllDay).HasColumnName("all_day");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EventEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("event_end");

                entity.Property(e => e.EventStart)
                    .HasColumnType("datetime")
                    .HasColumnName("event_start");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TbAccesibilidad>(entity =>
            {
                entity.HasKey(e => e.IdAccesibilidad);

                entity.ToTable("TB_ACCESIBILIDAD");

                entity.Property(e => e.IdAccesibilidad).HasColumnName("idAccesibilidad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbAcceso>(entity =>
            {
                entity.HasKey(e => e.IdAcceso);

                entity.ToTable("TB_ACCESO");

                entity.HasComment("t");

                entity.Property(e => e.IdAcceso).HasColumnName("idAcceso");

                entity.Property(e => e.CodigoIdentificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoIdentificador");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.TipoAcceso)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipoAcceso");
            });

            modelBuilder.Entity<TbActividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad);

                entity.ToTable("TB_ACTIVIDAD");

                entity.Property(e => e.IdActividad).HasColumnName("idActividad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbAgendaOficina>(entity =>
            {
                entity.HasKey(e => e.IdAgendaOficina)
                    .HasName("PK_TB_AGENDA_OFICINA_1");

                entity.ToTable("TB_AGENDA_OFICINA");

                entity.Property(e => e.IdAgendaOficina).HasColumnName("idAgendaOficina");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdAgenda).HasColumnName("idAgenda");

                entity.HasOne(d => d.IdAgendaNavigation)
                    .WithMany(p => p.TbAgendaOficinas)
                    .HasForeignKey(d => d.IdAgenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_AGENDA_OFICINA_TB_AGENDA");
            });

            modelBuilder.Entity<TbAgendaPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdAgendaProp);

                entity.ToTable("TB_AGENDA_PROPIEDAD");

                entity.Property(e => e.IdAgendaProp).HasColumnName("idAgendaProp");

                entity.Property(e => e.IdAgenda).HasColumnName("idAgenda");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.HasOne(d => d.IdAgendaNavigation)
                    .WithMany(p => p.TbAgendaPropiedads)
                    .HasForeignKey(d => d.IdAgenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_AGENDA_PROPIEDAD_TB_AGENDA");

                entity.HasOne(d => d.IdAgenda1)
                    .WithMany(p => p.TbAgendaPropiedads)
                    .HasForeignKey(d => d.IdAgenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_AGENDA_PROPIEDAD_TB_PROPIEDADES");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbAgendaPropiedads)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_AGENDA_PROPIEDAD_TB_ACTIVIDAD");
            });

            modelBuilder.Entity<TbAgendum>(entity =>
            {
                entity.HasKey(e => e.IdAgenda)
                    .HasName("PK_TB_AGENDA_OFICINA");

                entity.ToTable("TB_AGENDA");

                entity.Property(e => e.IdAgenda).HasColumnName("idAgenda");

                entity.Property(e => e.Color)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.DiaCompleto).HasColumnName("diaCompleto");

                entity.Property(e => e.Final)
                    .HasColumnType("datetime")
                    .HasColumnName("final");

                entity.Property(e => e.Inicio)
                    .HasColumnType("datetime")
                    .HasColumnName("inicio");

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombreUsuario");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("titulo");
            });

            modelBuilder.Entity<TbAvaluo>(entity =>
            {
                entity.HasKey(e => e.IdAvaluo);

                entity.ToTable("TB_AVALUOS");

                entity.Property(e => e.IdAvaluo).HasColumnName("idAvaluo");

                entity.Property(e => e.FechaAvaluo)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAvaluo");

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("monto");

                entity.HasOne(d => d.IdConstruccionNavigation)
                    .WithMany(p => p.TbAvaluos)
                    .HasForeignKey(d => d.IdConstruccion)
                    .HasConstraintName("FK_TB_AVALUOS_TB_CONSTRUCCION");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbAvaluos)
                    .HasForeignKey(d => d.IdPropiedad)
                    .HasConstraintName("FK_TB_AVALUOS_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbBitacora>(entity =>
            {
                entity.HasKey(e => e.IdBitacora);

                entity.ToTable("TB_BITACORA");

                entity.Property(e => e.IdBitacora).HasColumnName("id_bitacora");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdOperacion).HasColumnName("id_operacion");

                entity.Property(e => e.IdTablaAfectada)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id_tablaAfectada");

                entity.Property(e => e.IdUsuario)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("id_usuario");

                entity.Property(e => e.TablaAfectada)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tablaAfectada");

                entity.HasOne(d => d.IdOperacionNavigation)
                    .WithMany(p => p.TbBitacoras)
                    .HasForeignKey(d => d.IdOperacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_BITACORA_TB_OPERACIONES_BITACORA");
            });

            modelBuilder.Entity<TbCaractRequeridasCompradorPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdCaracteristica);

                entity.ToTable("TB_CaractRequeridasCompradorPropiedad");

                entity.Property(e => e.IdCaracteristica).HasColumnName("idCaracteristica");

                entity.Property(e => e.IdClienteComprador).HasColumnName("idClienteComprador");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.IdTipoPropiedad).HasColumnName("idTipoPropiedad");

                entity.Property(e => e.LugarCompra)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lugarCompra");

                entity.Property(e => e.Presupuesto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("presupuesto");

                entity.Property(e => e.TienePropiedadEspecifica)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tienePropiedadEspecifica")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdClienteCompradorNavigation)
                    .WithMany(p => p.TbCaractRequeridasCompradorPropiedads)
                    .HasForeignKey(d => d.IdClienteComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CaractRequeridasCompradorPropiedad_TB_CLIENTE COMPRADOR");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbCaractRequeridasCompradorPropiedads)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CaractRequeridasCompradorPropiedad_TB_PROPIEDADES");

                entity.HasOne(d => d.IdTipoPropiedadNavigation)
                    .WithMany(p => p.TbCaractRequeridasCompradorPropiedads)
                    .HasForeignKey(d => d.IdTipoPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CaractRequeridasCompradorPropiedad_TB_TIPO_PROPIEDADES");
            });

            modelBuilder.Entity<TbCaracteristica>(entity =>
            {
                entity.HasKey(e => e.IdCaracteristica);

                entity.ToTable("TB_CARACTERISTICAS");

                entity.Property(e => e.IdCaracteristica).HasColumnName("idCaracteristica");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbClienteComprador>(entity =>
            {
                entity.HasKey(e => e.IdClienteC);

                entity.ToTable("TB_CLIENTE COMPRADOR");

                entity.Property(e => e.IdClienteC).HasColumnName("idClienteC");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TbClienteCompradors)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CLIENTE COMPRADOR_TB_PERSONAS");
            });

            modelBuilder.Entity<TbClienteVendedor>(entity =>
            {
                entity.HasKey(e => e.IdClienteV);

                entity.ToTable("TB_CLIENTE_VENDEDOR");

                entity.HasComment("ESTADO DE  LA PROPIEDAD ACTUAL:\r\nEN VENTA O ALQUILER  ACTIVO : Al estar esta bandera se debe publicar la informacion en WEB\r\nEN VENTA O ALQUILER  INACTIVO : Al estar esta bandera indica que la finca está en venta pero no publicada en la WEB\r\n\r\nVENDIDA : Al estar esta bandera indica que la propiedad se vendio y no se debe publicar en la WEB\r\n\r\n");

                entity.Property(e => e.IdClienteV).HasColumnName("idClienteV");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TbClienteVendedors)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CLIENTE_VENDEDOR_TB_PERSONAS");
            });

            modelBuilder.Entity<TbComisione>(entity =>
            {
                entity.HasKey(e => e.IdComision);

                entity.ToTable("TB_COMISIONES");

                entity.HasComment("TABLA DE FORMULARIO QUE SE INSERTA MANUALMENTE, TIENE DEPENDENCIA DE LAS PROPIEDADES PARA SER INSERTADAS\r\n\r\nLOS DATOS DE ESTA TABLA SE DEFINEN DE LA SIGUIENTE MANERA:\r\ntipoComision : Directa o Indirecta - Ganada por medio del dueño o ganada por medio de un tercero. \r\n\r\nprocentajeTotal : Esta es la comision obtenida directa o indirectamente definida en porcentaje.\r\n\r\nporcentajeOfi: Este es un porcenje definido de ganancia para la oficina.\r\n\r\nFactura: Si el cliente que se le realice (SI O NO)\r\n\r\nsobrePrecio: Cuando el cliente quiere el monto cobrado por la propiedad intacto y la empresa debe poner un monto superior para comisionar. El cliente indica que SI y porterior el montoSP ");

                entity.Property(e => e.IdComision).HasColumnName("idComision");

                entity.Property(e => e.Factura)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("factura")
                    .IsFixedLength(true);

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.MontoSp)
                    .HasColumnType("money")
                    .HasColumnName("montoSP");

                entity.Property(e => e.PorcentajeOfi)
                    .HasColumnType("decimal(2, 2)")
                    .HasColumnName("porcentajeOfi");

                entity.Property(e => e.PorcentajeTotal)
                    .HasColumnType("decimal(2, 2)")
                    .HasColumnName("porcentajeTotal");

                entity.Property(e => e.SobrePrecio)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sobrePrecio")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoComision)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipoComision")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbComisiones)
                    .HasForeignKey(d => d.IdPropiedad)
                    .HasConstraintName("FK_TB_COMISIONES_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbCompradorPropiedade>(entity =>
            {
                entity.HasKey(e => e.IdCp);

                entity.ToTable("TB_COMPRADOR_PROPIEDADES");

                entity.Property(e => e.IdCp).HasColumnName("idCP");

                entity.Property(e => e.FechaReg)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaReg");

                entity.Property(e => e.IdClienteC).HasColumnName("idClienteC");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.HasOne(d => d.IdClienteCNavigation)
                    .WithMany(p => p.TbCompradorPropiedades)
                    .HasForeignKey(d => d.IdClienteC)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_COMPRADOR_PROPIEDADES_TB_CLIENTE COMPRADOR");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbCompradorPropiedades)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_COMPRADOR_PROPIEDADES_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbConstruccion>(entity =>
            {
                entity.HasKey(e => e.IdConstruccion);

                entity.ToTable("TB_CONSTRUCCION");

                entity.HasComment("EL ESTADOfISICO DE LA CONSTRUCCION ES :\r\nNUEVA \r\nUSADA \r\nPARA RESTAURAR\r\nEN CONTRUCCION\r\n\r\nEL idPerVisual son los tipos de vision que tiene la propiedad :\r\nHACIA LA MONTAÑA\r\nHACIA EL MAR \r\nEtc\r\n\r\nEL ESTADO TIENE 3 TIPOS\r\n\r\nA = ACTIVO\r\nI = INACTIVO\r\nE = EN PROCESO, EL USUARIO AUN LA ESTA AGREGANDO,\r\nPOR ENDE SI HAY UNA PROPIEDAD EN LA PANTALLA DE\r\nVER PROPIEDAD, Y AUN NO HA FINALIZADO EL INGRESO DE DATOS, PUES NO PUEDE AGREGAR OTRA EN DICHO MOMENTO");

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.Property(e => e.Antiguedad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("antiguedad");

                entity.Property(e => e.CodigoIdentificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoIdentificador");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.Estadofisico)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estadofisico")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.IdMedidaCon).HasColumnName("idMedidaCon");

                entity.Property(e => e.IdPerVisual).HasColumnName("idPerVisual");

                entity.HasOne(d => d.IdMedidaConNavigation)
                    .WithMany(p => p.TbConstruccions)
                    .HasForeignKey(d => d.IdMedidaCon)
                    .HasConstraintName("FK_TB_CONSTRUCCION_TB_MEDIDA_CONTRUCCION");

                entity.HasOne(d => d.IdPerVisualNavigation)
                    .WithMany(p => p.TbConstruccions)
                    .HasForeignKey(d => d.IdPerVisual)
                    .HasConstraintName("FK_TB_CONSTRUCCION_TB_PERS_VISUAL");
            });

            modelBuilder.Entity<TbConstruccionCableado>(entity =>
            {
                entity.HasKey(e => e.IdConstruccionCableado);

                entity.ToTable("TB_CONSTRUCCION_CABLEADO");

                entity.HasComment("estubado = SI - NO \r\ntipoCableado = Aereo - Subterraneo");

                entity.Property(e => e.IdConstruccionCableado).HasColumnName("idConstruccionCableado");

                entity.Property(e => e.Entubado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("entubado")
                    .IsFixedLength(true);

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.Property(e => e.IdTipoCableado).HasColumnName("idTipoCableado");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdConstruccionNavigation)
                    .WithMany(p => p.TbConstruccionCableados)
                    .HasForeignKey(d => d.IdConstruccion)
                    .HasConstraintName("FK_TB_CONSTRUCCION_CABLEADO_TB_CONSTRUCCION1");

                entity.HasOne(d => d.IdTipoCableadoNavigation)
                    .WithMany(p => p.TbConstruccionCableados)
                    .HasForeignKey(d => d.IdTipoCableado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CONSTRUCCION_CABLEADO_TB_TIPOCABLEADO1");
            });

            modelBuilder.Entity<TbConstruccionDivicione>(entity =>
            {
                entity.HasKey(e => e.IdConsDivisiones);

                entity.ToTable("TB_CONSTRUCCION_DIVICIONES");

                entity.Property(e => e.IdConsDivisiones).HasColumnName("idConsDivisiones");

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.Property(e => e.IdDivision).HasColumnName("idDivision");

                entity.Property(e => e.NombreDescriptivo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreDescriptivo");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdConstruccionNavigation)
                    .WithMany(p => p.TbConstruccionDiviciones)
                    .HasForeignKey(d => d.IdConstruccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CONSTRUCCION_DIVICIONES_TB_CONSTRUCCION");

                entity.HasOne(d => d.IdDivisionNavigation)
                    .WithMany(p => p.TbConstruccionDiviciones)
                    .HasForeignKey(d => d.IdDivision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CONSTRUCCION_DIVICIONES_TB_DIVISIONES_OBRA");
            });

            modelBuilder.Entity<TbConstruccionEquipamiento>(entity =>
            {
                entity.HasKey(e => e.IdConstruccionEquipamiento)
                    .HasName("PK_TB_CONSTRUCCION_EQUIPAMIENTO_1");

                entity.ToTable("TB_CONSTRUCCION_EQUIPAMIENTO");

                entity.Property(e => e.IdConstruccionEquipamiento).HasColumnName("idConstruccionEquipamiento");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.Property(e => e.IdEquipamiento).HasColumnName("idEquipamiento");

                entity.HasOne(d => d.IdConstruccionNavigation)
                    .WithMany(p => p.TbConstruccionEquipamientos)
                    .HasForeignKey(d => d.IdConstruccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CONSTRUCCION_EQUIPAMIENTO_TB_CONSTRUCCION");

                entity.HasOne(d => d.IdEquipamientoNavigation)
                    .WithMany(p => p.TbConstruccionEquipamientos)
                    .HasForeignKey(d => d.IdEquipamiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CONSTRUCCION_EQUIPAMIENTO_TB_EQUIPAMIENTO");
            });

            modelBuilder.Entity<TbContacto>(entity =>
            {
                entity.HasKey(e => e.IdContacto);

                entity.ToTable("TB_CONTACTO");

                entity.Property(e => e.IdContacto).HasColumnName("idContacto");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tel");

                entity.Property(e => e.TipoContacto)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("tipoContacto");
            });

            modelBuilder.Entity<TbContratoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdContrato);

                entity.ToTable("TB_CONTRATO_DETALLE");

                entity.HasComment("EN ESTA TABLA SE INSERTAN TODOS LOS CONTRATOS QUE REALIZA LA EMPRESA CON UNA O VARIAS PROPIEDADES.\r\n\r\nLOS DATOS DE ESTAS SON REPRESENTADOS DE LA SIGUIENTE MANERA:\r\n \r\nexclusivo : SI - EL CLIENTE VENDEDOR NO PUEDE VENDER EN OTRA EMPRESA. NO- EL CLIENTE PUEDE VENDER DONDE QUIERA\r\n\r\ndirecto: SI- EL CLIENTE ES DUEÑO LEGITIMO DE LA PROPIEDAD. NO- EL CLIENTE ES INTERMEDIARIO \r\n\r\nfechaContrato: fecha exacta del dia en que se realiza el contrato\r\n\r\nVencimiento: Plazo de validez del contrato , la fecha en finaliza");

                entity.Property(e => e.IdContrato).HasColumnName("idContrato");

                entity.Property(e => e.Directo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("directo")
                    .IsFixedLength(true);

                entity.Property(e => e.Exclusivo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exclusivo")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaContratato)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaContratato");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.Vencimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("vencimiento");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbContratoDetalles)
                    .HasForeignKey(d => d.IdPropiedad)
                    .HasConstraintName("FK_TB_CONTRATO_DETALLE_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbContruccionCaracteristica>(entity =>
            {
                entity.HasKey(e => e.IdConstruccionCaracteristica);

                entity.ToTable("TB_CONTRUCCION_CARACTERISTICAS");

                entity.Property(e => e.IdConstruccionCaracteristica).HasColumnName("idConstruccionCaracteristica");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdCaracteristica).HasColumnName("idCaracteristica");

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.HasOne(d => d.IdCaracteristicaNavigation)
                    .WithMany(p => p.TbContruccionCaracteristicas)
                    .HasForeignKey(d => d.IdCaracteristica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CONTRUCCION_CARACTERISTICAS_TB_CARACTERISTICAS");

                entity.HasOne(d => d.IdConstruccionNavigation)
                    .WithMany(p => p.TbContruccionCaracteristicas)
                    .HasForeignKey(d => d.IdConstruccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_CONTRUCCION_CARACTERISTICAS_TB_CONSTRUCCION");
            });

            modelBuilder.Entity<TbDivisionMateriale>(entity =>
            {
                entity.HasKey(e => e.IdDivisionMateriales);

                entity.ToTable("TB_DIVISION_MATERIALES");

                entity.Property(e => e.IdDivisionMateriales).HasColumnName("idDivisionMateriales");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdConsDivisiones).HasColumnName("idConsDivisiones");

                entity.Property(e => e.IdDivision).HasColumnName("idDivision");

                entity.Property(e => e.IdMaterial).HasColumnName("idMaterial");

                entity.HasOne(d => d.IdConsDivisionesNavigation)
                    .WithMany(p => p.TbDivisionMateriales)
                    .HasForeignKey(d => d.IdConsDivisiones)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_DIVISION_MATERIALES_TB_CONSTRUCCION_DIVICIONES");

                entity.HasOne(d => d.IdDivisionNavigation)
                    .WithMany(p => p.TbDivisionMateriales)
                    .HasForeignKey(d => d.IdDivision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_DIVISION_MATERIALES_TB_DIVISIONES_OBRA");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.TbDivisionMateriales)
                    .HasForeignKey(d => d.IdMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_DIVISION_MATERIALES_TB_MATERIALES_OBRA");
            });

            modelBuilder.Entity<TbDivisionesObra>(entity =>
            {
                entity.HasKey(e => e.IdDivision);

                entity.ToTable("TB_DIVISIONES_OBRA");

                entity.HasComment("ESTA TABLA SE REFIERE A LAS PARTES INTERNAS Y EXTERNAS DE LA CONSTRUCCION, POR EJEMPLO : \r\n\r\nBAÑOS\r\nCOCINA\r\nCIELO RASO\r\nVENTANAS \r\nTECHO \r\nMURO\r\nEtc");

                entity.Property(e => e.IdDivision).HasColumnName("idDivision");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbDocumento>(entity =>
            {
                entity.HasKey(e => e.IdDocumento);

                entity.ToTable("TB_DOCUMENTOS");

                entity.HasComment("ESTA TABLA INDICARA POR MEDIO DE UN CHACKLIST LOS DOCUMENTOS ENTREGAS POR CUALQUIER CLIENTE, SEA VENDEDOR O COMPRADOR , PERSONALES O DE LA PROPIEDAD. \r\n\r\nPOR EJEMPLO:\r\nCÉDULA\r\nPLANOS \r\nUSO DE SUELO\r\nCERTIFICADOS \r\nENTRE OTROS");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbDocumentosComprador>(entity =>
            {
                entity.HasKey(e => e.IdDocComp);

                entity.ToTable("Tb_Documentos_Comprador");

                entity.Property(e => e.IdDocComp).HasColumnName("idDocComp");

                entity.Property(e => e.EstadoRecepcion)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estadoRecepcion")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.IdComprador).HasColumnName("idComprador");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.Property(e => e.Notas)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("notas");

                entity.Property(e => e.Vencimiento)
                    .HasColumnType("date")
                    .HasColumnName("vencimiento");

                entity.HasOne(d => d.IdCompradorNavigation)
                    .WithMany(p => p.TbDocumentosCompradors)
                    .HasForeignKey(d => d.IdComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tb_Documentos_Comprador_TB_CLIENTE COMPRADOR");

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.TbDocumentosCompradors)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tb_Documentos_Comprador_TB_DOCUMENTOS");
            });

            modelBuilder.Entity<TbDocumentosPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdDocPro);

                entity.ToTable("TB_DOCUMENTOS_PROPIEDAD");

                entity.HasComment("Fecha de registro indica el momento en que se recibio el documento, y el vencimiento es la fecha en que el documento se vence\r\n\r\nTambien se liga el usuario que modifico o inserto el dato a la tabla.");

                entity.Property(e => e.IdDocPro).HasColumnName("idDocPro");

                entity.Property(e => e.EstadoRecepcion)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estadoRecepcion")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.Notas)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("notas");

                entity.Property(e => e.Vencimiento)
                    .HasColumnType("date")
                    .HasColumnName("vencimiento");

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.TbDocumentosPropiedads)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_DOCUMENTOS_PROPIEDAD_TB_DOCUMENTOS");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbDocumentosPropiedads)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_DOCUMENTOS_PROPIEDAD_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbEquipamiento>(entity =>
            {
                entity.HasKey(e => e.IdEquipamiento);

                entity.ToTable("TB_EQUIPAMIENTO");

                entity.HasComment("ESTA TABLA CONTIENE LOS INMUEBLES DE LA CONSTRUCCION EN GENERAL");

                entity.Property(e => e.IdEquipamiento).HasColumnName("idEquipamiento");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbEstadosPozo>(entity =>
            {
                entity.HasKey(e => e.IdEstadosPozo);

                entity.ToTable("TB_ESTADOS_POZO");

                entity.HasComment("EN ESTA TABLA ESPECIFICAMOS ESTAS TRES POSIBLES OPCIONES.\r\npozo                            estadoLegal (0,1,2)\r\nPROFUNDO 	INSCRITO\r\nPROFUNDO 	EN TRAMITE\r\nPROFUNDO 	SIN INSCRIBIR\r\nARTESANAL 	INSCRITO\r\nARTESANAL 	EN TRAMITE\r\nARTESANAL 	SIN INSCRIBIR\r\n");

                entity.Property(e => e.IdEstadosPozo).HasColumnName("idEstadosPozo");

                entity.Property(e => e.CodigoIdentificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoIdentificador");

                entity.Property(e => e.EstadoLegal)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estadoLegal")
                    .IsFixedLength(true);

                entity.Property(e => e.Pozo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pozo");
            });

            modelBuilder.Entity<TbGestionesCompra>(entity =>
            {
                entity.HasKey(e => e.IdGestion);

                entity.ToTable("Tb_Gestiones_Compra");

                entity.Property(e => e.IdGestion).HasColumnName("idGestion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaEntrega)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaEntrega");

                entity.Property(e => e.FechaSolicitud)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaSolicitud");

                entity.Property(e => e.IdComprador).HasColumnName("idComprador");

                entity.HasOne(d => d.IdCompradorNavigation)
                    .WithMany(p => p.TbGestionesCompras)
                    .HasForeignKey(d => d.IdComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tb_Gestiones_Compra_TB_CLIENTE COMPRADOR");
            });

            modelBuilder.Entity<TbIntermediario>(entity =>
            {
                entity.HasKey(e => e.IdIntermediario);

                entity.ToTable("TB_INTERMEDIARIOS");

                entity.Property(e => e.IdIntermediario).HasColumnName("idIntermediario");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.IdTipoInter).HasColumnName("idTipoInter");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TbIntermediarios)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_TB_INTERMEDIARIOS_TB_PERSONAS");

                entity.HasOne(d => d.IdTipoInterNavigation)
                    .WithMany(p => p.TbIntermediarios)
                    .HasForeignKey(d => d.IdTipoInter)
                    .HasConstraintName("FK_TB_INTERMEDIARIOS_TB_TIPO_INTERMEDIARIO1");
            });

            modelBuilder.Entity<TbIntermediarioPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdInterProp);

                entity.ToTable("TB_INTERMEDIARIO_PROPIEDAD");

                entity.Property(e => e.IdInterProp).HasColumnName("idInterProp");

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.IdIntermediario).HasColumnName("idIntermediario");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.HasOne(d => d.IdIntermediarioNavigation)
                    .WithMany(p => p.TbIntermediarioPropiedads)
                    .HasForeignKey(d => d.IdIntermediario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_INTERMEDIARIO_PROPIEDAD_TB_INTERMEDIARIOS");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbIntermediarioPropiedads)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_INTERMEDIARIO_PROPIEDAD_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbLegalPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdLegal);

                entity.ToTable("TB_LEGAL_PROPIEDAD");

                entity.HasComment("EN ESTA TABLA SE ESPECIFICARA TODA LA SIUACION LEGAL DE UNA PROPIEDAD, POR EJEMPLO SI ESTA CUENTA CON HIPOTECAS, CONCESIONES, INFORMACIONES POSESORIAS, QUE SE ESPECIFICARAN EN TB_TIPOS_SITUACION\r\n\r\nDEFINICION: \r\nnombre entidad : ESPECIFICA EL NOMBRE DE LOS BANCOS O INSTITUCIONES COMO MUNICIPALIDAD \r\n\r\nidCuota: ESPECIFICA EL TIPO DE CUOTA QUE SE PAGA, POR EJEMPLO, ANUAL, MENSUAL, ETC\r\nmonto : EL MONTO A PAGAR \r\nestado : ESTE ESPECIFICA SI ESTÁ EN TRÁMITE O NO. \r\n\r\nALGUNOS DE LOS CAMPOS ANTERIORES VAN A QUEDAR EN N/A DEBIDO A QUE NO TODAS LAS SITUACIONES AMERITAN EL LLENADO TOTAL. \r\nEJEMPLO: MORTUAL, INFORMACION POSESORIA");

                entity.Property(e => e.IdLegal).HasColumnName("idLegal");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.IdCuota).HasColumnName("idCuota");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.IdTipoSituacion).HasColumnName("idTipoSituacion");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("monto");

                entity.Property(e => e.NombreEntidad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreEntidad");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdCuotaNavigation)
                    .WithMany(p => p.TbLegalPropiedads)
                    .HasForeignKey(d => d.IdCuota)
                    .HasConstraintName("FK_TB_LEGAL_PROPIEDAD_TB_TIPO_CUOTAS");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbLegalPropiedads)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_LEGAL_PROPIEDAD_TB_PROPIEDADES");

                entity.HasOne(d => d.IdTipoSituacionNavigation)
                    .WithMany(p => p.TbLegalPropiedads)
                    .HasForeignKey(d => d.IdTipoSituacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_LEGAL_PROPIEDAD_TB_TIPO_SITUACION");
            });

            modelBuilder.Entity<TbMaterialesObra>(entity =>
            {
                entity.HasKey(e => e.IdMaterial);

                entity.ToTable("TB_MATERIALES_OBRA");

                entity.HasComment("t");

                entity.Property(e => e.IdMaterial).HasColumnName("idMaterial");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbMediaPropiedade>(entity =>
            {
                entity.HasKey(e => e.IdMedPro);

                entity.ToTable("TB_MEDIA_PROPIEDADES");

                entity.Property(e => e.IdMedPro).HasColumnName("idMedPro");

                entity.Property(e => e.IdMedia).HasColumnName("idMedia");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.HasOne(d => d.IdMediaNavigation)
                    .WithMany(p => p.TbMediaPropiedades)
                    .HasForeignKey(d => d.IdMedia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_MEDIA_PROPIEDADES_TB_MEDIA");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbMediaPropiedades)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_MEDIA_PROPIEDADES_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbMediaWeb>(entity =>
            {
                entity.HasKey(e => e.IdMedWeb);

                entity.ToTable("TB_MEDIA_WEB");

                entity.Property(e => e.IdMedWeb).HasColumnName("idMedWeb");

                entity.Property(e => e.IdMedia).HasColumnName("idMedia");

                entity.HasOne(d => d.IdMediaNavigation)
                    .WithMany(p => p.TbMediaWebs)
                    .HasForeignKey(d => d.IdMedia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_MEDIA_WEB_TB_MEDIA");
            });

            modelBuilder.Entity<TbMedidaContruccion>(entity =>
            {
                entity.HasKey(e => e.IdMedidaCon);

                entity.ToTable("TB_MEDIDA_CONTRUCCION");

                entity.Property(e => e.IdMedidaCon).HasColumnName("idMedidaCon");

                entity.Property(e => e.CodigoIdentificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoIdentificador");

                entity.Property(e => e.IdTipoMedida).HasColumnName("idTipoMedida");

                entity.Property(e => e.Medida)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("medida");

                entity.HasOne(d => d.IdTipoMedidaNavigation)
                    .WithMany(p => p.TbMedidaContruccions)
                    .HasForeignKey(d => d.IdTipoMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_MEDIDA_CONTRUCCION_TB_TIPO_MEDIDAS");
            });

            modelBuilder.Entity<TbMedidaPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdMedidaPro);

                entity.ToTable("TB_MEDIDA_PROPIEDAD");

                entity.Property(e => e.IdMedidaPro).HasColumnName("idMedidaPro");

                entity.Property(e => e.CodigoIdentificador)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.IdTipoMedida).HasColumnName("idTipoMedida");

                entity.Property(e => e.Medida)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("medida");
            });

            modelBuilder.Entity<TbMedium>(entity =>
            {
                entity.HasKey(e => e.IdMedia);

                entity.ToTable("TB_MEDIA");

                entity.Property(e => e.IdMedia).HasColumnName("idMedia");

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ruta");
            });

            modelBuilder.Entity<TbOperacionesBitacora>(entity =>
            {
                entity.HasKey(e => e.IdOperacion);

                entity.ToTable("TB_OPERACIONES_BITACORA");

                entity.Property(e => e.IdOperacion).HasColumnName("id_operacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbPersVisual>(entity =>
            {
                entity.HasKey(e => e.IdPerVisual);

                entity.ToTable("TB_PERS_VISUAL");

                entity.Property(e => e.IdPerVisual).HasColumnName("idPerVisual");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("TB_PERSONAS");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.Ape1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ape1");

                entity.Property(e => e.Ape2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ape2");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdPersonaJ).HasColumnName("idPersonaJ");

                entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");

                entity.Property(e => e.IdUbicacion).HasColumnName("idUbicacion");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.TelCas)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("telCas");

                entity.Property(e => e.TelOfi)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("telOfi");

                entity.Property(e => e.TelPer)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("telPer");

                entity.HasOne(d => d.IdPersonaJNavigation)
                    .WithMany(p => p.TbPersonas)
                    .HasForeignKey(d => d.IdPersonaJ)
                    .HasConstraintName("FK_TB_PERSONAS_TB_PERSONA_JURIDICA");

                entity.HasOne(d => d.IdTipoIdentificacionNavigation)
                    .WithMany(p => p.TbPersonas)
                    .HasForeignKey(d => d.IdTipoIdentificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PERSONAS_TB_TIPO_IDENTIFICACION");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.TbPersonas)
                    .HasForeignKey(d => d.IdUbicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PERSONAS_Ubicacion");
            });

            modelBuilder.Entity<TbPersonaJuridica>(entity =>
            {
                entity.HasKey(e => e.IdPersonaJ);

                entity.ToTable("TB_PERSONA_JURIDICA");

                entity.Property(e => e.IdPersonaJ).HasColumnName("idPersonaJ");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.NombreEntidad)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("nombreEntidad");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("razonSocial");
            });

            modelBuilder.Entity<TbPropiedadCaracteristica>(entity =>
            {
                entity.HasKey(e => e.IdPropiedadCaracteristica);

                entity.ToTable("TB_PROPIEDAD_CARACTERISTICAS");

                entity.HasComment("TABLA COMPUESTA PARA SELECCIONAR TODAS LAS CARACTERITICAS DE LA PROPIEDAD, POR EJEMPLO SI ESTA CUENTA CON: \r\nARBOLES FRUTALES\r\nJARDINES \r\nPATIO \r\nZONAS VERDES \r\nBODEGA \r\nGALERON , \r\nENTRE OTRAS");

                entity.Property(e => e.IdPropiedadCaracteristica).HasColumnName("idPropiedadCaracteristica");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdCaracteristica).HasColumnName("idCaracteristica");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.HasOne(d => d.IdCaracteristicaNavigation)
                    .WithMany(p => p.TbPropiedadCaracteristicas)
                    .HasForeignKey(d => d.IdCaracteristica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PROPIEDAD_CARACTERISTICAS_TB_CARACTERISTICAS");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbPropiedadCaracteristicas)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PROPIEDAD_CARACTERISTICAS_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbPropiedadConstruccion>(entity =>
            {
                entity.HasKey(e => e.IdPropiedadConstruccion);

                entity.ToTable("TB_PROPIEDAD_CONSTRUCCION");

                entity.Property(e => e.IdPropiedadConstruccion).HasColumnName("idPropiedadConstruccion");

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.HasOne(d => d.IdConstruccionNavigation)
                    .WithMany(p => p.TbPropiedadConstruccions)
                    .HasForeignKey(d => d.IdConstruccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PROPIEDAD_CONSTRUCCION_TB_CONSTRUCCION");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbPropiedadConstruccions)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PROPIEDAD_CONSTRUCCION_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbPropiedade>(entity =>
            {
                entity.HasKey(e => e.IdPropiedad);

                entity.ToTable("TB_PROPIEDADES");

                entity.HasComment("EN ESTA TABLA SE INGRESAN ALGUNOS DATOS DIRECTOS, POR EJEMPLO : \r\ndireccion\r\nprecioMax\r\nprecioMin\r\nacceso : (Se refiere a la facilidad de ingreso a la propiedad en caso de ser finca)\r\ncuotaMante: este dato es en caso de ser condominio o si alguna otra lo requiere, por lo general va a aparecer en blanco o con N/A\r\ndisAgua: Nos indica por medio de una banderilla si tiene o no (1,0), esto si el agua es por naciente , u otra forma que no sea publico. \r\n\r\nEn idEstadosPozo se jala el dato del estado legal, en dado que no cuente con este solamente no se inserta nada.");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.BarrioPoblado)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("barrioPoblado");

                entity.Property(e => e.CodigoIdentificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoIdentificador");

                entity.Property(e => e.CodigoTipoUsoPropiedad)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigoTipoUsoPropiedad");

                entity.Property(e => e.CuotaMante)
                    .HasColumnType("money")
                    .HasColumnName("cuotaMante");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.DireccionCompleta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccionCompleta");

                entity.Property(e => e.DisAgua)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("disAgua")
                    .IsFixedLength(true);

                entity.Property(e => e.Eliminado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("eliminado")
                    .IsFixedLength(true);

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaRegis)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegis");

                entity.Property(e => e.IdAcceso).HasColumnName("idAcceso");

                entity.Property(e => e.IdClienteV).HasColumnName("idClienteV");

                entity.Property(e => e.IdEstadosPozo).HasColumnName("idEstadosPozo");

                entity.Property(e => e.IdMedidaPro).HasColumnName("idMedidaPro");

                entity.Property(e => e.IdUbicacion).HasColumnName("idUbicacion");

                entity.Property(e => e.IdUsoSuelo).HasColumnName("idUsoSuelo");

                entity.Property(e => e.IdUsoTipo).HasColumnName("idUsoTipo");

                entity.Property(e => e.Intencion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("intencion")
                    .HasComment("Esta es para renta o venta");

                entity.Property(e => e.LinkVideo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("linkVideo");

                entity.Property(e => e.Megusta).HasColumnName("megusta");

                entity.Property(e => e.Moneda)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("moneda");

                entity.Property(e => e.NivelCalle)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nivelCalle");

                entity.Property(e => e.NumFinca)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("numFinca");

                entity.Property(e => e.NumPlano)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("numPlano");

                entity.Property(e => e.PoseeVistaMar)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PoseeVistaMontania)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PoseeVistaValle)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PrecioMax)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("precioMax");

                entity.Property(e => e.PrecioMin)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("precioMin");

                entity.Property(e => e.Publicado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("publicado")
                    .IsFixedLength(true);

                entity.Property(e => e.SinVista)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Topografia)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("topografia");

                entity.HasOne(d => d.IdAccesoNavigation)
                    .WithMany(p => p.TbPropiedades)
                    .HasForeignKey(d => d.IdAcceso)
                    .HasConstraintName("FK_TB_PROPIEDADES_TB_ACCESO");

                entity.HasOne(d => d.IdClienteVNavigation)
                    .WithMany(p => p.TbPropiedades)
                    .HasForeignKey(d => d.IdClienteV)
                    .HasConstraintName("FK_TB_PROPIEDADES_TB_CLIENTE_VENDEDOR");

                entity.HasOne(d => d.IdEstadosPozoNavigation)
                    .WithMany(p => p.TbPropiedades)
                    .HasForeignKey(d => d.IdEstadosPozo)
                    .HasConstraintName("FK_TB_PROPIEDADES_TB_ESTADOS_POZO");

                entity.HasOne(d => d.IdMedidaProNavigation)
                    .WithMany(p => p.TbPropiedades)
                    .HasForeignKey(d => d.IdMedidaPro)
                    .HasConstraintName("FK_TB_PROPIEDADES_TB_MEDIDA_PROPIEDAD");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.TbPropiedades)
                    .HasForeignKey(d => d.IdUbicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PROPIEDADES_Ubicacion");

                entity.HasOne(d => d.IdUsoSueloNavigation)
                    .WithMany(p => p.TbPropiedades)
                    .HasForeignKey(d => d.IdUsoSuelo)
                    .HasConstraintName("FK_TB_PROPIEDADES_TB_USO_SUELO");

                entity.HasOne(d => d.IdUsoTipoNavigation)
                    .WithMany(p => p.TbPropiedades)
                    .HasForeignKey(d => d.IdUsoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_PROPIEDADES_TB_USO_TIPOPROPIEDADES1");
            });

            modelBuilder.Entity<TbRecorrido>(entity =>
            {
                entity.HasKey(e => e.IdRecorrido);

                entity.ToTable("TB_RECORRIDO");

                entity.HasComment("EN ESTA TABLA INDICAMOS EL RECORRIDO A CADA ACCESIBILIDAD, POR EJEMPLO EL ACCESO A UNA PLAYA, ESCUELAS, HOSPITAL, VETERINARIO, ENTRE OTROS. \r\n\r\nSe hace de esta forma porque una finca puede tener  tantos accesibilidades quiere y viceversa. \r\n\r\nEN EL CAMPO recorridoKm se ponen los Kilometros para llegar a ese destino");

                entity.Property(e => e.IdRecorrido).HasColumnName("idRecorrido");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdAccesibilidad).HasColumnName("idAccesibilidad");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.RecorridoKm)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("recorridoKm");

                entity.HasOne(d => d.IdAccesibilidadNavigation)
                    .WithMany(p => p.TbRecorridos)
                    .HasForeignKey(d => d.IdAccesibilidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_RECORRIDO_TB_ACCESIBILIDAD2");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbRecorridos)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_RECORRIDO_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbReferenciasComprador>(entity =>
            {
                entity.HasKey(e => e.IdReferencia);

                entity.ToTable("TB_REFERENCIAS_COMPRADOR");

                entity.Property(e => e.IdReferencia).HasColumnName("idReferencia");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.IdClienteC).HasColumnName("idClienteC");

                entity.HasOne(d => d.IdClienteCNavigation)
                    .WithMany(p => p.TbReferenciasCompradors)
                    .HasForeignKey(d => d.IdClienteC)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_REFERENCIAS_COMPRADOR_TB_CLIENTE COMPRADOR");
            });

            modelBuilder.Entity<TbReferenciasVendedor>(entity =>
            {
                entity.HasKey(e => e.IdReferencia);

                entity.ToTable("TB_REFERENCIAS_VENDEDOR");

                entity.Property(e => e.IdReferencia).HasColumnName("idReferencia");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.IdClienteV).HasColumnName("idClienteV");

                entity.HasOne(d => d.IdClienteVNavigation)
                    .WithMany(p => p.TbReferenciasVendedors)
                    .HasForeignKey(d => d.IdClienteV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_REFERENCIAS_VENDEDOR_TB_CLIENTE_VENDEDOR");
            });

            modelBuilder.Entity<TbResultadoSolicitante>(entity =>
            {
                entity.HasKey(e => e.IdResultado);

                entity.ToTable("Tb_ResultadoSolicitante");

                entity.Property(e => e.IdResultado).HasColumnName("idResultado");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdComprador).HasColumnName("idComprador");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdCompradorNavigation)
                    .WithMany(p => p.TbResultadoSolicitantes)
                    .HasForeignKey(d => d.IdComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tb_ResultadoSolicitante_TB_CLIENTE COMPRADOR");
            });

            modelBuilder.Entity<TbResultadoSugef>(entity =>
            {
                entity.HasKey(e => e.IdResultado);

                entity.ToTable("Tb_ResultadoSugef");

                entity.Property(e => e.IdResultado).HasColumnName("idResultado");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdComprador).HasColumnName("idComprador");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdCompradorNavigation)
                    .WithMany(p => p.TbResultadoSugefs)
                    .HasForeignKey(d => d.IdComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tb_ResultadoSugef_TB_CLIENTE COMPRADOR");
            });

            modelBuilder.Entity<TbRutaImgconst>(entity =>
            {
                entity.HasKey(e => e.IdRuta);

                entity.ToTable("TB_RUTA_IMGCONST");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaIns");

                entity.Property(e => e.IdConstruccion).HasColumnName("idConstruccion");

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdConstruccionNavigation)
                    .WithMany(p => p.TbRutaImgconsts)
                    .HasForeignKey(d => d.IdConstruccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_RUTA_IMGCONST_TB_CONSTRUCCION");
            });

            modelBuilder.Entity<TbRutaImgprop>(entity =>
            {
                entity.HasKey(e => e.IdRuta);

                entity.ToTable("TB_RUTA_IMGPROP");

                entity.HasComment("Esta tabla especifica la ruta en que se guardo la la imagen, además el Id de la propiedad , el cual se puede concatener para formar una llave unica de ubicacion. \r\n\r\nAsimismo, la fecha en que se inserta a la BD ");

                entity.Property(e => e.IdRuta).HasColumnName("idRuta");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaIns");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("ruta");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbRutaImgprops)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_RUTA_IMGPROP_TB_PROPIEDADES");
            });

            modelBuilder.Entity<TbServiciosMunicipale>(entity =>
            {
                entity.HasKey(e => e.IdSerMuni);

                entity.ToTable("TB_SERVICIOS_MUNICIPALES");

                entity.HasComment("rtt");

                entity.Property(e => e.IdSerMuni).HasColumnName("idSerMuni");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("costo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.IdCuota).HasColumnName("idCuota");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.IdTipoSer).HasColumnName("idTipoSer");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdCuotaNavigation)
                    .WithMany(p => p.TbServiciosMunicipales)
                    .HasForeignKey(d => d.IdCuota)
                    .HasConstraintName("FK_TB_SERVICIOS_MUNICIPALES_TB_TIPO_CUOTAS");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbServiciosMunicipales)
                    .HasForeignKey(d => d.IdPropiedad)
                    .HasConstraintName("FK_TB_SERVICIOS_MUNICIPALES_TB_PROPIEDADES");

                entity.HasOne(d => d.IdTipoSerNavigation)
                    .WithMany(p => p.TbServiciosMunicipales)
                    .HasForeignKey(d => d.IdTipoSer)
                    .HasConstraintName("FK_TB_SERVICIOS_MUNICIPALES_TB_TIPOSER_MUNICIPAL");
            });

            modelBuilder.Entity<TbServiciosPub>(entity =>
            {
                entity.HasKey(e => e.IdServicioPublico);

                entity.ToTable("TB_SERVICIOS_PUB");

                entity.Property(e => e.IdServicioPublico).HasColumnName("idServicioPublico");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbServiciosPubPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdServicioPubPropiedad);

                entity.ToTable("TB_SERVICIOS_PUB_PROPIEDAD");

                entity.Property(e => e.IdServicioPubPropiedad).HasColumnName("idServicioPubPropiedad");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("costo");

                entity.Property(e => e.Distancia)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("distancia");

                entity.Property(e => e.Empresa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("empresa");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.IdServicioPublico).HasColumnName("idServicioPublico");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.TbServiciosPubPropiedads)
                    .HasForeignKey(d => d.IdPropiedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_SERVICIOS_PUB_PROPIEDAD_TB_PROPIEDADES");

                entity.HasOne(d => d.IdServicioPublicoNavigation)
                    .WithMany(p => p.TbServiciosPubPropiedads)
                    .HasForeignKey(d => d.IdServicioPublico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_SERVICIOS_PUB_PROPIEDAD_TB_SERVICIOS_PUB");
            });

            modelBuilder.Entity<TbTipoCuota>(entity =>
            {
                entity.HasKey(e => e.IdCuota);

                entity.ToTable("TB_TIPO_CUOTAS");

                entity.HasComment("LOS TIPOS DE CUOTAS ´PUEDEN SERVIR PARA LOS SERVICIOS MUNICIPALES Y PARA LAS CUOTAS UTILIZADAS EN HIPOTECAS O CONCESIONES DE LEGAL. \r\n\r\nPOR ESO LA TABLA PUEDE SER CONSULTADA Y UTILIZADA POR AMBOS");

                entity.Property(e => e.IdCuota).HasColumnName("idCuota");

                entity.Property(e => e.Cuota)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cuota");
            });

            modelBuilder.Entity<TbTipoIdentificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoIdentificacion);

                entity.ToTable("TB_TIPO_IDENTIFICACION");

                entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbTipoIntermediario>(entity =>
            {
                entity.HasKey(e => e.IdTipoInter);

                entity.ToTable("TB_TIPO_INTERMEDIARIO");

                entity.HasComment("SE DEFINE COMO AQUELLAS PERSONAS QUE INTERMEDIAN EN UN NEGOCIO DIRECTO ENTRE UNA PROPIEDAD O UN CLIENTE COMPRADOR , LOS DATOS SON :\r\n\r\ncomisionista\r\nreferencia\r\nrecomendante");

                entity.Property(e => e.IdTipoInter).HasColumnName("idTipoInter");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("detalle");
            });

            modelBuilder.Entity<TbTipoMedida>(entity =>
            {
                entity.HasKey(e => e.IdTipoMedida);

                entity.ToTable("TB_TIPO_MEDIDAS");

                entity.Property(e => e.IdTipoMedida).HasColumnName("idTipoMedida");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Siglas)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("siglas")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbTipoPropiedade>(entity =>
            {
                entity.HasKey(e => e.IdTipoPro);

                entity.ToTable("TB_TIPO_PROPIEDADES");

                entity.Property(e => e.IdTipoPro).HasColumnName("idTipoPro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbTipoSituacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoSituacion);

                entity.ToTable("TB_TIPO_SITUACION");

                entity.Property(e => e.IdTipoSituacion).HasColumnName("idTipoSituacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbTipocableado>(entity =>
            {
                entity.HasKey(e => e.IdTipoCableado);

                entity.ToTable("TB_TIPOCABLEADO");

                entity.Property(e => e.IdTipoCableado).HasColumnName("idTipoCableado");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbTiposerMunicipal>(entity =>
            {
                entity.HasKey(e => e.IdTipoSer);

                entity.ToTable("TB_TIPOSER_MUNICIPAL");

                entity.HasComment("LOS TIPOS DE SERVICIOS PUEDEN SER \r\nRECOLECCION DE BASURA\r\nIMPUESTOS \r\nETC");

                entity.Property(e => e.IdTipoSer).HasColumnName("idTipoSer");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbTopPropiedade>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TB_TopPropiedades");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.IdTop).HasColumnName("idTop");
            });

            modelBuilder.Entity<TbUsoPropiedad>(entity =>
            {
                entity.HasKey(e => e.IdUsoPro);

                entity.ToTable("TB_USO_PROPIEDAD");

                entity.HasComment("Tabla con el fin de definir el uso de la propiedad, por ejemplo: Comercial, Residencial, Industrial. El apartado es similar al Uso de suelo, pero este es solo para diferenciar inicialmente la finca. El Uso de suelo se aporta en otra tabla  para distinguir de esta.");

                entity.Property(e => e.IdUsoPro).HasColumnName("idUsoPro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbUsoSuelo>(entity =>
            {
                entity.HasKey(e => e.IdUsoSuelo);

                entity.ToTable("TB_USO_SUELO");

                entity.HasComment("COMO LA INFORMACION DE USO DE SUELO ES OTORGADA POR EL MUNICIPIO, A UNO DE ESTOS SE LE VA A PONER PENDIENTE, PARA EVITAR CONFLICTOS DE INSERCION, Y QUE ESTE SE PUEDA MODIFICAR A LA LISTA QUE EXISTA INTERNAMENTE");

                entity.Property(e => e.IdUsoSuelo).HasColumnName("idUsoSuelo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TbUsoTipopropiedade>(entity =>
            {
                entity.HasKey(e => e.IdUsoTipo);

                entity.ToTable("TB_USO_TIPOPROPIEDADES");

                entity.HasComment("ESTA TABLA SE LLENA AL SELECCIONAR EL USO Y EL TIPO DE PROPIEDAD PARA SER ASIGNADA A LA TABLA PROPIEDADES. \r\n\r\nCabe recalcar que un tipo de propiedad puede tener varios usos, pero estos se deben ligar antes de insertados en la TB_PROPIEDADES");

                entity.Property(e => e.IdUsoTipo).HasColumnName("idUsoTipo");

                entity.Property(e => e.CodigoIdentificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigoIdentificador");

                entity.Property(e => e.IdTipoPro).HasColumnName("idTipoPro");

                entity.Property(e => e.IdUsoPro).HasColumnName("idUsoPro");

                entity.HasOne(d => d.IdTipoProNavigation)
                    .WithMany(p => p.TbUsoTipopropiedades)
                    .HasForeignKey(d => d.IdTipoPro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_USO_TIPOPROPIEDADES_TB_TIPO_PROPIEDADES");

                entity.HasOne(d => d.IdTipoPro1)
                    .WithMany(p => p.TbUsoTipopropiedades)
                    .HasForeignKey(d => d.IdTipoPro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_USO_TIPOPROPIEDADES_TB_USO_PROPIEDAD");
            });

            modelBuilder.Entity<TbUsuariosIn>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioIn);

                entity.ToTable("TB_USUARIOS_IN");

                entity.Property(e => e.IdUsuarioIn).HasColumnName("idUsuarioIn");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength(true);

                entity.Property(e => e.FechIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fechIngreso");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.Puesto)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("puesto");

                entity.Property(e => e.Role)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("role")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TbUsuariosIns)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_USUARIOS_IN_TB_PERSONAS");
            });

            modelBuilder.Entity<TbVideosRutum>(entity =>
            {
                entity.HasKey(e => e.IdVideo);

                entity.ToTable("TB_VIDEOS_RUTA");

                entity.Property(e => e.IdVideo).HasColumnName("idVideo");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaIns");

                entity.Property(e => e.IdPropiedad).HasColumnName("idPropiedad");

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("ruta");
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.HasKey(e => e.IdUbicacion)
                    .HasName("PK_Ubicacion_1");

                entity.ToTable("Ubicacion");

                entity.Property(e => e.IdUbicacion).HasColumnName("idUbicacion");

                entity.Property(e => e.Canton).HasMaxLength(255);

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Distrito).HasMaxLength(255);

                entity.Property(e => e.Lat)
                    .HasMaxLength(500)
                    .IsFixedLength(true);

                entity.Property(e => e.Long)
                    .HasMaxLength(500)
                    .IsFixedLength(true);

                entity.Property(e => e.Provincia).HasMaxLength(255);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
