using UnityEngine;
using Zenject;

namespace RoR
{
    public class WeaponColliderToggler : MonoBehaviour
    {
        private Collider _meleeCollider;

        [Inject]
        public void Construct(WeaponMele _mele)
        {
            GetCollider(_mele);
        }

        private void GetCollider(WeaponMele _mele)
        {
            _meleeCollider = _mele?.GetComponent<Collider>();

            if(_meleeCollider == null)
                Debug.LogWarning("WeaponMele not dound");
        }

        // для вызова в файле анимации
        public void SetWeaponColliderEnabled() => _meleeCollider.enabled = true;
        public void SetWeaponColliderDisable() => _meleeCollider.enabled = false;
    }
}