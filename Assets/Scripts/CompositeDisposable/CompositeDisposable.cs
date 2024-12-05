using System;
using System.Collections.Generic;

public class CompositeDisposable
{
    private readonly Stack<IDisposable> _disposables = new();

    public void Dispose()
    {
        IDisposable disposable;
        do
        {
            lock (_disposables)
            {
                disposable = _disposables.Count > 0
                    ? _disposables.Pop()
                    : null;
            }
            disposable?.Dispose();
        } while (disposable != null);
    }

    public void Add(IDisposable disposable)
    {
        lock (_disposables)
        {
            _disposables.Push(disposable);
        }
    }
}