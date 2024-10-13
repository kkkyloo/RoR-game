//using System.Collections.Generic;   не нужен так как сделал статичным ивент вызова смены на пошаговый
//using UnityEngine;

//namespace RoR 
//{
//    public class EnemyManager : MonoBehaviour
//    {
//        public List<EnemyStats> enemyList = new();

//        private void Awake() => RegisterExistingEnemies();
        
//        public void RegisterEnemy(EnemyStats enemy)
//        {
//            if (!enemyList.Contains(enemy))
//            {
//                enemyList.Add(enemy);
//            }
//        }

//        public void UnregisterEnemy(EnemyStats enemy)
//        {
//            if (enemyList.Contains(enemy))           
//                enemyList.Remove(enemy);         
//        }

//        public void RegisterExistingEnemies()
//        {
//            EnemyStats[] existingEnemies = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);

//            foreach (EnemyStats enemy in existingEnemies)
//            {
//                RegisterEnemy(enemy);
//            }
//        }
//    }
//}
