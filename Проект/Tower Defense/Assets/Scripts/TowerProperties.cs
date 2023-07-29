using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefense
{
    public enum TurretType
    {
        /// <summary>
        /// Турель стреляет пулями.
        /// </summary>
        Bullets,
        /// <summary>
        /// Турель стреляет непрерывным лучом.
        /// </summary>
        Continious
    }

    [CreateAssetMenu]
    public class TowerProperties : ScriptableObject
    {
        /// <summary>
        /// Изображение башни на кнопке.
        /// </summary>
        [Header("GUI")]
        [SerializeField] public Sprite ButtonPicture;

        /// <summary>
        /// Стоимость покупки башни, монет.
        /// </summary>
        [Header("Игровые параметры")]
        [SerializeField] public int Cost = 15;
        /// <summary>
        /// Изображение башни на игровом поле.
        /// </summary>
        [SerializeField] public Sprite TowerPicture;

        /// <summary>
        /// Тип турели.
        /// </summary>
        [Header("Стрельба")]
        [SerializeField] public TurretType Type = TurretType.Bullets;

        /// <summary>
        /// Типы уничтожаемых целей.
        /// </summary>
        [SerializeField] public EnemyType EnemyType = EnemyType.All;

        /// <summary>
        /// Звуковой эффект выстрела из турели.
        /// </summary>
        [SerializeField] public AudioClip FireSfx;

        /// <summary>
        /// Шаблон снаряда.
        /// </summary>
        [Header("Снаряды")]
        [SerializeField] public Projectile ProjectilePrefab;

        /// <summary>
        /// Скорострельность турели, выстр./мин.
        /// </summary>
        [SerializeField] public float FireRate = 60;

        /// <summary>
        /// Рендерер для луча.
        /// </summary>
        [Header("Луч")]
        [SerializeField] public RayWeapon RayWeaponPrefab;
    }
}
