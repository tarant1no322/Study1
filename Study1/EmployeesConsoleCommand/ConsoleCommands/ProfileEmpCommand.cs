using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesConsoleCommand.DataController;

namespace EmployeesConsoleCommand.ConsoleCommands
{
    internal class ProfileEmpCommand : IConsoleCommand
    {
        private Dictionary<ConsoleKey, IConsoleCommand> _supportNextCommand;
        private readonly IConsoleCommand _prevCommand;
        private readonly IDataController _dataController = new SQLiteController();
        private Guid _guid;
        private readonly List<Employee> _employees;
        public ProfileEmpCommand(IConsoleCommand prevCommand, Guid guid)
        {
            _prevCommand = prevCommand;
            _guid = guid;
            _employees = _dataController.GetData();
            
            _supportNextCommand = new Dictionary<ConsoleKey, IConsoleCommand>()
            {
                { ConsoleKey.D1, new EditEmpCommand(prevCommand, _guid, EmployeeFieldsEnum.FirstName) }, 
                { ConsoleKey.D2, new EditEmpCommand(prevCommand, _guid, EmployeeFieldsEnum.LastName) }, 
                { ConsoleKey.D3, new EditEmpCommand(prevCommand, _guid, EmployeeFieldsEnum.PhoneNumber) }, 
                { ConsoleKey.D4, new EditEmpCommand(prevCommand, _guid, EmployeeFieldsEnum.Description) }
            };
        }
        public void Functionality()
        {
            Employee currentEmployee = _employees.Find(x => x.Id == _guid)!;
            Print.PrintLogo("EmployeeProfile");
            Console.WriteLine(
            @$"
[1] Имя: {currentEmployee.FirstName}
[2] Фамилия: {currentEmployee.LastName}
[3] Телефон: {currentEmployee.PhoneNumber}
[4] Описание: {currentEmployee.Description}

Для редактирования данных нажмите соответствующую клавишу...
");
        }
        public IConsoleCommand Execute(ConsoleKey key)
        {
            if (!_supportNextCommand.TryGetValue(key, out var command))
            {
                return this;
            }
            return command;
        }

        public IConsoleCommand PrevCommand() => _prevCommand;

    }
}
