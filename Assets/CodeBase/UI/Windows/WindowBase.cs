using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        private void Awake() =>
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdate();
        }

        private void OnDestroy() =>
            CleanUp();

        protected virtual void OnAwake()
        {
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdate()
        {
        }

        protected virtual void CleanUp()
        {
        }

    }
}