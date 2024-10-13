using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoR
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

    public class BattleSystem : MonoBehaviour
    {
        public BattleState state;

        private CharacterStats _currentPlayer;
        private EnemyStats _currentEnemyStats;
        private EnemySelector _currentSelectEnemy;
        private EnemyFinder _enemyFinder;
        private ManagerTeam _managerTeam;

        [SerializeField] private List<CharacterStats> _playerTeam = new();
        [SerializeField] private List<EnemyStats> _enemyTeam = new();

        public event Action OnEndBattle;

        public bool _isStepFight = false;
        private int _currentPlayerIndex = 0;

        [SerializeField] private Button _currentButton;
        [SerializeField] private Image _image;

        private TransformStepFight _transformStepFightData;

        ChangeTransformCharacter _transformClass;

        [Inject]
        public void Construct(EnemyFinder enemyFinder, TransformStepFight transformStepFightData, ManagerTeam managerTeam)
        {
            _enemyFinder = enemyFinder;
            _transformStepFightData = transformStepFightData;
            _managerTeam = managerTeam;
        }

        private void OnEnable() => _enemyFinder.OnEnemyFind += InitializeFight;
        private void OnDisable() => _enemyFinder.OnEnemyFind -= InitializeFight;

        private void InitializeFight(List<EnemyStats> enemyTeam)
        {
            _transformClass = new(_transformStepFightData);

            _playerTeam = _managerTeam.GetListTeam();


            _enemyTeam = enemyTeam;

            if (_enemyTeam.Count == 0)
            {
                Debug.LogError("No enemies found for the battle!");
                return;
            }

            _enemyTeam = enemyTeam;
            _isStepFight = true;
            state = BattleState.START;
            _transformClass.SaveStartTransform(_playerTeam);
            StartCoroutine(SetupBattle());
        }

        private IEnumerator SetupBattle()
        {
            if (state == BattleState.START)
            {
                state = BattleState.PLAYERTURN;
                StartCoroutine(DisableScreen());

                yield return new WaitForSeconds(1.5f);
                _transformClass.ChangeTransformFight(_playerTeam, _enemyTeam);
                SelectEnemy(_enemyTeam[0], _enemyTeam[0].GetComponent<EnemySelector>());
                StartPlayerTurn();
            }
        }

        private void StartPlayerTurn()
        {
            if (state != BattleState.PLAYERTURN) return;

            _currentPlayer = _playerTeam[_currentPlayerIndex];
            Debug.Log($"Ход: {_currentPlayer.name}");

            _currentButton.gameObject.SetActive(true);

            _currentPlayer.transform.LookAt(_currentEnemyStats.transform);
            _currentEnemyStats.transform.LookAt(_currentPlayer.transform);
        }

        public void SelectEnemy(EnemyStats _enemy, EnemySelector select)
        {
            if (_currentEnemyStats != null)
                _currentSelectEnemy.HighlightEnemy(false);

            _currentSelectEnemy = select;
            _currentEnemyStats = _enemy;
            select.HighlightEnemy(true);
            Debug.Log($"Выбран враг: {_currentEnemyStats.name}");
        }

        public void OnAttackButton()
        {
            if (state != BattleState.PLAYERTURN) return;

            _currentButton.gameObject.SetActive(false);
            StartCoroutine(PlayerAttack());
        }

        private IEnumerator DisableScreen()
        {
            _image.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            _image.gameObject.SetActive(false);

        }

        private IEnumerator PlayerAttack()
        {
            Debug.Log($"{_currentPlayer.name} атакует {_currentEnemyStats.name}!");
            yield return new WaitForSeconds(2f);

            bool isAlive = _currentEnemyStats.ReceiveDamage(_currentPlayer.Damage, _currentPlayer.CharacterAgent);

            if (!isAlive)
            {
                Debug.Log("Враг побежден!");
                _enemyTeam.Remove(_currentEnemyStats);

                if (_enemyTeam.Count == 0)
                {
                    state = BattleState.WON;
                    EndBattle();
                    yield break;
                }
                else
                {
                    SelectEnemy(_enemyTeam[0], _enemyTeam[0].GetComponent<EnemySelector>());
                }
            }

            if (_playerTeam.Count > 1 && _currentPlayerIndex != _playerTeam.Count - 1)
            {
                state = BattleState.PLAYERTURN;
                StartNextPlayerTurn();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        private IEnumerator EnemyTurn()
        {
            Debug.Log("Ход врагов!");

            if (_playerTeam.Count == 0)
            {
                state = BattleState.LOST;
                EndBattle();
                yield break;
            }

            for (int i = 0; i < _enemyTeam.Count; i++)
            {
                var currentEnemy = _enemyTeam[i];
                int randomPlayerIndex = UnityEngine.Random.Range(0, _playerTeam.Count);
                var randomPlayer = _playerTeam[randomPlayerIndex];

                Debug.Log($"{currentEnemy.name} атакует {randomPlayer.name}!");

                yield return new WaitForSeconds(2f);

                bool isAlive = randomPlayer.ReceiveDamage(currentEnemy.Damage, currentEnemy.CharacterAgent);

                if (!isAlive)
                {
                    Debug.Log($"{randomPlayer.name} погиб!");
                    _playerTeam.RemoveAt(randomPlayerIndex);

                    if (_playerTeam.Count == 0)
                    {
                        state = BattleState.LOST;
                        EndBattle();
                        yield break;
                    }
                }
            }

            state = BattleState.PLAYERTURN;
            StartNextPlayerTurn();
        }

        private void StartNextPlayerTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _playerTeam.Count;
            StartPlayerTurn();
        }

        private void EndBattle()
        {
            Debug.Log(state == BattleState.WON ? "Битва выиграна!" : "Битва проиграна...");
            _isStepFight = false;
            StartCoroutine(DisableScreen());

            _transformClass.ReturnStartTransform(_playerTeam);
            _transformClass = null;

            OnEndBattle?.Invoke();
        }
    }
}