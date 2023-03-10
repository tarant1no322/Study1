using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand.ConsoleCommands
{
    class MainMenuCommand : IConsoleCommand
    {
        private readonly Dictionary<ConsoleKey, IConsoleCommand> _supportNextCommand;
        public MainMenuCommand()
        {
            _supportNextCommand = new Dictionary<ConsoleKey, IConsoleCommand>()
            {
                { ConsoleKey.D1, new ViewAllEmpCommand(this) },
                { ConsoleKey.D2, new AddEmpCommand(this) },
                { ConsoleKey.D3, new RemoveEmpCommand(this) }
            };
        }
        public void Functionality()
        {
            Print.PrintLogo("Welcome");
            Print.PrintMenu(new string[] { "Список сотрудников", "Добавить сотрудника", "Удалить сотрудника" });
        }
        public IConsoleCommand Execute(ConsoleKey key)
        {
            if(!_supportNextCommand.TryGetValue(key, out var command))
            {
                return this;
            }
            return command;
        }

        public IConsoleCommand PrevCommand() => this;
    }
}
