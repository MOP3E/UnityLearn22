using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerByButton : MonoBehaviour
    {
        /// <summary>
        /// Настройки башни.
        /// </summary>
        [SerializeField] private TowerProperties _towerProperties;

        /// <summary>
        /// Место, в котором должна быть построена башня.
        /// </summary>
        public Transform BuildSite { get; set; }

        /// <summary>
        /// Мой текст.
        /// </summary>
        private Text _myText;

        /// <summary>
        /// Моя кнопка.
        /// </summary>
        private Button _myButton;

        /// <summary>
        /// Мой интерфейс постройки башен.
        /// </summary>
        private BuyUI _myUI;

        private void Awake()
        {
            _myText = GetComponentInChildren<Text>();
            _myButton = GetComponentInChildren<Button>();
            _myUI = transform.parent.GetComponent<BuyUI>();
        }

        /// <summary>
        /// Start запускается перед первым кадром.
        /// </summary>
        private void Start()
        {
            _myText.text = _towerProperties.Cost.ToString();
            _myButton.GetComponent<Image>().sprite = _towerProperties.ButtonPicture;
            ButtonSwitch(TdPlayer.MoneyChangedSubscribe(TdPlayerOnMoneyChanged) >= _towerProperties.Cost);
        }

        private void TdPlayerOnMoneyChanged(object sender, EventArgs e)
        {
            if (e is EventArgsIntValue ei)
            {
                if (ei.Value >= _towerProperties.Cost != _myButton.interactable)
                {
                    ButtonSwitch(ei.Value >= _towerProperties.Cost);
                }
            }
        }

        private void ButtonSwitch(bool interactable)
        {
            _myButton.interactable = interactable;
            _myText.color = interactable ? Color.white : Color.red;
        }

        /// <summary>
        /// Покупка башни.
        /// </summary>
        public void Buy()
        {
            if(TdPlayer.Instance.TryBuild(_towerProperties, BuildSite)) _myUI.gameObject.SetActive(false);
        }
    }
}
