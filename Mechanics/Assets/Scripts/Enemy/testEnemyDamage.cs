using UnityEngine;

namespace RoR
{
    public class testEnemyDamage : MonoBehaviour
    {

        private GameAgent x;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterStats>().ReceiveDamage(5, x);
            }
        }
    }
}
