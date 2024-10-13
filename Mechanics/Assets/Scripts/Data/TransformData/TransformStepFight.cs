using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR
{
    [CreateAssetMenu(fileName = "Transform StepFight Data", menuName = "Transform")]
    public class TransformStepFight : ScriptableObject
    {
        public string Id = "";

        [SerializeField]
        private List<TransformData> _playerTransforms = new()
        {
            new TransformData(new Vector3(27.13f, -0.93f, 2.7f), new Quaternion(0.0f, 0.9996527f, 0.0f, -0.02635138f)),
            new TransformData(new Vector3(28.3f, -0.93f, 2.6f), new Quaternion(0.0f, 237.9f, 0.0f, -0.02635138f)),
            new TransformData(new Vector3(24.4f, -0.93f, 2.8f), new Quaternion(0.0f, 0.9996527f, 0.0f, -0.02635138f))
        };

        [SerializeField]
        private List<TransformData> _enemyTransforms = new()
        {
            new TransformData(new Vector3(26.99967f, -0.900315f, -1.8f), new Quaternion(0.002648f, 0.245308f, -0.00067f, 0.969441f)),
            new TransformData(new Vector3(28.60012f, -0.900315f, -0.76f), new Quaternion(0.0027247f, -0.0702793f, 0.000191962f, 0.997524f)),
            new TransformData(new Vector3(25.0f, -0.90032f, -0.8f), new Quaternion(0.0f, 0.417085f, 0.0f, 0.908867f))
        };

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }
        }

        // Методы для получения данных трансформаций игрока
        public Vector3 GetPlayerPosition(int index)
        {
            if (index < 0 || index >= _playerTransforms.Count)
            {
                Debug.LogWarning("Index out of range for player transforms.");
                return default;
            }
            return _playerTransforms[index].Position;
        }

        public Quaternion GetPlayerRotation(int index)
        {
            if (index < 0 || index >= _playerTransforms.Count)
            {
                Debug.LogWarning("Index out of range for player transforms.");
                return default;
            }
            return _playerTransforms[index].Rotation;
        }

        // Методы для получения данных трансформаций врага
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
    public class TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public TransformData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
