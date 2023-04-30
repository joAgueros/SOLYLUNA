using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Modelos.ViewModels.Clientes;
using System;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class ContactoRepository : Repository<TbContacto>, IContactoRepository
    {
        private readonly SolyLunaDbContext context;

        public ContactoRepository(SolyLunaDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<string> RegistrarContacto(RegistrarContactoViewModel model)
        {
            string mensaje = string.Empty;

            try
            {
                if (model != null)
                {

                    //var contacto = await this.context.TbContactos.FirstOrDefaultAsync(v => v.Identificacion.Equals(model.Identificacion));

                    ///*Verificar si el vendedor ya existe con dicho correo*/
                    //if (contacto != null)
                    //{
                    //    return mensaje = "Ya existe";
                    //}

                    Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                    await strategy.ExecuteAsync(async () =>
                    {
                        using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                        try
                        {

                            TbContacto objetoEnviar = new TbContacto()
                            {
                                IdContacto = model.IdContacto,
                                Nombre = model.Nombre,
                                Apellidos = model.Apellidos,
                                Tel = model.Tel,
                                Correo = model.Correo,
                                TipoContacto = model.TipoContacto,
                                Descripcion = model.Descripcion,
                                Fecha = model.Fecha,
                                Estado = "1"

                            };

                            await context.TbContactos.AddAsync(objetoEnviar);

                            await context.SaveChangesAsync();


                            /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                            transaction.Commit();

                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            mensaje = "Error";
                        }

                    });

                    return mensaje = "OK";

                }
            }
            catch (Exception)
            {
                mensaje = "Error";
            }

            return mensaje = "Error";
        }

    }

}

