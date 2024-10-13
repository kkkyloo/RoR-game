using UnityEngine;
using Zenject;

namespace RoR

{
    public class CharacterWeapons : MonoBehaviour
    {
        private WeaponMele _currentWeapon;
        private StarterAssetsInputs _starterAssetsInputs;

        [Inject]
        public void Construct(WeaponMele weapon, StarterAssetsInputs starterAssetsInputs, GameAgent playerAgent)
        {
            _currentWeapon = weapon;
            _starterAssetsInputs = starterAssetsInputs;
            Initialize(playerAgent);
        }

        private void OnEnable()
        {
            if (_starterAssetsInputs != null)
                _starterAssetsInputs.OnAttackInput += FireWeapon;
            else
                Debug.LogError("StarterAssetsInputs (_links._input) is null.");
        }

        private void Initialize(GameAgent playerAgent)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Initialize(new DataWeaponExtrinsic
                {
                    GameAgent = playerAgent
                });
            }
        }

        private void OnDisable()
        {
            if (_starterAssetsInputs != null)
                _starterAssetsInputs.OnAttackInput -= FireWeapon;
        }

        public void FireWeapon()
        {
            if (_currentWeapon != null)
                _currentWeapon.AttackWeapon();
            else
                Debug.LogWarning("No current weapon to fire.");
        }
    }
}