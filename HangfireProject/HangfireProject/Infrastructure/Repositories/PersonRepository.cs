using HangfireProject.Application.Repository;
using HangfireProject.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HangfireProject.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public List<Person> GetPeople(string name)
        {
            var people = new List<Person>();

            using (var context = new HfContext())
            {
                var result = context.Persons
                    .FromSqlRaw("GetPersonByName @name", new SqlParameter("@name", name))
                    .ToList();

                result.ForEach(p =>
                {
                    var person = new Person(p.Id, p.FullName, p.Email);
                    people.Add(person);
                });

                return people;
            }
        }
    }
}
