using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Command.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Command Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create user and let her compute
            var user = new User();

            // Issue several compute commands
            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);

            // Undo 4 commands
            user.Undo(4);

            // Redo 3 commands
            user.Redo(3);

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Command' interface
    /// </summary>
    interface ICommand
    {
        void Execute();
        void UnExecute();
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    class CalculatorCommand : ICommand
    {
        char @operator;
        int operand;
        Calculator calculator;

        // Constructor
        public CalculatorCommand(Calculator calculator, char @operator, int operand)
        {
            this.calculator = calculator;
            this.@operator = @operator;
            this.operand = operand;
        }

        // Sets operator
        public char Operator
        {
            set { @operator = value; }
        }

        // Sets operand
        public int Operand
        {
            set { operand = value; }
        }

        // Execute command
        public void Execute()
        {
            calculator.Operation(@operator, operand);
        }

        // Unexecute command
        public void UnExecute()
        {
            calculator.Operation(Undo(@operator), operand);
        }

        // Return opposite operator for given operator
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default: throw new
                  ArgumentException("@operator");
            }
        }
    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    class Calculator
    {
        int current = 0;

        // Perform operation for given operator and operand
        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': current += operand; break;
                case '-': current -= operand; break;
                case '*': current *= operand; break;
                case '/': current /= operand; break;
            }
            Console.WriteLine(
                "Current value = {0,3} (following {1} {2})",
                current, @operator, operand);
        }
    }

    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    class User
    {
        Calculator calculator = new Calculator();
        List<ICommand> commands = new List<ICommand>();
        int current = 0;

        // Redo original commands
        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);

            // Perform redo operations
            for (int i = 0; i < levels; i++)
            {
                if (current < commands.Count - 1)
                {
                    commands[current++].Execute();
                }
            }
        }

        // Undo prior commands
        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);

            // Perform undo operations
            for (int i = 0; i < levels; i++)
            {
                if (current > 0)
                {
                    commands[--current].UnExecute();
                }
            }
        }

        // Compute new value given operator and operand
        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            var command = new CalculatorCommand(calculator, @operator, operand);
            command.Execute();

            // Add command to undo list
            commands.Add(command);
            current++;
        }
    }
}
