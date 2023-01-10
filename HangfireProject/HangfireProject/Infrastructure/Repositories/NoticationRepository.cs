using HangfireProject.Application.Repository;

namespace HangfireProject.Infrastructure.Repositories
{
    public class NoticationRepository : INoticationRepository
    {
        public async Task<bool> SendEmail(string email)
        {
            await Task.Delay(500);

            return true; 
        }
    }
}
