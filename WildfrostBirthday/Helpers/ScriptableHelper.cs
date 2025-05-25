using System;
using UnityEngine;

namespace WildfrostBirthday.Helpers
{
    /// <summary>
    /// Helper class for creating scriptable objects with initialization
    /// </summary>
    public class Scriptable<T> where T : ScriptableObject, new()
    {
        readonly Action<T> modifier;
        public Scriptable() { }
        public Scriptable(Action<T> modifier) { this.modifier = modifier; }
        public static implicit operator T(Scriptable<T> scriptable)
        {
            T result = ScriptableObject.CreateInstance<T>();
            scriptable.modifier?.Invoke(result);
            return result;
        }
    }
}
