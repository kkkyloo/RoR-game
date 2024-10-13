using UnityEngine;
using Zenject;

namespace RoR
{
    [RequireComponent(typeof(EnemyCountHit))]
    public class EnemyStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health = 100f;
        public float Health => _health;

        private Animator _animator;
        private EnemyCountHit _enemyCountHit;
        public GameAgent CharacterAgent;
        [SerializeField] private float _damage = 10f;
        public float Damage => _damage;
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
            _enemyCountHit = GetComponent<EnemyCountHit>();
            CharacterAgent = GetComponent<GameAgent>();
        }

        public bool ReceiveDamage(float damageAmount, GameAgent sender)
        {
            if (sender.ObjectAgentType != GameAgent.Faction.Enemies)
            {
                if (!_battleSystem._isStepFight) _enemyCountHit.EnemyGetHit();

                _health = Mathf.Max(_health - damageAmount, 0);
                PlayDamageEffects();

                Debug.Log($"Enemy got hit: -{damageAmount} from: {sender}. Current HP: {_health}");
            }

            return CheckHealth();
        }

        private void PlayDamageEffects()
        {
            if (_animator != null) _animator.SetTrigger("Hit");
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
            Debug.Log("Enemy has been defeated!");
            Destroy(gameObject);
        }

        public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender)
        {
        }
    }
}
