using UnityEngine;

namespace RoR
{
    public class GameAgent : MonoBehaviour
    {
        public enum Faction
        {
            Player,
            Enemies,
            Friend
        }

        public Faction ObjectAgentType;
    }
}