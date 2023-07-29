using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefense
{
    /// <summary>
    /// Параметры турели.
    /// </summary>
    [CreateAssetMenu]
    public sealed class EnemyWaveProperties : ScriptableObject
    {
        /// <summary>
        /// Количество одновременно генерируемых объектов.
        /// </summary>
        [SerializeField] public int WaveSize;

        /// <summary>
        /// Количество одновременно генерируемых объектов.
        /// </summary>
        [SerializeField] public int CountSpawns;

        /// <summary>
        /// Период генерации.
        /// </summary>
        [SerializeField] public float SpawnPeriod;

        /// <summary>
        /// Массив свойств сущностей для генерации.
        /// </summary>
        [SerializeField] public EnemyProperties[] EnemiesProperties;

        /// <summary>
        /// Пауза перед следующей волной.
        /// </summary>
        [SerializeField] public float PauseToNextWave;
    }
}
