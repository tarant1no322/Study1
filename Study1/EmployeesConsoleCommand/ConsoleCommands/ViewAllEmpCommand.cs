using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand.ConsoleCommands
{
    /// <summary>
    /// класс должен вызывать GetList после чего вызывать класс просмотра пользователя,
    ///     передавая в качестве параметра Guid Employee.
    ///     
    /// </summary>
    internal class ViewAllEmpCommand : IConsoleCommand
    {

        private readonly IConsoleCommand _prevCommand;
        private GenerateList _list;

        public ViewAllEmpCommand(IConsoleCommand prevCommand)
        {
            _prevCommand = prevCommand;
            _list = new GenerateList();
        }

        public void Functionality()
        {
            _list = new GenerateList();
            Print.PrintLogo("ViewEmployees");
            if (_list.IsListEmpty())
            {
                Console.WriteLine("\nСотрудников в базе нет!");
                return;
            }
            Console.WriteLine("\tВыберите сотрудника для просмотра детальной информации: \n");
            _list.PrintList();

        }
        public IConsoleCommand Execute(ConsoleKey key)
        {

            if (!_list.GetList().TryGetValue(key, out Guid guid))
            {
                if(key == ConsoleKey.DownArrow)
                    _list.PageDoun();
                else if(key == ConsoleKey.UpArrow)
                    _list.PageUp();
                _list = new GenerateList();
                return this;
            }
            return new ProfileEmpCommand(this, guid);
        }
        public IConsoleCommand PrevCommand() => _prevCommand;
    }
}
