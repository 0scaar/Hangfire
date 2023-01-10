namespace HangfireProject.Domain
{
    public class Person
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }

        public Person(int id, string fullName, string email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }
    }
}
