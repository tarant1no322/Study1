using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesConsoleCommand.DataController;

namespace EmployeesConsoleCommand.ConsoleCommands
{
    internal class RemoveEmpCommand : IConsoleCommand
    {

        private readonly IConsoleCommand _prevCommand;
        private GenerateList _list;
        private IDataController _dataController = new SQLiteController();

        public RemoveEmpCommand(IConsoleCommand prevCommand)
        {
            _prevCommand = prevCommand;
            _list = new GenerateList();
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
            _dataController.Remove(guid);
            return new MainMenuCommand();
        }
        public IConsoleCommand PrevCommand() => _prevCommand;
    }
}
