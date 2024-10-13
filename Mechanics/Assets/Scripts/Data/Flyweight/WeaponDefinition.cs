using System;
using UnityEngine;

namespace RoR
{
    [CreateAssetMenu(fileName = "Def WS Default", menuName = "Definitions/Battle/WeaponSpawnerDefiniton")]
    public class WeaponDefinition : ScriptableObject
    {

        [SerializeField] public string Id = "";
        [TextArea]
        [SerializeField] public string Name = "";
        [Range(0f, 5f)]
        public float Damage = 5f;






        private void OnValidate()
        {
            if (Id == "")
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}
