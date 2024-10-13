using System.Collections.Generic;
using UnityEngine;

namespace RoR
{
    public class ChangeTransformCharacter
    {
        private List<Vector3> _playerPositions = new();
        private List<Quaternion> _playerRotations = new();
        private TransformStepFight _transformStepFightData;

        public ChangeTransformCharacter(TransformStepFight transformStepFightData)
        {
            _transformStepFightData = transformStepFightData;
        }

        public void SaveStartTransform(List<CharacterStats> _playerTeam)
        {
            _playerPositions.Clear();
            _playerRotations.Clear();

            foreach (var player in _playerTeam)
            {
                _playerPositions.Add(player.transform.position);
                _playerRotations.Add(player.transform.rotation);
            }
        }

        public void ReturnStartTransform(List<CharacterStats> _playerTeam)
        {
            for (int i = 0; i < _playerTeam.Count; i++)
            {
                Vector3 newPosition = _playerPositions[i];
                newPosition.y += 1.5f;

                if (i == 0)
                {
                    _playerTeam[i].gameObject.GetComponent<CharacterController>().enabled = false;
                    _playerTeam[i].gameObject.GetComponent<ThirdPersonController>().enabled = false;

                    _playerTeam[i].transform.SetPositionAndRotation(newPosition, _playerRotations[i]);
                    _playerTeam[i].gameObject.GetComponent<CharacterController>().enabled = true;
                    _playerTeam[i].gameObject.GetComponent<ThirdPersonController>().enabled = true;
                }
                else
                {
                    _playerTeam[i].transform.SetPositionAndRotation(newPosition, _playerRotations[i]);
                }
            }
        }

        public void ChangeTransformFight(List<CharacterStats> _playerTeam, List<EnemyStats> _enemyTeam)
        {
            if (_transformStepFightData != null)
            {
                for (int i = 0; i < _playerTeam.Count; i++)
                {
                    if (i == 0)
                    {
                        _playerTeam[i].gameObject.GetComponent<CharacterController>().enabled = false;
                        _playerTeam[i].gameObject.GetComponent<Rigidbody>().useGravity = false;
                        _playerTeam[i].gameObject.GetComponent<ThirdPersonController>().enabled = false;

                        _playerTeam[i].transform.SetPositionAndRotation(_transformStepFightData.GetPlayerPosition(i), _transformStepFightData.GetPlayerRotation(i));
                        _playerTeam[i].gameObject.GetComponent<CharacterController>().enabled = true;
                        _playerTeam[i].gameObject.GetComponent<Rigidbody>().useGravity = true;
                        _playerTeam[i].gameObject.GetComponent<ThirdPersonController>().enabled = true;
                    }
                    else
                    {
                        _playerTeam[i].transform.SetPositionAndRotation(_transformStepFightData.GetPlayerPosition(i), _transformStepFightData.GetPlayerRotation(i));
                    }
                }

                for (int i = 0; i < _enemyTeam.Count; i++)
                {
                    _enemyTeam[i].transform.SetPositionAndRotation(
                        _transformStepFightData.GetEnemyPosition(i),
                        _transformStepFightData.GetEnemyRotation(i)
                    );
                }
            }
        }
    }
}