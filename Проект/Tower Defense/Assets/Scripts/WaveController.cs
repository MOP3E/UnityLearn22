using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class WaveController : MonoBehaviour
    {
        /// <summary>
        /// Массив свойств волны.
        /// </summary>
        [SerializeField] private EnemyWaveProperties[] _enemyWavesProperties;

        /// <summary>
        /// Спавнер, которым управляет контроллер.
        /// </summary>
        [SerializeField] private EnemiesSpawner _enemiesSpawner;

        /// <summary>
        /// Таймер паузы между волнами.
        /// </summary>
        private Timer _pauseTimer;

        private int _waveCounter;

        private void Awake()
        {
            _pauseTimer = new Timer(0);
            _waveCounter = 0;
        }

        /// <summary>
        /// Update запускается каждый кадр.
        /// </summary>
        private void Update()
        {
            //если волна окончена, но пауза по её окончании ещё нет - тикать таймером
            if (_enemiesSpawner.Complete && !_pauseTimer.IsDone) _pauseTimer.SubstractTime(Time.deltaTime);

            //если волна окончена и окончена пауза - запустить следующую волну
            if (_enemiesSpawner.Complete && _pauseTimer.IsDone && _waveCounter < _enemyWavesProperties.Length)
            {
                _enemiesSpawner.Restart(_enemyWavesProperties[_waveCounter]);
                _pauseTimer.Start(_enemyWavesProperties[_waveCounter].PauseToNextWave);
                _waveCounter++;
            }
        }
    }
}
