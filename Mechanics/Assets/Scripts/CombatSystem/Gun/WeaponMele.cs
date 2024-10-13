using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RoR
{
    public class WeaponMele : MonoBehaviour, IWeapon
    {
        private Animator _playerAnimator;

        [SerializeField] private float _damageAmount = 5f; // делегировать в data данные

        private List<IDamageable> TargetsHit = new();
        private DataWeaponExtrinsic _dataWeaponExtrinsic;

        private bool _canAttack = true;

        [Inject]
        public void Construct(Animator playerAnimator)
        {
            _playerAnimator = playerAnimator;
        }

        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic)
        {
            _dataWeaponExtrinsic = dataWeaponExtrinsic;
        }

        public void AttackWeapon()
        {
            if (!_canAttack || GetCurrentState().IsName("Attack")) return;

            _playerAnimator.SetTrigger("Attack");
            StartCoroutine(HandleAttackDelay());
        }

        private IEnumerator HandleAttackDelay()
        {
            _canAttack = false;

            yield return new WaitForSeconds(GetCurrentState().length);

            _canAttack = true;
        }

        private AnimatorStateInfo GetCurrentState()
        {
            return _playerAnimator.GetCurrentAnimatorStateInfo(1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;

            if (other.TryGetComponent<IDamageable>(out var hitIDamage))
            {
                TargetsHit.Add(hitIDamage);
                Damage(_damageAmount, other, _dataWeaponExtrinsic.GameAgent);
            }
        }

        private void Damage(float damageAmount, Collider other, GameAgent sender)
        {
            foreach (var targetHit in TargetsHit)
            {
                targetHit.ReceiveDamage(damageAmount, sender);
            }

            TargetsHit.Clear();
        }
    }
}