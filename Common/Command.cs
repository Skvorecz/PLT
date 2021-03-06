﻿using System;
using System.Windows.Input;

namespace Common
{
    public class Command : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public Command(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null 
                ? true
                : canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
