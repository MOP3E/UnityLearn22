using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Canvas = UnityEngine.Canvas;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        /// <summary>
        /// Это промах мимо точки строительства.
        /// </summary>
        [SerializeField] private bool _missKlick;

        /// <summary>
        /// Нажатие на кнопку мыши 
        /// </summary>
        public static event EventHandler Click;

        public void OnPointerDown(PointerEventData eventData)
        {
            Click?.Invoke(this, _missKlick ? EventArgs.Empty : new EventArgsTransformValue { Value = transform.root });
        }

        private void Awake()
        {
            GetComponent<CanvasRenderer>().SetColor(Color.clear);
        }
    }
}
