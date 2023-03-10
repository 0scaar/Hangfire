namespace HangfireProject.Application.Repository
{
    public interface IPersonRepository
    {
        List<Domain.Person> GetPeople(string name);
        List<Domain.Person> GetAllPeople();
    }
}
