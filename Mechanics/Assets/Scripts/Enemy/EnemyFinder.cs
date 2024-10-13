using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RoR
{
    public class EnemyFinder : MonoBehaviour
    {
        [SerializeField] private float searchRadius = 10f;
        [SerializeField] private LayerMask searchLayerMask;
        [SerializeField] private List<EnemyStats> _foundCharacters = new();

        [SerializeField] private ManagerChangeToStep _managerChangeToStepFight;

        public event Action<List<EnemyStats>> OnEnemyFind;

        private bool search = false;


        [Inject]
        public void Construct(ManagerChangeToStep managerChangeToStepFight)
        {
            _managerChangeToStepFight = managerChangeToStepFight;
        }

        private void Start()
        {
            searchLayerMask = LayerMask.GetMask("Enemy");
        }

        private void OnEnable()
        {
            _managerChangeToStepFight.OnChangeToStepFight += FindCharactersInRadius;
        }

        private void OnDisable()
        {
            _managerChangeToStepFight.OnChangeToStepFight -= FindCharactersInRadius;
        }

        private void FindCharactersInRadius()
        {
            _foundCharacters.Clear();

            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, searchLayerMask);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<EnemyStats>(out var characterStats))
                {
                    _foundCharacters.Add(characterStats);
                }
            }
            if(_foundCharacters != null)
            {
                OnEnemyFind?.Invoke(_foundCharacters);
                search = false;
            }
            else
            {
                if (!search)
                {
                    FindCharactersInRadius();
                    search = true;
                }
                Debug.LogWarning("_foundCharacters != null");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }
    }
}
