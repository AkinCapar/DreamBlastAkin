using Zenject;

namespace DreamBlast.Installer
{
    public class GameSignalsInstaller :Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<SwitchedToGameplayScreenSignal>().OptionalSubscriber();
            Container.DeclareSignal<LevelCompletedSignal>().OptionalSubscriber();
            Container.DeclareSignal<NoBubblesLeftToBlastSignal>().OptionalSubscriber();
        }
    }
}
