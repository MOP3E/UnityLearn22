using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;

namespace TowerDefense
{
    /// <summary>
    /// Генератор волны противников.
    /// </summary>
    public class EnemiesSpawner : Spawner
    {

        /// <summary>
        /// Ппрефаб противника для генерации.
        /// </summary>
        [Header("Spawn Enemies Wave")]
        [SerializeField] private Entity _enemyPrefab;

        /// <summary>
        /// Массив свойств сущностей для генерации.
        /// </summary>
        [SerializeField] private EnemyProperties[] _enemiesProperties;

        /// <summary>
        /// Команда волны пешеходов, которые создаёт генератор.
        /// </summary>
        [SerializeField] private int _waveTeam;
        
        /// <summary>
        /// Тип поведения ИИ новых пешеходов.
        /// </summary>
        [SerializeField] private AiWalkerBehaviour _behaviour;

        /// <summary>
        /// Массив точек маршрута патрулирования.
        /// </summary>
        [SerializeField] private Transform[] _patrolRoute;

        /// <summary>
        /// Длины отрезков патрулируемого участка.
        /// </summary>
        private float[] _patrolRouteLengths;

        /// <summary>
        /// Точность выхода на точку патрулирования после чего пешеход нацелится на следующую точку.
        /// </summary>
        [SerializeField] private float _patrolRoutePrecision;

        private void Awake()
        {
            _patrolRouteLengths = new float[_patrolRoute.Length - 1];
            for (int i = 0; i < _patrolRouteLengths.Length; i++)
            {
                _patrolRouteLengths[i] = Vector2.Distance(_patrolRoute[i].position, _patrolRoute[i + 1].position);
            }
        }

        /// <summary>
        /// Размещение пешехода на игровом поле.
        /// </summary>
        protected override GameObject GenerateSpawnedEntity()
        {
            //создать пешехода
            GameObject entity = Instantiate(_enemyPrefab.gameObject);
            //задать команду пешехода
            Walker walker = entity.GetComponent<Walker>();
            walker.TeamId = _waveTeam;
            AiWalkerController controller = entity.GetComponent<AiWalkerController>();
            //настроить поведение пешехода
            controller.SetPatrolRoute(_patrolRoute, _patrolRouteLengths, _patrolRoutePrecision, _behaviour);

            //применение свойств врагу
            if (entity.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Use(_enemiesProperties[Random.Range(0, _enemiesProperties.Length)]);
            }

            return entity;
        }

        /// <summary>
        /// Перезапуск генератора.
        /// </summary>
        public void Restart(EnemyWaveProperties waveProperties)
        {
            _enemiesProperties = waveProperties.EnemiesProperties;
            Restart(waveProperties.WaveSize, waveProperties.CountSpawns, waveProperties.SpawnPeriod);
        }
    }
}
