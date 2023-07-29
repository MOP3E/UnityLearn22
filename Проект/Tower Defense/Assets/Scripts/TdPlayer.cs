using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт игрока.
    /// </summary>
    public class TdPlayer : Player
    {
        /// <summary>
        /// Префаб башни.
        /// </summary>
        [SerializeField] private Tower _towerPrefab;

        public new static TdPlayer Instance => (TdPlayer)Player.Instance;
        
        /// <summary>
        /// Деньги игрока.
        /// </summary>
        [SerializeField] private int _money = 0;

        /// <summary>
        /// Деньги игрока.
        /// </summary>
        public int Money
        {
            get => _money;
            private set
            {
                _money = value;
                _moneyChanged?.Invoke(this, new EventArgsIntValue {Value = _money});
            }
        }

        /// <summary>
        /// Деньги игрока изменились.
        /// </summary>
        private static event EventHandler _moneyChanged;

        /// <summary>
        /// Подписка на событие изменения денег игрока.
        /// </summary>
        public static int MoneyChangedSubscribe(EventHandler handler)
        {
            _moneyChanged += handler;
            return Instance._money;
        }

        /// <summary>
        /// Изменение количества денег игрока. Возвращает истину если количество денег изменилось.
        /// </summary>
        public bool ChangeMoney(int money)
        {
            if (_money + money < 0) return false;
            Money += money;
            return true;
        }

        /// <summary>
        /// Попытка постройки игроком башни.
        /// </summary>
        /// <param name="towerProperties">Свойства башни.</param>
        /// <param name="buildSite">Позиция, в которой строится башня.</param>
        public bool TryBuild(TowerProperties towerProperties, Transform buildSite)
        {
            if(_money < towerProperties.Cost) return false;

            Tower tower = Instantiate(_towerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerProperties.TowerPicture;
            tower.Use(towerProperties);
            Money -= towerProperties.Cost;
            Destroy(buildSite.root.gameObject);
            return true;
        }
    }
}
