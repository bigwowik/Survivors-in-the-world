using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}