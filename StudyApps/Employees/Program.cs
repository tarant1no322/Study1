using System.Text.Json;
using System.IO;

namespace Employees
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var employee = new List<Employee>
            //    {
            //    new Employee("John", "Connor", "88005553535", "ggg"),
            //    new Employee("Sarah", "Connor", "6567", "Мать Джона"),
            //    new Employee("T", "800", "101", "Терминатор"),
            //    new Employee("Kyle", "Reese", "868", "Отец Джона")
            //    };

            //File.WriteAllText("Employees.json", JsonSerializer.Serialize(employee));

            Menu mm = new Menu();
            mm.MainMenu();
        }
    }
}