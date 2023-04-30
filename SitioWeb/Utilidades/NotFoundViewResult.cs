using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SitioWeb.Utilidades
{
    public class NotFoundViewResult : ViewResult
    {
        public NotFoundViewResult(string nombreVista)
        {
            ViewName = nombreVista;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
