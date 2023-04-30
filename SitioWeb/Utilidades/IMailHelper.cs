namespace SitioWeb.Utilidades
{
    public interface IMailHelper
    {
        bool SendMail(string to, string cc, string subject, string body);
    }
}
