using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendOrderStatusNotificationEmail(Order order, string lastStatus, string link);
        Task<bool> SendMRBNotificationEmail(string recipientEmails, MRB mrb, string link);
        Task<bool> SendMrbCompleteNotivication(List<string> recipientsEmails, MRB mrb, string link);
        List<string> GetDefaultMrbMembers();
        Task<string> SendCoatingProcessnotification(string link, int processNo);
    }
}
