using System;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyVisible : MonoBehaviour
    {
        private void OnBecameVisible()
        {
            Debug.Log($"{name} OnBecameVisible");
        }
        private void OnBecameInvisible()
        {
            Debug.Log($"{name} OnBecameInvisible");
        }
    }
}