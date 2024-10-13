using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RoR
{
    public class ManagerTeam : MonoBehaviour
    {
        public List<CharacterStats> _characterList;

        private CharacterStats _playerStats;

        [Inject]
        public void Construct(CharacterStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void Start()
        {
            RegisterExistingTeams();
        }

        private void RegisterExistingTeams()
        {
            _characterList.Add(_playerStats);

            CharacterStats[] existingTeams = FindObjectsByType<CharacterStats>(FindObjectsSortMode.None);

            foreach (CharacterStats character in existingTeams)
            {
                RegisterCharacter(character);
            }
        }
        private void RegisterCharacter(CharacterStats character)
        {
            if (!_characterList.Contains(character))      
                _characterList.Add(character);
        }

        public List<CharacterStats> GetListTeam()
        {
            return _characterList;
        }
    }
}
