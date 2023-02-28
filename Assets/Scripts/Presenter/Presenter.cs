using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Presenter<T> : MonoBehaviour where T : Model
{
    public abstract IEnumerable<IDisposable> Bind(T model);
}