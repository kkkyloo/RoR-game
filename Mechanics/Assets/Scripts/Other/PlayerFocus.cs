using UnityEngine;

namespace RoR
{
    public class PlayerFocus : MonoBehaviour
    {
        public float rotationSpeed = 5f;
        public float focusDistance = 10f;
        public float moveSpeed = 5f; // Скорость движения персонажа вокруг врага
        public float strafeSpeed = 4f; // Скорость бокового движения
        public LayerMask enemyLayer;

        private Transform focusedEnemy;
        private CharacterController characterController;
        private bool isFocusing = false;  // Переменная для отслеживания состояния фокуса

        // Добавим ссылку на StarterAssetsInputs для получения движения
        private StarterAssetsInputs input;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();

            // Ищем скрипт StarterAssetsInputs на родительском объекте
            input = GetComponentInParent<StarterAssetsInputs>();
        }

        private void Update()
        {
            // Проверяем, удерживает ли игрок клавишу (например, "Q")
            if (Input.GetKeyDown(KeyCode.Q))
            {
                FocusOnClosestEnemy();  // Активируем фокусировку
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                ResetFocus();  // Сбрасываем фокус
            }

            if (isFocusing && focusedEnemy != null)
            {
                // Поворот игрока к врагу, если фокус активен
                RotateTowardsFocusedEnemy();

                // Двигаем персонажа вокруг врага
                MoveAroundFocusedEnemy();
            }
        }

        private void FocusOnClosestEnemy()
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, focusDistance, enemyLayer);

            if (enemiesInRange.Length > 0)
            {
                // Ищем ближайшего врага
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
                isFocusing = true;  // Активируем фокус
            }
        }

        private void RotateTowardsFocusedEnemy()
        {
            if (focusedEnemy == null) return;

            // Поворот игрока к врагу
            Vector3 directionToEnemy = (focusedEnemy.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToEnemy.x, 0, directionToEnemy.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        private void MoveAroundFocusedEnemy()
        {
            if (focusedEnemy == null || input == null) return;

            // Получаем движение игрока
            Vector2 moveInput = input.move;

            // Определяем направление движения относительно врага
            Vector3 directionToEnemy = (focusedEnemy.position - transform.position).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, directionToEnemy).normalized;  // Направление вправо

            // Двигаем персонажа в зависимости от ввода
            if (moveInput.y > 0)  // Вперёд
            {
                // Двигаем игрока вперёд к врагу
                Vector3 moveDirection = directionToEnemy;
                Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
                characterController.Move(movement);
            }
            else if (moveInput.y < 0)  // Назад
            {
                // Двигаем игрока назад, перпендикулярно врагу
                Vector3 moveDirection = -right;
                Vector3 movement = moveDirection * strafeSpeed * Time.deltaTime;
                characterController.Move(movement);
            }
            else if (moveInput.x != 0)  // Влево/вправо
            {
                // Двигаем игрока влево/вправо вокруг врага
                Vector3 moveDirection = (moveInput.x > 0 ? right : -right);  // Определяем сторону
                Vector3 movement = moveDirection * strafeSpeed * Time.deltaTime;
                characterController.Move(movement);
            }
        }

        private void ResetFocus()
        {
            focusedEnemy = null;
            isFocusing = false;  // Отключаем фокус
        }
    }
}
