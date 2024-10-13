using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR
{
    [CreateAssetMenu(fileName = "Transform StepFight Data", menuName = "EnemySpawnData")]
    public class EnemySpawnData : ScriptableObject
    {
        public string Id = "";

        [SerializeField]
        private List<SpawnData> _enemyTransforms = new()
        {
            new SpawnData(new Vector3(26.99967f, -0.900315f, -1.8f), new Quaternion(0.002648f, 0.245308f, -0.00067f, 0.969441f)),
            new SpawnData(new Vector3(28.60012f, -0.900315f, -0.76f), new Quaternion(0.0027247f, -0.0702793f, 0.000191962f, 0.997524f)),
            new SpawnData(new Vector3(25.0f, -0.90032f, -0.8f), new Quaternion(0.0f, 0.417085f, 0.0f, 0.908867f))
        };

        [SerializeField] public List<GameObject> _enemyPrefab = new();

        public GameObject GetEnemyPrefab(int index)
        {
            if (index < 0 || index >= _enemyTransforms.Count)
            {
                Debug.LogWarning("Index out of range for enemy transforms.");
                return default;
            }
            return _enemyPrefab[index];
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }
        }

        public Vector3 GetEnemyPosition(int index)
        {
            if (index < 0 || index >= _enemyTransforms.Count)
            {
                Debug.LogWarning("Index out of range for enemy transforms.");
                return default;
            }
            return _enemyTransforms[index].Position;
        }

        public Quaternion GetEnemyRotation(int index)
        {
            if (index < 0 || index >= _enemyTransforms.Count)
            {
                Debug.LogWarning("Index out of range for enemy transforms.");
                return default;
            }
            return _enemyTransforms[index].Rotation;
        }
    }

    [System.Serializable]
    public class SpawnData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public SpawnData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }        
    }
}