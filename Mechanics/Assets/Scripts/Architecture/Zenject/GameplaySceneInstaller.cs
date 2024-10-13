using UnityEngine;
using Zenject;

namespace RoR
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private ThirdPersonController _thirdPersonController;
        [SerializeField] private WeaponMele _weaponMele;
        [SerializeField] private StarterAssetsInputs _starterAssetsInputs;
       // [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private CharacterStats _playerStats;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private ManagerChangeToStep _managerChangeToStep;
        [SerializeField] private GameAgent _playerAgent;
        [SerializeField] private EnemyFinder _enemyFinder;
        [SerializeField] private TransformStepFight _transformStepFightData;
        [SerializeField] private ManagerTeam _managerTeam;
        public override void InstallBindings()
        {
            Container.Bind<ThirdPersonController>().FromInstance(_thirdPersonController).AsSingle();
      //      Container.Bind<EnemyManager>().FromInstance(_enemyManager).AsSingle();
            Container.Bind<WeaponMele>().FromInstance(_weaponMele).AsSingle();
            Container.Bind<StarterAssetsInputs>().FromInstance(_starterAssetsInputs).AsSingle();
            Container.Bind<BattleSystem>().FromInstance(_battleSystem).AsSingle();
            Container.Bind<CharacterStats>().FromInstance(_playerStats).AsSingle();
            Container.Bind<Animator>().FromInstance(_playerAnimator).AsSingle();
            Container.Bind<ManagerChangeToStep>().FromInstance(_managerChangeToStep).AsSingle();
            Container.Bind<GameAgent>().FromInstance(_playerAgent).AsSingle();
            Container.Bind<EnemyFinder>().FromInstance(_enemyFinder).AsSingle();
            Container.Bind<TransformStepFight>().FromInstance(_transformStepFightData).AsSingle();
            Container.Bind<ManagerTeam>().FromInstance(_managerTeam).AsSingle();

        }
    }
}
