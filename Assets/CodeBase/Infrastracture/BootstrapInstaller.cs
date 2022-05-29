using CodeBase.Hero;
using Zenject;

namespace CodeBase.Infrastracture
{
    public class BootstrapInstaller : MonoInstaller
    {
        private IInputService inputService;

        public override void InstallBindings()
        {
            BingInputService();
        }

        private void BingInputService()
        {
            Container
                .Bind<IInputService>()
                .To<UnityInputService>()
                .AsSingle();
        }
    }
}