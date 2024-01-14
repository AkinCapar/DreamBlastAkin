using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DreamBlast.Installer
{
    public class GameSignalsInstaller :Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}
