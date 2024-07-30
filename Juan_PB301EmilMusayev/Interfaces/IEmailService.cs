namespace Juan_PB301EmilMusayev.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string body);
    }
}
