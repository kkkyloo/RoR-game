using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace RoR
{
    public class CameraManager : MonoBehaviour
    {

        [SerializeField] private ManagerChangeToStep _managerChangeToStepFight;
        [SerializeField] private BattleSystem _managerStepFight;

        [SerializeField] private List<CinemachineCamera> _cameraList;
        [SerializeField] private int _index = 0;

        [Inject]
        public void Construct(ManagerChangeToStep managerChangeToStepFight, BattleSystem managerStepFight)
        {
            _managerChangeToStepFight = managerChangeToStepFight;
            _managerStepFight = managerStepFight;
        }

        private void OnEnable()
        {
            _managerChangeToStepFight.OnChangeToStepFight += ChangeCamera;
            _managerStepFight.OnEndBattle += ChangeCamera;
        }

        private void OnDisable()
        {
            _managerChangeToStepFight.OnChangeToStepFight -= ChangeCamera;
            _managerStepFight.OnEndBattle -= ChangeCamera;
        }

        private void ChangeCamera()
        {
            if (_cameraList != null && _cameraList.Count > 1)
            {
                _cameraList[_index].gameObject.SetActive(false);
                _index = (_index + 1) % _cameraList.Count;
                _cameraList[_index].gameObject.SetActive(true);
            }
        }
    }
}
