using Microsoft.EntityFrameworkCore;

namespace coursework0
{
    public class Teacher //Поля в бд
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Kaf { get; set; }
        public string Spec { get; set; }
        public string Number { get; set; }
        public string Birthday { get; set; }
        public string Adres { get; set; }
        public string ImagePath { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        public ApplicationContext() //Проверяет наличие бд
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //Подключение к бд
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-K2ONRI75\\SQLEXPRESS; Database=bdo; Trusted_Connection=True");
        }
    }

}
