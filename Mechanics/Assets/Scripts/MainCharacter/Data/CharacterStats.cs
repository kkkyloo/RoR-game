using System;
using UnityEngine;
using Zenject;

namespace RoR
{
    public class CharacterStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health = 100f;
        public float Health => _health;

        [SerializeField] private float _damage = 50f;
        public float Damage => _damage;


        [SerializeField] private float _maxHealth = 100;
        private Animator _animator;

        //[SerializeField] private float _energystats;
        //[SerializeField] private float _ragestats;

        public event Action OnPlayerGetDamage;
        public GameAgent CharacterAgent;
        private BattleSystem _battleSystem;

        [Inject]
        public void Construct(BattleSystem battleSystem)
        {
            _battleSystem = battleSystem;
        }

        private void Awake() => InitializeComponents();

        private void InitializeComponents()
        {
            _animator = GetComponent<Animator>();
            CharacterAgent = GetComponent<GameAgent>();
        }

        public bool ReceiveDamage(float damageAmount, GameAgent sender)
        {
            if (sender.ObjectAgentType == GameAgent.Faction.Enemies)
            {
                _health = Mathf.Clamp(_health - damageAmount, 0, _maxHealth);

                Debug.Log($"{CharacterAgent} received {damageAmount} damage. Current HP: {_health}");

                PlayDamageEffects();
            }

            return CheckHealth();
        }

        private void PlayDamageEffects()
        {
            if (_animator != null) _animator.SetTrigger("Hit");
            if (CharacterAgent.ObjectAgentType == GameAgent.Faction.Player && !_battleSystem._isStepFight) 
                OnPlayerGetDamage?.Invoke();
        }

        private bool CheckHealth()
        {
            if (_health <= 0)
            {
                Death();
                return false;
            }
            else
                return true;
        }

        private void Death()
        {
            if (CharacterAgent.ObjectAgentType == GameAgent.Faction.Friend)
            {
                Debug.Log("Character has been destroyed!");
             //   Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player dead!");
                // окно перезапуска игры
            }

        }

        public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender)
        {

        }
    }
}
