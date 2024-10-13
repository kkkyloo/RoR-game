using UnityEngine;

namespace RoR
{
    public interface IDamageable
    {
        float Health { get; }
        bool ReceiveDamage(float damageAmount, GameAgent sender);
        void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender);
    }
}