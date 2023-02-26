using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand.ConsoleCommands
{
    internal class DeleteEmpCommand : IConsoleCommand
    {

        private readonly IConsoleCommand _prevCommand;
        private GenerateList _list;
        private IDataController _dataController;

        public DeleteEmpCommand(IConsoleCommand prevCommand)
        {
            _prevCommand = prevCommand;
            _list = new GenerateList();
            _dataController = new JsonController();
        }

        public void Functionality()
        {
            Print.PrintLogo("DeleteEmployee");
            if (_list.IsListEmpty())
            {
                Console.WriteLine("\nСотрудников в базе нет!");
                return;
            }
            Console.WriteLine("\tВыберите сотрудника для удаления: \n"); 
            _list.PrintList();
        }
        public IConsoleCommand Execute(ConsoleKey key)
        {
            if (!_list.GetList().TryGetValue(key, out Guid guid))
            {
                if (key == ConsoleKey.DownArrow)
                    _list.PageDoun();
                if (key == ConsoleKey.UpArrow)
                    _list.PageUp();
                _list = new GenerateList();
                return this;
            }
            List<Employee> employees = _dataController.GetData();
            employees.Remove(employees.Find(x => x.Guid == guid)!);
            _dataController.PushData(employees);
            return new MainMenuCommand();
        }
        public IConsoleCommand PrevCommand() => _prevCommand;
    }
}
