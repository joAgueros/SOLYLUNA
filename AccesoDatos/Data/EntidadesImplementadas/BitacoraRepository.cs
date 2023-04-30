using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Helpers;
using AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using System;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class BitacoraRepository : Repository<TbBitacora>, IBitacoraRepository
    {
        private readonly SolyLunaDbContext context;
        private readonly IUserHelper userHelper;

        public BitacoraRepository(SolyLunaDbContext context, IUserHelper userHelper) : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task<bool> EditarPropiedad(string usuario, string codigoIdentificadorPropiedad)
        {
            if (!string.IsNullOrEmpty(usuario))
            {
                TbPropiedade propiedad = await context.TbPropiedades.FirstOrDefaultAsync(x =>
                                            x.CodigoIdentificador.Equals(codigoIdentificadorPropiedad));

                TbOperacionesBitacora operBitacoraObtenida = await context.TbOperacionesBitacoras.FirstOrDefaultAsync(x =>
                                            x.Descripcion.Equals(OperacionBitacora.ACTUALIZAR.ToString()));

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                TbBitacora bitacora = new TbBitacora()
                {
                    IdUsuario = usuarioObtenido.UserName,
                    IdOperacion = operBitacoraObtenida.IdOperacion,
                    Descripcion = $"El usuario {usuarioObtenido.Nombre} {usuarioObtenido.Apellidos}" +
                    $" ha actualizado la propiedad con código {propiedad.IdPropiedad}",
                    Fecha = DateTime.UtcNow,
                    TablaAfectada = "TB_PROPIEDADES",
                    IdTablaAfectada = propiedad.IdPropiedad.ToString()
                };

                await context.TbBitacoras.AddAsync(bitacora);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> InsertarPropiedad(string usuario, string codigoIdentificadorPropiedad)
        {

            if (!string.IsNullOrEmpty(usuario))
            {
                TbPropiedade propiedad = await context.TbPropiedades.FirstOrDefaultAsync(x =>
                                            x.CodigoIdentificador.Equals(codigoIdentificadorPropiedad));

                TbOperacionesBitacora operBitacoraObtenida = await context.TbOperacionesBitacoras.FirstOrDefaultAsync(x =>
                                            x.Descripcion.Equals(OperacionBitacora.INSERTAR.ToString()));

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                TbBitacora bitacora = new TbBitacora()
                {
                    IdUsuario = usuarioObtenido.UserName,
                    IdOperacion = operBitacoraObtenida.IdOperacion,
                    Descripcion = $"El usuario {usuarioObtenido.Nombre} {usuarioObtenido.Apellidos} " +
                    $" ha insertado la propiedad con código {propiedad.IdPropiedad}",
                    Fecha = DateTime.UtcNow,
                    TablaAfectada = "TB_PROPIEDADES",
                    IdTablaAfectada = propiedad.IdPropiedad.ToString()
                };

                await context.TbBitacoras.AddAsync(bitacora);
                await context.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<bool> InsertarConstruccion(string usuario, string codigoIdentificadorConstruccion)
        {

            if (!string.IsNullOrEmpty(usuario))
            {
                TbConstruccion construccion = await context.TbConstruccions.FirstOrDefaultAsync(x =>
                                            x.CodigoIdentificador.Equals(codigoIdentificadorConstruccion));

                TbOperacionesBitacora operBitacoraObtenida = await context.TbOperacionesBitacoras.FirstOrDefaultAsync(x =>
                                            x.Descripcion.Equals(OperacionBitacora.INSERTAR.ToString()));

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                TbBitacora bitacora = new TbBitacora()
                {
                    IdUsuario = usuarioObtenido.UserName,
                    IdOperacion = operBitacoraObtenida.IdOperacion,
                    Descripcion = $"El usuario {usuarioObtenido.Nombre} {usuarioObtenido.Apellidos} " +
                    $" ha actualizado la construcción con código {construccion.IdConstruccion}",
                    Fecha = DateTime.UtcNow,
                    TablaAfectada = "TB_CONSTRUCCION",
                    IdTablaAfectada = construccion.IdConstruccion.ToString()
                };

                await context.TbBitacoras.AddAsync(bitacora);
                await context.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<bool> EditarConstruccion(string usuario, string codigoIdentificadorConstruccion)
        {
            if (!string.IsNullOrEmpty(usuario))
            {
                TbConstruccion construccion = await context.TbConstruccions.FirstOrDefaultAsync(x =>
                                            x.CodigoIdentificador.Equals(codigoIdentificadorConstruccion));

                TbOperacionesBitacora operBitacoraObtenida = await context.TbOperacionesBitacoras.FirstOrDefaultAsync(x =>
                                            x.Descripcion.Equals(OperacionBitacora.ACTUALIZAR.ToString()));

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                TbBitacora bitacora = new TbBitacora()
                {
                    IdUsuario = usuarioObtenido.UserName,
                    IdOperacion = operBitacoraObtenida.IdOperacion,
                    Descripcion = $"El usuario {usuarioObtenido.Nombre} {usuarioObtenido.Apellidos} " +
                    $" ha insertado la construcción con código {construccion.IdConstruccion}",
                    Fecha = DateTime.UtcNow,
                    TablaAfectada = "TB_CONSTRUCCION",
                    IdTablaAfectada = construccion.IdConstruccion.ToString()
                };

                await context.TbBitacoras.AddAsync(bitacora);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> InsertarVendedor(string usuario, string codigoIdentificadorVendedor)
        {
            if (!string.IsNullOrEmpty(usuario))
            {
                TbPersona vendedor = await context.TbPersonas.FirstOrDefaultAsync(x =>
                                            x.Identificacion.Equals(codigoIdentificadorVendedor));

                TbOperacionesBitacora operBitacoraObtenida = await context.TbOperacionesBitacoras.FirstOrDefaultAsync(x =>
                                            x.Descripcion.Equals(OperacionBitacora.INSERTAR.ToString()));

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                TbBitacora bitacora = new TbBitacora()
                {
                    IdUsuario = usuarioObtenido.UserName,
                    IdOperacion = operBitacoraObtenida.IdOperacion,
                    Descripcion = $"El usuario {usuarioObtenido.Nombre} {usuarioObtenido.Apellidos} " +
                    $" ha insertado el cliente vendedor con identificación {vendedor.Identificacion}",
                    Fecha = DateTime.UtcNow,
                    TablaAfectada = "TB_PERSONAS",
                    IdTablaAfectada = vendedor.IdPersona.ToString()
                };

                await context.TbBitacoras.AddAsync(bitacora);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> EditarVendedor(string usuario, string codigoIdentificadorVendedor)
        {
            if (!string.IsNullOrEmpty(usuario))
            {
                TbPersona vendedor = await context.TbPersonas.FirstOrDefaultAsync(x =>
                                            x.Identificacion.Equals(codigoIdentificadorVendedor));

                TbOperacionesBitacora operBitacoraObtenida = await context.TbOperacionesBitacoras.FirstOrDefaultAsync(x =>
                                            x.Descripcion.Equals(OperacionBitacora.ACTUALIZAR.ToString()));

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                TbBitacora bitacora = new TbBitacora()
                {
                    IdUsuario = usuarioObtenido.UserName,
                    IdOperacion = operBitacoraObtenida.IdOperacion,
                    Descripcion = $"El usuario {usuarioObtenido.Nombre} {usuarioObtenido.Apellidos} " +
                    $" ha actualizado el cliente vendedor con identificación {vendedor.Identificacion}",
                    Fecha = DateTime.UtcNow,
                    TablaAfectada = "TB_PERSONAS",
                    IdTablaAfectada = vendedor.IdPersona.ToString()
                };

                await context.TbBitacoras.AddAsync(bitacora);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public Task<bool> InsertarComprador(string usuario, string codigoIdentificadorVendedor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarComprador(string usuario, string codigoIdentificadorVendedor)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InformesModificacionBitacora(string usuario, string mensaje, string tablaAfectada, string operacion, int idTablaAfectada)
        {
            if (!string.IsNullOrEmpty(usuario))
            {
                int valor = 0;

                TbOperacionesBitacora operBitacoraObtenida = await context.TbOperacionesBitacoras.FirstOrDefaultAsync(x =>
                                           x.Descripcion.Equals(operacion));

                switch (operacion)
                {
                    case "INSERTAR":
                        valor = operBitacoraObtenida.IdOperacion;
                        break;
                    case "ACTUALIZAR":
                        valor = operBitacoraObtenida.IdOperacion;
                        break;
                    case "ELIMINAR":
                        valor = operBitacoraObtenida.IdOperacion;
                        break;
                }

                ApplicationUser usuarioObtenido = await userHelper.GetUserByEmailAsync(usuario);

                TbBitacora bitacora = new TbBitacora()
                {
                    IdUsuario = usuarioObtenido.UserName,
                    IdOperacion = valor,
                    Descripcion = mensaje,
                    Fecha = DateTime.UtcNow,
                    TablaAfectada = tablaAfectada,
                    IdTablaAfectada = idTablaAfectada.ToString()
                };

                await context.TbBitacoras.AddAsync(bitacora);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
