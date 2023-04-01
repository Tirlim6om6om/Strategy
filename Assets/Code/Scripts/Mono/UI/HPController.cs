using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Code.Scripts.Mono.UI
{
    public class HpController : MonoBehaviour
    {
        [SerializeField] private Image bar;
        [SerializeField] private TextMeshProUGUI text;


        public void Set(int hp, int maxHp)
        {
            if (hp < 0)
                hp = 0;
            bar.fillAmount = (float)hp / maxHp;
            text.SetText(hp+"/"+maxHp);
        }
    }

}