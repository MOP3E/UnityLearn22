using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

namespace TowerDefense
{
    public class Tower : Destructible
    {
        /// <summary>
        /// Тип турели.
        /// </summary>
        private TurretType _type;

        /// <summary>
        /// Тип турели.
        /// </summary>
        public TurretType Type => _type;

        /// <summary>
        /// Типы уничтожаемых целей.
        /// </summary>
        private EnemyType _enemyType;

        /// <summary>
        /// Типы уничтожаемых целей.
        /// </summary>
        public EnemyType EnemyType => _enemyType;

        /// <summary>
        /// Скорость снаряда.
        /// </summary>
        public float ProjectileVelocity => _projectilePrefab.Velocity;

        /// <summary>
        /// Радиус поражения.
        /// </summary>
        public float FiringRadius
        {
            get
            {
                switch (_type)
                {
                    case TurretType.Bullets:
                        return _projectilePrefab.LifeRadius;
                    case TurretType.Continious:
                        return _rayWeaponPrefab.FiringRadius;
                }

                return 0;
            }
        }

        /// <summary>
        /// Префаб снаряда турели.
        /// </summary>
        private Projectile _projectilePrefab;

        /// <summary>
        /// Скорострельность турели, выстр./мин.
        /// </summary>
        private float _fireRate = 60;

        /// <summary>
        /// Звуковой эффект выстрела из турели.
        /// </summary>
        private AudioClip _fireSfx;

        /// <summary>
        /// Таймер перезарядки турели.
        /// </summary>
        private float _reloadTimer;

        /// <summary>
        /// Турель может выстрелить.
        /// </summary>
        public bool CanFire => _reloadTimer <= 0;

        private Collider2D _towerCollider;

        /// <summary>
        /// Цель для турели непрерывной стрельбы.
        /// </summary>
        public Destructible ContiniousTarget { get; set; }

        /// <summary>
        /// Префаб лучевого оружия.
        /// </summary>
        private RayWeapon _rayWeaponPrefab;

        /// <summary>
        /// Лучевое оружие.
        /// </summary>
        private RayWeapon _rayWeapon;

        /// <summary>
        /// Непрерывный урон.
        /// </summary>
        private float _oneSecondDamage;

        private Turret _turret;

        /// <summary>
        /// Start запускается перед первым кадром.
        /// </summary>
        protected override void Start()
        {
            base.Start();
            _indestrictible = true;
            Transform root = transform.root;
            _towerCollider = root.GetComponentInChildren<Collider2D>();
            _turret = GetComponentInChildren<Turret>();
            if (_type == TurretType.Continious && _rayWeaponPrefab != null)
            {
                _rayWeapon = Instantiate(_rayWeaponPrefab).GetComponent<RayWeapon>();
            }
        }

        /// <summary>
        /// Update запускается каждый кадр.
        /// </summary>
        private void Update()
        {
            //таймер перезарядки оружия
            if (_reloadTimer > 0) _reloadTimer -= Time.deltaTime;
        }

        /// <summary>
        /// Запускается 50 раз в секунду.
        /// </summary>
        private void FixedUpdate()
        {
            if (_type == TurretType.Continious && _rayWeapon != null)
            {
                if (ContiniousTarget != null)
                {
                    _rayWeapon.Render(_turret.transform.position, ContiniousTarget.transform.position);
                    ContiniousTarget.Hit(_rayWeapon.OneSecondsDamage * Time.fixedDeltaTime);
                }
                else
                {
                    _rayWeapon.Clear();
                }
            }
        }

        /// <summary>
        /// Стрельба из снарядной турели.
        /// </summary>
        public void Fire(Quaternion rotation)
        {
            if (_type != TurretType.Bullets || _reloadTimer > 0) return;

            //создать снаряд
            Projectile projectile = Instantiate(_projectilePrefab).GetComponent<Projectile>();

            //задать позицию и направление движения снаряда
            Transform projectileTransform = projectile.transform;
            Transform myTransform = transform;
            projectileTransform.position = myTransform.position;
            projectileTransform.rotation = rotation;

            //запретить столкновения между снарядом и кораблём
            projectile.ParentDestructible = this;

            //перезапустить таймер перезарядки
            _reloadTimer = 60 / _fireRate;

            //todo воспроизвести звук выстрела (домашнее задание)
        }

        /// <summary>
        /// Стрельба из лучевой турели.
        /// </summary>
        public void Fire(Vector3? target)
        {
            //Turret.transform.rotation = rotation;
            //_turret.Fire();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, FiringRadius);
        }

        /// <summary>
        /// Применение настроек к башне.
        /// </summary>
        public void Use(TowerProperties properties)
        {
            _type = properties.Type;
            _enemyType = properties.EnemyType;
            _projectilePrefab = properties.ProjectilePrefab;
            _fireRate = properties.FireRate;
            _fireSfx = properties.FireSfx;
            _rayWeaponPrefab = properties.RayWeaponPrefab;
        }
    }
}
