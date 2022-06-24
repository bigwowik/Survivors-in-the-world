using System;
using CodeBase.Stats;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour, IDestructable
    {
        public event Action<HeroDeath, GameObject> OnDeathEvent;


        public void OnDestruction(GameObject destroyer)
        {
            OnDeathEvent?.Invoke(this, destroyer);
            
            GetComponent<HeroEnabler>().EnablePlayer(false);
            //Destroy(gameObject);
        }

    }
}