using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Employees
{
    class Menu
    {
        readonly Print p = new();
        public string[]? PointsMenu { get; set; }
        public string? Logo { get; set; }

        public void MainMenu()
        {
            PointsMenu = new string[] { "Список сотрудников", "Добавить сотрудника", "Удалить сотруднка" };
            Logo = "Welcome";
            p.PrintMenu(Logo, true, PointsMenu);
            switch (Console.Read())
            {
                case '1':
                    ViewEmployees();
                    break;
                case '2':
                    AddEmployee();
                    break;
                case '3':
                    DeleteEmployee();
                    break;
                default:
                    Console.WriteLine("Необходимо выбрать пункт меню!");
                    Thread.Sleep(1500);
                    MainMenu();
                    break;

            }
        }
        public void ViewEmployees()
        {
            Logo = "ViewEmployees";
            var employees = File.Exists("Employees.json") ? JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText("Employees.json")) : null;
            if (employees != null)
            {
                PointsMenu = employees.Select(x => x.FirstName + " " + x.LastName).ToArray();
                p.PrintMenu(Logo, true, PointsMenu);
                Console.Write("\n\nВыберите сотрудника для просмотра детальной информации: ");
                try
                {
                    Console.ReadLine();
                    int x = Convert.ToInt32(Console.ReadLine());

                    if (employees.Count <= x || x >= 0)
                    {
                        Logo = "EmployeeProfile";
                        p.PrintMenu(Logo, false, PointsMenu);
                        Console.WriteLine("\n\nИмя сотрудника: \n\t" + employees[x - 1].FirstName + "\nФамилия отрудника: \n\t" + employees[x - 1].LastName + "\nТелефон сотудника: \n\t" + employees[x - 1].PhoneNumber + "\nОписание сотрудника: \n\t" + employees[x - 1].Description + "\n\nНажмите любую кнопку чтобы вернуться в меню...\n\n");
                        Console.ReadKey();
                        MainMenu();
                    }
                }
                catch
                {
                    Console.WriteLine("Введено некорректное значение!");
                    Thread.Sleep(1500);
                    ViewEmployees();
                }

            }
            else
            {
                Console.WriteLine("Сотрудников в базе нет!");
                Thread.Sleep(1500);
                MainMenu();
            }
        }
        public void AddEmployee()
        {
            Logo = "AddEmployee";
            p.PrintMenu(Logo, false, PointsMenu);
            string timeFirstName, timeLastName, timePhoneNumber, timeDiscription;

            Console.WriteLine("Введите имя сотрудника:");
            do
            {
                timeFirstName = Console.ReadLine();
            }
            while (timeFirstName.Length <= 0);

            Console.WriteLine("Введите фамилию сотрудника:");
            do
            {
                timeLastName = Console.ReadLine();
            }
            while (timeLastName.Length <= 0);

            Console.WriteLine("Введите телефон сотрудника:");
            do
            {
                timePhoneNumber = Console.ReadLine();
            }
            while (timePhoneNumber.Length <= 0);

            Console.WriteLine("Введите описание сотрудника(либо пропустите)");
            timeDiscription = Console.ReadLine();

            var TimeEmp = new Employee(timeFirstName, timeLastName, timePhoneNumber, timeDiscription);
            var employees = File.Exists("Employees.json") ? JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText("Employees.json")) : new List<Employee>();
            if (employees != null)
            {
                employees.Add(TimeEmp);
                File.WriteAllText("Employees.json", JsonSerializer.Serialize(employees));
            }
            else
            {
                File.WriteAllText("Employees.json", JsonSerializer.Serialize(TimeEmp));
            }
            Console.WriteLine("Работник добавлен в базу!");
            Thread.Sleep(1500);
            MainMenu();
        }
        public void DeleteEmployee()
        {
            Logo = "DeleteEmployee";
            var employees = File.Exists("Employees.json") ? JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText("Employees.json")) : null;
            if (employees != null)
            {
                PointsMenu = employees.Select(x => x.FirstName + " " + x.LastName).ToArray();
                p.PrintMenu(Logo, true, PointsMenu);
                Console.Write("\n\nВыберите сотрудника для удаления: ");
                try
                {
                    Console.ReadLine();
                    int x = Convert.ToInt32(Console.ReadLine());

                    if (employees.Count <= x || x >= 0)
                    {
                        Console.Write("\n\nВы учерены в удалении? Нажмите Y для подтверждения:\nНажмите N для отмены:\n\n\tЛибо нажмите Q для выхода...  ");
                        char t = Convert.ToChar(Console.Read());
                        if (t == 'y')
                        {
                            employees.RemoveAt(x - 1);
                            if (employees.Count == 0)
                            {
                                File.Delete("Employees.json");
                            }
                            else
                            {
                                File.WriteAllText("Employees.json", JsonSerializer.Serialize(employees));
                            }
                            Console.WriteLine("\nСотрудник успешно удален!");
                            Thread.Sleep(2000);
                        }
                        else if (t == 'q')
                        {
                            MainMenu();
                        }
                        else if (t == 'N')
                        {
                            DeleteEmployee();
                        }
                        else
                        {
                            Console.WriteLine("Ошибка!..");
                            Thread.Sleep(2000);
                            DeleteEmployee();
                        }
                        MainMenu();
                    }
                }
                catch
                {
                    Console.WriteLine("Введено некорректное значение!");
                    Thread.Sleep(1500);
                    ViewEmployees();
                }

            }
            else
            {
                Console.WriteLine("Сотрудников в базе нет!");
                Thread.Sleep(2000);
                MainMenu();
            }
        }
        //public void EmployeeProfile(Employee emp, int index)
        //{
        //    Console.WriteLine("Имя сотрудника: " + emp.FirstName + "\nФамилия отрудника: " + emp.LastName + "\nТелефон сотудника" + emp.PhoneNumber + "\nОписание сотрудника:" + emp.Description + "\n\nНажмите любую кнопку чтобы вернуться в меню...\n\n");
        //}
    }
}
