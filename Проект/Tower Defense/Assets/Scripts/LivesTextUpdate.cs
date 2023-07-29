using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class LivesTextUpdate : MonoBehaviour
    {
        private Text _myText;

        /// <summary>
        /// Start запускается перед первым кадром.
        /// </summary>
        private void Start()
        {
            _myText = GetComponentInChildren<Text>();
            _myText.text = Player.LivesCountChangedSubscribe(OnTextChanged).ToString();
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_myText != null) _myText.text = e.ToString();
        }
    }
}
