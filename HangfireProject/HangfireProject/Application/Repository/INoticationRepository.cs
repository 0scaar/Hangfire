namespace HangfireProject.Application.Repository
{
    public interface INoticationRepository
    {
        Task<bool> SendEmail(string email);
    }
}
