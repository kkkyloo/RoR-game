using System;
using UnityEngine;
using Zenject;

namespace RoR
{
    public class ManagerChangeToStep : MonoBehaviour
    {
        private CharacterStats _playerStats;
        private BattleSystem _managerStepFight;

        private bool _isStepFight = false;

        public event Action OnChangeToStepFight;

        [Inject]
        public void Construct(CharacterStats playerStats, BattleSystem managerStepFight)
        {
            _playerStats = playerStats;
            _managerStepFight = managerStepFight;
        }

        private void OnEnable()
        {
            if (_playerStats != null)
                _playerStats.OnPlayerGetDamage += OnEnemyStepFightTriggered;
            else
                Debug.LogWarning("_playerStats != null");

            _managerStepFight.OnEndBattle += OnStepFightEnded;
            EnemyCountHit.OnChangeToStepFight += OnEnemyStepFightTriggered;
        }

        private void OnDestroy()
        {
            if (_playerStats != null)
                _playerStats.OnPlayerGetDamage -= OnEnemyStepFightTriggered;

            _managerStepFight.OnEndBattle -= OnStepFightEnded;
            EnemyCountHit.OnChangeToStepFight -= OnEnemyStepFightTriggered;
        }

        private void OnEnemyStepFightTriggered()
        {
            if (_isStepFight || _managerStepFight == null) return;

            _isStepFight = true;
            OnChangeToStepFight?.Invoke();

            Debug.Log("Переход в пошаговый бой");
        }

        private void OnStepFightEnded()
        {
            _isStepFight = false;
        }
    }
}