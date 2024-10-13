using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace RoR
{
    public class EnemyCountHit : MonoBehaviour
    {
        [SerializeField] private int _hitCount = 0;
        [SerializeField] private int _maxHitCount = 3;

        [SerializeField] private bool _canStepFight = false;
        private Coroutine _fightTimerCoroutine;
        [SerializeField] private float _delayClearHit = 4f;

        public static event Action OnChangeToStepFight;

        private StarterAssetsInputs _starterAssetsInputs;

        [Inject]
        public void Construct(StarterAssetsInputs starterAssetsInputs)
        {
            _starterAssetsInputs = starterAssetsInputs;
        }

        private void OnEnable()
        {
            if (_starterAssetsInputs != null)
                _starterAssetsInputs.OnStepInput += StepFightSwitcher;
            else
                Debug.Log("_starterAssetsInputs == null");
        }

        private void OnDisable()
        {
            if (_starterAssetsInputs != null)
                _starterAssetsInputs.OnStepInput -= StepFightSwitcher;
        }

        private IEnumerator FightTimer(float delay)
        {
            yield return new WaitForSeconds(delay);
            _hitCount = 0;
            _canStepFight = false;
            Debug.Log("hit reset after time");
        }

        public void EnemyGetHit()
        {
            _hitCount++;

            if (_fightTimerCoroutine != null)
                StopCoroutine(_fightTimerCoroutine);

            _fightTimerCoroutine = StartCoroutine(FightTimer(_delayClearHit));

            if (_hitCount >= _maxHitCount)
            {
                Debug.Log("Type X to STEP FIGHT");
                _canStepFight = true;
            }
        }

        private void StepFightSwitcher()
        {
            if (_canStepFight)
            {
                Debug.Log("CAN STEP FIGHT ENABLED");
                if (_fightTimerCoroutine != null)
                {
                    StopCoroutine(_fightTimerCoroutine);
                    _fightTimerCoroutine = null;

                    OnChangeToStepFight?.Invoke(); // тут не вызывается так как никто не подписан на энемит хит
                }
            }
            else
                Debug.Log("CANT STEP FIGHT");
        }
    }
}
