using UnityEngine;
using Zenject;

namespace RoR
{
    public class EnemySelector : MonoBehaviour
    {
        private Outline _outline;
        private BattleSystem _battleSystem;
        private EnemyStats _enemyStats;

        [Inject]
        public void Construct(BattleSystem _battleSystem)
        {
            this._battleSystem = _battleSystem;
        }

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _enemyStats = GetComponent<EnemyStats>();   

            if (_outline != null)         
                _outline.enabled = false;          
        }

        public void HighlightEnemy(bool highlight)
        {
            if (_outline != null)         
                _outline.enabled = highlight;           
        }

        private void OnMouseDown()
        {
            if (!_battleSystem._isStepFight) return;


            _battleSystem.SelectEnemy(_enemyStats, this);
        }
    }
}