using System;
using System.Text.Json;
using EmployeesConsoleCommand.ConsoleCommands;
using EmployeesConsoleCommand.DataController;

namespace EmployeesConsoleCommand
{
    class Program
    {
        /// <summary>
        /// Через цикл ловим нажатия, вызываем методы которые печатают инфу, выполняют логику,
        /// после чего возвращают в исходный метод, который снова ловит нажатия клавиш
        /// с поправкой на предыдущую логику, возможно с применением делегатов.
        /// Должен работать эскейп, Y/N, управление клавишами по динамическим пунктам меню
        /// ~~~~Идеал переносим в готовый проект и проводим рефакторинг
        /// </summary>


        static void Main()
        {
            #region генерация базы из 20 сотрудников
            var _employees = new List<Employee>
                {
                new Employee("John", "Connor", "+79773522765", "Спаситель Человечества"),
                new Employee("Sarah", "Connor", "+7995017543", "Мать Джона"),
                new Employee("T", "800", "Model-101", "Терминатор"),
                new Employee("Kyle", "Reese", "+79263386351", "Отец Джона"),
                new Employee("John1", "Connor", "+79773522765", "Спаситель Человечества"),
                new Employee("Sarah1", "Connor", "+7995017543", "Мать Джона"),
                new Employee("T1", "800", "Model-101", "Терминатор"),
                new Employee("Kyle1", "Reese", "+79263386351", "Отец Джона"),
                new Employee("John2", "Connor", "+79773522765", "Спаситель Человечества"),
                new Employee("Sarah2", "Connor", "+7995017543", "Мать Джона"),
                new Employee("T2", "800", "Model-101", "Терминатор"),
                new Employee("Kyle2", "Reese", "+79263386351", "Отец Джона"),
                new Employee("John3", "Connor", "+79773522765", "Спаситель Человечества"),
                new Employee("Sarah3", "Connor", "+7995017543", "Мать Джона"),
                new Employee("T3", "800", "Model-101", "Терминатор"),
                new Employee("Kyle3", "Reese", "+79263386351", "Отец Джона"),
                new Employee("John4", "Connor", "+79773522765", "Спаситель Человечества"),
                new Employee("Sarah4", "Connor", "+7995017543", "Мать Джона"),
                new Employee("T4", "800", "Model-101", "Терминатор"),
                new Employee("Kyle4", "Reese", "+79263386351", "Отец Джона"),
            };
            //File.WriteAllText("EmployeesDataBase.json", JsonSerializer.Serialize(_employees));
            #endregion 
            IConsoleCommand currentStep = new MainMenuCommand();
            Console.CursorVisible = false;
            while (true)
            {
                currentStep.Functionality();

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    currentStep = currentStep.PrevCommand();
                }
                else
                {
                    currentStep = currentStep.Execute(key);
                }
            }
        }
    }
}