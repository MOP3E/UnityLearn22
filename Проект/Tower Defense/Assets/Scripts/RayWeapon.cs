using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Базовый класс для лучевого оружия башен.
    /// </summary>
    public abstract class RayWeapon : MonoBehaviour
    {
        /// <summary>
        /// Рендерер для лучач.
        /// </summary>
        [SerializeField] protected LineRenderer _lr;

        /// <summary>
        /// Урон, наносимый лучом в секунду.
        /// </summary>
        [SerializeField] protected float _firingRadius;

        /// <summary>
        /// Урон, наносимый лучом в секунду.
        /// </summary>
        public float FiringRadius => _firingRadius;

        /// <summary>
        /// Урон, наносимый лучом в секунду.
        /// </summary>
        [SerializeField] protected float _oneSecondsDamage;

        /// <summary>
        /// Урон, наносимый лучом в секунду.
        /// </summary>
        public float OneSecondsDamage => _oneSecondsDamage;

        public abstract void Render(Vector2 turret, Vector2 target);
        public abstract void Clear();
    }
}
