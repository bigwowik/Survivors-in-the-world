using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Loading
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}