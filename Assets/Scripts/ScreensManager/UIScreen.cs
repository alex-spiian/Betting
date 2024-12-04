using System;
using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    public event Action<string> Closed;
    public event Action<string> Opened;
    private string _guid;

    public abstract void Tick();

    public void Initialize(string guid)
    {
        _guid = guid;
    }

    public virtual void Open()
    {
        Opened?.Invoke(_guid);
        gameObject.SetActive(true);
    }
        
    public virtual void Close()
    {
        Closed?.Invoke(_guid);
        gameObject.SetActive(false);
    }
}