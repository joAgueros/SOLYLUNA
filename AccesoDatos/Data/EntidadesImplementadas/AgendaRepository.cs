using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Modelos.Entidades;
using Modelos.ViewModels.Agendas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class AgendaRepository : Repository<Event>, IAgendaRepository
    {
        private readonly SolyLunaDbContext context;

        public AgendaRepository(SolyLunaDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<Eventos> GetCalendarEvents(string start, string end)
        {
            try


            {
                CultureInfo culture2 = new CultureInfo("en-US");
                List<Eventos> Consulta = (from events in context.Events
                                          where events.EventStart >= Convert.ToDateTime(start) && events.EventEnd <= Convert.ToDateTime(end)
                                          select new Eventos

                                          {
                                              EventId = events.EventId,
                                              Title = Convert.ToString(events.Title),
                                              Description = Convert.ToString(events.Description),
                                              Start = events.EventStart.ToString("MM/dd/yyyy hh:mm"),
                                              End = events.EventEnd.ToString("MM/dd/yyyy hh:mm"),
                                              AllDay = events.AllDay
                                          }).ToList();

                return Consulta;
            }
            catch (Exception)
            {
                return null;
            }
        }




        public async Task<Response> AddEvent(Eventos evt)
        {
            Response response = null;

            if (evt != null)
            {

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        DateTime? fechaStarConcat = null;

                        if (!string.IsNullOrEmpty(evt.Start))
                        {

                            string[] subs = evt.Start.Split(" ");
                            string[] array = subs[0].Split("/");
                            int mes = Convert.ToInt32(array[0]);
                            int dia = Convert.ToInt32(array[1]);
                            int anio = Convert.ToInt32(array[2]);

                            string[] array2 = subs[1].Split(":");

                            int hora = Convert.ToInt32(array2[0]);
                            int min = Convert.ToInt32(array2[1]);

                            DateTime? fechaVenc = new DateTime(anio, mes, dia);
                            TimeSpan tiempo = new TimeSpan(hora + min);

                            fechaStarConcat = fechaVenc.Value.Add(tiempo);
                            DateTime? prueba = fechaStarConcat;
                        }

                        DateTime? fechaEndConcat = null;

                        if (!string.IsNullOrEmpty(evt.End))
                        {

                            string[] subsEnd = evt.End.Split(" ");
                            string[] arrayEnd = subsEnd[0].Split("/");
                            int mes = Convert.ToInt32(arrayEnd[0]);
                            int dia = Convert.ToInt32(arrayEnd[1]);
                            int anio = Convert.ToInt32(arrayEnd[2]);

                            string[] arrayEnd2 = subsEnd[1].Split(":");

                            int hora = Convert.ToInt32(arrayEnd2[0]);
                            int min = Convert.ToInt32(arrayEnd2[1]);

                            DateTime? fechaVenc = new DateTime(anio, mes, dia);
                            TimeSpan tiempo = new TimeSpan(hora + min);

                            fechaEndConcat = fechaVenc.Value.Add(tiempo);

                            DateTime? prueba = fechaEndConcat;

                        }
                        Event objetoEnviar = new Event()
                        {
                            Title = evt.Title,
                            Description = evt.Description,
                            EventStart = Convert.ToDateTime(fechaStarConcat),
                            EventEnd = Convert.ToDateTime(fechaEndConcat),
                            AllDay = evt.AllDay
                        };

                        await context.Events.AddAsync(objetoEnviar);
                        await context.SaveChangesAsync();


                        Event obEvent = context.Events.FirstOrDefault(
                         x => x.EventStart == fechaStarConcat && x.EventEnd == fechaEndConcat);

                        /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                        transaction.Commit();
                        response = new Response { EsCorrecto = true, Resultado = obEvent, Mensaje = "OK" };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response = new Response { EsCorrecto = false, Mensaje = ex.Message };
                    }

                });

            }

            return response;

        }

        public async Task<Response> UpdateEvents(Eventos evt)
        {

            Response response = null;
            if (evt != null)
            {

                Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();

                    try
                    {
                        DateTime? fechaStarConcat = null;

                        if (!string.IsNullOrEmpty(evt.Start))
                        {

                            string[] subs = evt.Start.Split(" ");
                            string[] array = subs[0].Split("/");
                            int mes = Convert.ToInt32(array[0]);
                            int dia = Convert.ToInt32(array[1]);
                            int anio = Convert.ToInt32(array[2]);

                            string[] array2 = subs[1].Split(":");

                            int hora = Convert.ToInt32(array2[0]);
                            int min = Convert.ToInt32(array2[1]);

                            DateTime? fechaVenc = new DateTime(anio, mes, dia);
                            TimeSpan tiempo = new TimeSpan(hora + min);

                            fechaStarConcat = fechaVenc.Value.Add(tiempo);
                            DateTime? prueba = fechaStarConcat;
                        }

                        DateTime? fechaEndConcat = null;

                        if (!string.IsNullOrEmpty(evt.End))
                        {
                            string[] subsEnd = evt.End.Split(" ");
                            string[] arrayEnd = subsEnd[0].Split("/");
                            int mes = Convert.ToInt32(arrayEnd[0]);
                            int dia = Convert.ToInt32(arrayEnd[1]);
                            int anio = Convert.ToInt32(arrayEnd[2]);

                            string[] arrayEnd2 = subsEnd[1].Split(":");

                            int hora = Convert.ToInt32(arrayEnd2[0]);
                            int min = Convert.ToInt32(arrayEnd2[1]);

                            DateTime? fechaVenc = new DateTime(anio, mes, dia);
                            TimeSpan tiempo = new TimeSpan(hora + min);

                            fechaEndConcat = fechaVenc.Value.Add(tiempo);

                            DateTime? prueba = fechaEndConcat;

                        }

                        Event objetoEnviar = await context.Events.FirstOrDefaultAsync(x =>
                                             x.EventId.Equals(evt.EventId));

                        objetoEnviar.Title = evt.Title;
                        objetoEnviar.Description = evt.Description;
                        objetoEnviar.EventStart = Convert.ToDateTime(fechaStarConcat);
                        objetoEnviar.EventEnd = Convert.ToDateTime(fechaEndConcat);
                        objetoEnviar.AllDay = evt.AllDay;


                        context.Events.Update(objetoEnviar);
                        await context.SaveChangesAsync();

                        /*Si todo transcurrio correctamente, se cierra la transaccion de manera exitosa*/
                        transaction.Commit();
                        response = new Response { EsCorrecto = true, Mensaje = "OK" };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response = new Response { EsCorrecto = false, Mensaje = ex.Message };
                    }

                });

            }

            return response;

        }

        public async Task<Response> DeleteEvents(int eventId)
        {
            try
            {
                Event objetoObtenido = await context.Events.FindAsync(eventId);

                if (objetoObtenido != null)
                {
                    context.Events.Remove(objetoObtenido);
                    await context.SaveChangesAsync();
                    return new Response { EsCorrecto = true, Mensaje = "OK" };
                }

                return new Response { EsCorrecto = false, Mensaje = "No existe" };
            }
            catch (Exception ex)
            {
                return new Response { EsCorrecto = false, Mensaje = $"ERROR {ex.Message}" };
            }
        }

    }
}
