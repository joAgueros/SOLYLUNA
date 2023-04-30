using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using System;
using System.Data;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class HomeAdminRepository : IHomeAdminRepository
    {
        private readonly SolyLunaDbContext context;


        public HomeAdminRepository(SolyLunaDbContext context)
        {
            this.context = context;
        }


        public TotalesPanelAdmin Totales()
        {
            SqlConnection connection = (SqlConnection)context.Database.GetDbConnection(); /*para obtener la conexion con la BD*/

            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.CommandText = "SP_ObtenerTotalesPanelAdmin";

                command.Parameters.Add("@totalClienteVendedor", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalClienteComprador", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@totalPropiedades", SqlDbType.Int).Direction = ParameterDirection.Output;
                //command.Parameters.Add("@totalSolicitudes", SqlDbType.Int).Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                return new TotalesPanelAdmin()
                {
                    TotalClienteVendedor = command.Parameters["@totalClienteVendedor"].Value is DBNull ? 0 : (int)command.Parameters["@totalClienteVendedor"].Value,
                    TotalClienteComprador = command.Parameters["@totalClienteComprador"].Value is DBNull ? 0 : (int)command.Parameters["@totalClienteComprador"].Value,
                    TotalPropiedades = command.Parameters["@totalPropiedades"].Value is DBNull ? 0 : (int)command.Parameters["@totalPropiedades"].Value,
                    //TotalSolicitudes = command.Parameters["@totalSolicitudes"].Value is DBNull ? 0 : (int)command.Parameters["@totalSolicitudes"].Value
                };
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}
