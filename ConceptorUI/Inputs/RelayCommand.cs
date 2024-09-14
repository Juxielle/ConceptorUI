using System;
using System.Windows.Input;

namespace ConceptorUI.Inputs;

public interface IRelayCommand : ICommand
{
    public void RaiseCanExecuteChanged();
}

public class RelayCommand(Action<object> execute, Func<bool> canExecute = null!) : IRelayCommand
{
    private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return canExecute == null! || canExecute();
    }

    public void Execute(object? parameter)
    {
        _execute(parameter!);
    }

    public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
}