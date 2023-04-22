namespace MyhotelApi.Services.IServices;

public interface IEmailService
{
    void SendEmail(string[] receiverEmail, string contentMessage);
}