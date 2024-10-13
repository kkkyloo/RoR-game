using UnityEngine;
using Zenject;

namespace RoR
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;

        [SerializeField] private Vector3 _spawnPoint; 

        private DiContainer _diContainer;

        [SerializeField] private EnemySpawnData _enemySpawnData; 

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }


        private void OnEnable()
        {
         // подписка на статическое событие, которое будет вызываться в скрипте, лежащем на колайдере,
         // при входе в который будут спавниться враги. событие будет передавать скриптбл обжект с позициями
         // для спавна и префабами врагов   
        }

        private void OnDisable()
        {

        }



        [ContextMenu("Spawn Enemy Now")]
        public void SpawnEnemyNow()
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, 5);
            SpawnEnemy(spawnPosition);
        }

        private void SpawnEnemy(Vector3 spawnPosition)
        {
            for (int i = 0; i < _enemySpawnData._enemyPrefab.Count; i++)
            {
                _diContainer.InstantiatePrefab(_enemySpawnData.GetEnemyPrefab(i), _enemySpawnData.GetEnemyPosition(i), _enemySpawnData.GetEnemyRotation(i), null);

            }

       //     _diContainer.InstantiatePrefab(enemyPrefab, spawnPosition, Quaternion.identity, null);
        }
    }
}