using System;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using Zenject;
#endif

namespace RoR
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool attack;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        [SerializeField] private bool cursorLocked = true;
        [SerializeField] private bool cursorInputForLook = true;
        [SerializeField] private bool canMove = true;

        public event Action OnAttackInput;
        public event Action OnStepInput;

        private ThirdPersonController _thirdPersonController;
        private BattleSystem _battleSystem;
        private ManagerChangeToStep _managerChangeToStepFight;

        [Inject]
        public void Construct(ThirdPersonController thirdPersonController, BattleSystem battleSystem, ManagerChangeToStep managerChangeToStepFight)
        {
            _thirdPersonController = thirdPersonController;
            _battleSystem = battleSystem;
            _managerChangeToStepFight = managerChangeToStepFight;
        }

        private void OnEnable()
        {
            _managerChangeToStepFight.OnChangeToStepFight += LockControls;
            _battleSystem.OnEndBattle += UnlockControls;
        }

        private void OnDisable()
        {
            _managerChangeToStepFight.OnChangeToStepFight -= LockControls;
            _battleSystem.OnEndBattle -= UnlockControls;
        }

        private void LockControls()
        {
            cursorInputForLook = false;
            canMove = false;
            look = new Vector2(0, 0);
            move = new Vector2(0, 0);
            sprint = false;
        }

        private void UnlockControls()
        {
            cursorInputForLook = true;
            canMove = true;
        }


#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            if (canMove) MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook) LookInput(value.Get<Vector2>());
        }

        public void OnJump(InputValue value) => JumpInput(value.isPressed);

        public void OnSprint(InputValue value)
        {
            if (canMove) SprintInput(value.isPressed);
        }

        public void OnAttack(InputValue value) => AttackInput(value.isPressed);

        public void OnStepFight(InputValue value) => StepInput(value.isPressed);

#endif
        private void StepInput(bool newAttackState)
        {
            if (newAttackState)
                OnStepInput?.Invoke();
        }

        private void AttackInput(bool newAttackState)
        {
            if (newAttackState && _thirdPersonController?.Grounded == true && !jump)
            {
                attack = true;
                OnAttackInput?.Invoke();
            }
            else
            {
                attack = false;
            }
        }

        private void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;

        private void LookInput(Vector2 newLookDirection) => look = newLookDirection;

        private void JumpInput(bool newJumpState) => jump = newJumpState;

        private void SprintInput(bool newSprintState) => sprint = newSprintState;

        private void OnApplicationFocus(bool hasFocus) => SetCursorState(cursorLocked);

        private void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}