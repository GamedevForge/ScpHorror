using UnityEngine;
using Zenject;

namespace CoopHorror.CodeBase.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSpawner _playerSpawner;

        public override void InstallBindings()
        {
            Container.Bind<PlayerSpawner>().FromInstance(_playerSpawner).AsSingle();
        }
    }
}