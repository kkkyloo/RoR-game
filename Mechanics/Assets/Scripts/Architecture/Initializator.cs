using UnityEngine;
using Zenject;

namespace RoR
{
    public class Initializator : MonoBehaviour
    {
   //     [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private BattleSystem _managerStepFight;
        [SerializeField] private CharacterStats _playerStats;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private ManagerChangeToStep _managerChangeToStep;
        [SerializeField] private GameAgent _playerAgent;
        [SerializeField] private EnemyFinder _enemyFinder;




        [Inject]
        public void Construct(ManagerChangeToStep _managerChangeToStepFight, EnemyFinder _enemyFinder)
        {
            //this._managerChangeToStepFight = _managerChangeToStepFight;
            //this._enemyFinder = _enemyFinder;


        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            //projectLinks._weapon?.Initialize();
            //projectLinks._playerStats?.Initialize();
            //projectLinks._colliderDisabler?.Initialize();
            //projectLinks._enemyHealthHit?.Initialize();
            //projectLinks._enemyManager.InitializeEnemies();
            //projectLinks._managerChangeToStepFight?.Initialize();
            //projectLinks._enemySpawner?.Initialize();


        }
    }
}
