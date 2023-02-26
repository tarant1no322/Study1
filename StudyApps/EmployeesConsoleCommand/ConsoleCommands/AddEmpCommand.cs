using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand.ConsoleCommands
{
    internal class AddEmpCommand : IConsoleCommand
    {
        private IConsoleCommand _prevCommand;
        private List<Employee> _employees;
        private IDataController _dataController = new JsonController();
        public AddEmpCommand(IConsoleCommand prevCommand)
        {
            _prevCommand = prevCommand;
            _employees = _dataController.GetData();
        }
        public IConsoleCommand Execute(ConsoleKey key)
        {
            return new MainMenuCommand();
        }

        public void Functionality()
        {
            Print.PrintLogo("AddEmployee");
            string? FirstName = Print.InputString("Введите имя нового сотрудника: ");
            if (IsAddCancel(FirstName)) return;
            string? LastName = Print.InputString("Введите фамилию нового сотрудника: ");
            if (IsAddCancel(LastName)) return;
            string? PhoneNumber = Print.InputString("Введите номер телефона нового сотрудника: ");
            if (IsAddCancel(PhoneNumber)) return;
            string? Description = Print.InputString("Введите описание нового сотрудника: ");
            if (IsAddCancel(Description)) return;
            var tempEmp = new Employee(FirstName!, LastName!, PhoneNumber!, Description!);
            if (_employees == null || _employees.Count == 0)
                _employees = new List<Employee>() { tempEmp };
            else
                _employees.Add(tempEmp);
            _dataController.PushData(_employees);
            Console.WriteLine("\nСотрудник добавлен в базу!\nНажмите любую кнопку для возврата в меню...");
        }
        public IConsoleCommand PrevCommand() => _prevCommand;
        private bool IsAddCancel(string? data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                Console.WriteLine("\nОтмена ввода...\nНажмите любую клавишу для возврата...");
                return true;
            }
            return false;
        }
    }
}
