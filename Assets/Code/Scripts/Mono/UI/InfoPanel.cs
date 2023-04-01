using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Mono.Interactable;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Mono.UI
{
    public class InfoPanel : MonoBehaviour
    {
        public static InfoPanel instance;
        
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private HpController hpController;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
            Reset();
        }
        
        public void Reset()
        {
            panel.SetActive(false);
        }

        public void Set(int hp, InfoIndex houseInfo)
        {
             panel.SetActive(true);
             hpController.Set(hp, houseInfo.maxHp);
             textName.SetText(houseInfo.name);
        }
    }
}
