using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefense
{
    /// <summary>
    /// Параметры врага.
    /// </summary>
    [CreateAssetMenu]
    public sealed class EnemyProperties : ScriptableObject
    {
        /// <summary>
        /// Цвет спрайта.
        /// </summary>
        [Header("Внешний вид")]
        [SerializeField] public Color Color = Color.white;
        /// <summary>
        /// Масштраб спрайта.
        /// </summary>
        [SerializeField] public Vector2 SpriteScale = new Vector2(3, 3);
        /// <summary>
        /// Аниматор.
        /// </summary>
        [SerializeField] public RuntimeAnimatorController Animator;

        /// <summary>
        /// Скорость перемещения.
        /// </summary>
        [Header("Игровые параметры")]
        [SerializeField] public float MoveSpeed = 1;
        /// <summary>
        /// Очки жизни.
        /// </summary>
        [SerializeField] public int HitPoints = 15;
        /// <summary>
        /// Очки за уничтожение.
        /// </summary>
        [SerializeField] public int Score = 15;
        /// <summary>
        /// Размер коллайдера.
        /// </summary>
        [SerializeField] public float ColliderRadius = .2f;
        /// <summary>
        /// Тип врага.
        /// </summary>
        [SerializeField] public EnemyType EnemyType;
        /// <summary>
        /// Урон, наносимый врагом игроку.
        /// </summary>
        [SerializeField] public int Damage = 1;
        /// <summary>
        /// Монетки, передаваемые игроку после уничтожения врага.
        /// </summary>
        [SerializeField] public int Money = 1;
    }
}
