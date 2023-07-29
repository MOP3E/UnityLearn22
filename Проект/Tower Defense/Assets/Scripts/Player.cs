using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт игрока.
    /// </summary>
    public class Player : MonoSingleton<Player>
    {
        /// <summary>
        /// Очки жизни игрока.
        /// </summary>
        [Header("Player")]
        [SerializeField] private int _livesCount;

        /// <summary>
        /// Очки жизни игрока.
        /// </summary>
        public int LivesCount
        {
            get => _livesCount;
            set
            {
                _livesCount = value;
                _livesCountChanged?.Invoke(this, new EventArgsIntValue { Value = _livesCount });
            }
        }

        /// <summary>
        /// Очки игрока изменились.
        /// </summary>
        private static event EventHandler _livesCountChanged;

        /// <summary>
        /// Подписка на событие изменения очков жизни игрока.
        /// </summary>
        public static int LivesCountChangedSubscribe(EventHandler handler)
        {
            _livesCountChanged += handler;
            return Instance._livesCount;
        }

        /// <summary>
        /// Космический корабль на сцене.
        /// </summary>
        [SerializeField] private Walker _walker;

        /// <summary>
        /// Космический корабль игрока на сцене.
        ///  </summary>
        public Walker ActiveWalker => _walker;

        /// <summary>
        /// Префаб взрыва.
        /// </summary>
        [SerializeField] private GameObject _explodePrefab;

        /// <summary>
        /// Контроллер управления движением.
        /// </summary>
        [SerializeField] private Controller _walkerController;

        private int _score;

        /// <summary>
        /// Очки игрока.
        /// </summary>
        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                _scoreChanged?.Invoke(this, new EventArgsIntValue { Value = _score });
            }
        }

        /// <summary>
        /// Очки игрока изменились.
        /// </summary>
        private static event EventHandler _scoreChanged;

        /// <summary>
        /// Подписка на событие изменения очков игрока.
        /// </summary>
        public static int ScoreChangedSubscribe(EventHandler handler)
        {
            _scoreChanged += handler;
            return Instance._score;
        }

        private int _kills;

        /// <summary>
        /// Количество убийств игрока.
        /// </summary>
        public int Kills
        {
            get => _kills;
            private set
            {
                _kills = value;
                KillsChanged?.Invoke(this, new EventArgsIntValue { Value = _kills });
            }
        }

        /// <summary>
        /// Событие изменения количества убийств игрока.
        /// </summary>
        public static event EventHandler KillsChanged;

        protected override void Awake()
        {
            base.Awake();

            if(_walker != null)
            {
                _walker.Destruction -= ShipDestructed;
                Destroy(_walker);
            }
        }

        /// <summary>
        /// Обработчик события разрушения корабля игрока.
        /// </summary>
        private void ShipDestructed(object go, EventArgs ea)
        {
            Vector3 position = ((GameObject)go).transform.position;
            LivesCount--;
            StartCoroutine(Respawn(position));
        }

        /// <summary>
        /// Взрыв старого и размещение нового корабля игрока на карте.
        /// </summary>
        private IEnumerator Respawn(Vector3 position)
        {
            //создать эффект взрыва корабля
            ParticleSystem explode = Instantiate(_explodePrefab, position, Quaternion.identity).GetComponent<ParticleSystem>();
            explode.Play();
            //ждать завершения работы эффекта взрыва
            yield return new WaitForSeconds(explode.main.duration);

            //разместить новый корабль игрока
            if(LivesCount > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            yield return null;
        }

        /// <summary>
        /// Размещение нового корабля игрока на карте.
        /// </summary>
        private void Respawn()
        {
            if (LevelSequenceController.PlayerWalker != null)
            {
                //создать новый корабль игрока
                GameObject newWalker = Instantiate(LevelSequenceController.PlayerWalker.gameObject);
                //получить ссылку на корабль
                _walker = newWalker.GetComponent<Walker>();
                //настроить камеру
                //_cameraContoller.Tagret = newShip.GetComponentInChildren<CameraTarget>().transform;
                //настроить управление кораблём
                _walker.Controller = _walkerController;
                //подписаться на событие разрушения корабля
                _walker.Destruction += ShipDestructed;
            }
        }

        /// <summary>
        /// Увеличение счётчика очков.
        /// </summary>
        public void ScoreAdd(int score)
        {
            Score += score;
        }

        /// <summary>
        /// Увеличение счётчика убийств.
        /// </summary>
        public void KillAdd(int count = 1)
        {
            Kills += count;
        }

        /// <summary>
        /// Получение игроком повреждения.
        /// </summary>
        public void TakeDamage(int damage)
        {
            LivesCount -= damage;
            if (LivesCount <= 0)
            {
                //LevelSequenceController.Instance.FinishCurrentLevel(false);
                LevelSequenceController.Instance.LevelRestart();
            }
        }
    }
}
