using System;
using Project.Scripts.Garden;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class PlantTimerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text textField;
        
        private string _curValue;

        public string CurValue
        {
            get
            {
                return _curValue;
            }
            set
            {
                _curValue = value;
            }
        }

        private void Update()
        {
            textField.text = _curValue;
        }

        public void Hide()
        {
            Destroy(gameObject);
        }
    }
}