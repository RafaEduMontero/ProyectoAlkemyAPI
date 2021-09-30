using System;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces.IServices
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
