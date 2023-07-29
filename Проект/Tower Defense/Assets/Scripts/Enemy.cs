using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Типы врагов.
    /// </summary>
    public enum EnemyType
    {
        /// <summary>
        /// Все доступные цели.
        /// </summary>
        All,
        /// <summary>
        /// Пешеход.
        /// </summary>
        Walker,
        /// <summary>
        /// Летающий враг.
        /// </summary>
        Flyer
    }

    public class Enemy : Walker
    {
        /// <summary>
        /// Тип врага.
        /// </summary>
        [SerializeField] private EnemyType _enemyType;

        /// <summary>
        /// Тип врага.
        /// </summary>
        public EnemyType EnemyType => _enemyType;

        /// <summary>
        /// Урон, наносимый врагом игроку.
        /// </summary>
        [SerializeField] private int _damage = 1;

        /// <summary>
        /// Монетки, передаваемые игроку после уничтожения врага.
        /// </summary>
        [SerializeField] private int _money = 1;

        /// <summary>
        /// Применение к врагу настроек.
        /// </summary>
        public new void Use(EnemyProperties properties)
        {
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

            //применение цвета
            sr.color = properties.Color;

            //применение размера
            sr.transform.localScale = new Vector3(properties.SpriteScale.x, properties.SpriteScale.y);

            //применение аниматора
            sr.GetComponent<Animator>().runtimeAnimatorController = properties.Animator;

            //применение размера коллайдера
            GetComponentInChildren<CircleCollider2D>().radius = properties.ColliderRadius;

            //применение урона игроку
            _damage = properties.Damage;

            //применение призовых монеток для игрока
            _money = properties.Money;

            //применение типа врага
            _enemyType = properties.EnemyType;

            base.Use(properties);
        }

        /// <summary>
        /// Нанесение урона игроку.
        /// </summary>
        public void PlayerDamage(object sender, EventArgs ea)
        {
            Player.Instance.TakeDamage(_damage);
        }

        /// <summary>
        /// Вручение игроку награды за убитого противника.
        /// </summary>
        public void PlayerRreward(object sender, EventArgs ea)
        {
            TdPlayer.Instance.ChangeMoney(_money);
        }
    }
}
