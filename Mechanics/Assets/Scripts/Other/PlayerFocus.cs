using UnityEngine;

namespace RoR
{
    public class PlayerFocus : MonoBehaviour
    {
        public float rotationSpeed = 5f;
        public float focusDistance = 10f;
        public float moveSpeed = 5f; // �������� �������� ��������� ������ �����
        public float strafeSpeed = 4f; // �������� �������� ��������
        public LayerMask enemyLayer;

        private Transform focusedEnemy;
        private CharacterController characterController;
        private bool isFocusing = false;  // ���������� ��� ������������ ��������� ������

        // ������� ������ �� StarterAssetsInputs ��� ��������� ��������
        private StarterAssetsInputs input;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();

            // ���� ������ StarterAssetsInputs �� ������������ �������
            input = GetComponentInParent<StarterAssetsInputs>();
        }

        private void Update()
        {
            // ���������, ���������� �� ����� ������� (��������, "Q")
            if (Input.GetKeyDown(KeyCode.Q))
            {
                FocusOnClosestEnemy();  // ���������� �����������
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                ResetFocus();  // ���������� �����
            }

            if (isFocusing && focusedEnemy != null)
            {
                // ������� ������ � �����, ���� ����� �������
                RotateTowardsFocusedEnemy();

                // ������� ��������� ������ �����
                MoveAroundFocusedEnemy();
            }
        }

        private void FocusOnClosestEnemy()
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, focusDistance, enemyLayer);

            if (enemiesInRange.Length > 0)
            {
                // ���� ���������� �����
                float closestDistance = Mathf.Infinity;
                Transform closestEnemy = null;

                foreach (var enemyCollider in enemiesInRange)
                {
                    float distance = Vector3.Distance(transform.position, enemyCollider.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemyCollider.transform;
                    }
                }

                focusedEnemy = closestEnemy;
                isFocusing = true;  // ���������� �����
            }
        }

        private void RotateTowardsFocusedEnemy()
        {
            if (focusedEnemy == null) return;

            // ������� ������ � �����
            Vector3 directionToEnemy = (focusedEnemy.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToEnemy.x, 0, directionToEnemy.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        private void MoveAroundFocusedEnemy()
        {
            if (focusedEnemy == null || input == null) return;

            // �������� �������� ������
            Vector2 moveInput = input.move;

            // ���������� ����������� �������� ������������ �����
            Vector3 directionToEnemy = (focusedEnemy.position - transform.position).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, directionToEnemy).normalized;  // ����������� ������

            // ������� ��������� � ����������� �� �����
            if (moveInput.y > 0)  // �����
            {
                // ������� ������ ����� � �����
                Vector3 moveDirection = directionToEnemy;
                Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
                characterController.Move(movement);
            }
            else if (moveInput.y < 0)  // �����
            {
                // ������� ������ �����, ��������������� �����
                Vector3 moveDirection = -right;
                Vector3 movement = moveDirection * strafeSpeed * Time.deltaTime;
                characterController.Move(movement);
            }
            else if (moveInput.x != 0)  // �����/������
            {
                // ������� ������ �����/������ ������ �����
                Vector3 moveDirection = (moveInput.x > 0 ? right : -right);  // ���������� �������
                Vector3 movement = moveDirection * strafeSpeed * Time.deltaTime;
                characterController.Move(movement);
            }
        }

        private void ResetFocus()
        {
            focusedEnemy = null;
            isFocusing = false;  // ��������� �����
        }
    }
}
