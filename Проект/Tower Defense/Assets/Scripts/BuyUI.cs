using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuyUI : MonoBehaviour
    {
        private RectTransform _myTransform;

        private TowerByButton[] _buttons;

        private void Awake()
        {
            _myTransform = GetComponent<RectTransform>();
            _buttons = GetComponentsInChildren<TowerByButton>();
            BuildSite.Click += BuildSiteOnClick;
            gameObject.SetActive(false);
        }

        private void BuildSiteOnClick(object sender, EventArgs e)
        {
            if (e is EventArgsTransformValue et)
            {
                //разместить интерфейс покупки в заданной позиции
                _myTransform.anchoredPosition = Camera.main.WorldToScreenPoint(et.Value.position);
                //задать кнопкам покупки позицию постройки башни
                foreach (TowerByButton button in _buttons) button.BuildSite = ((BuildSite)sender).transform;
                //включить интерфейс покупки
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
