using System;
using System.Collections.Generic;
using com.kodai100.ArtNetApp.Models;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Presenter
{
    public abstract class Presenter<T> : MonoBehaviour where T : Model
    {
        public abstract IEnumerable<IDisposable> Bind(T model);
    }
}
