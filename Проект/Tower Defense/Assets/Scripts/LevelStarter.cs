using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Запускает спавнеры уровня после расстановки игроком башен.
    /// </summary>
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GameObject[] _enemyCamps;

        /// <summary>
        /// Запуск уровня.
        /// </summary>
        public void LevelStart()
        {
            foreach (GameObject camp in _enemyCamps) camp.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
