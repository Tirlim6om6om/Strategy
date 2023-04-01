using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Mono.Interactable
{
    public class CanvasInfo : MonoBehaviour
    {
        public static CanvasInfo instance;
        
        [SerializeField] private GameObject obj;
        [SerializeField] private TextMeshProUGUI text;

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
        }

        public void Set(Vector3 pos, String str)
        {
            transform.position = pos;
            text.SetText(str);
            obj.SetActive(true);
        }

        public void Set(bool active)
        {
            obj.SetActive(false);
        }

        public void Set(Vector3 pos, HouseInfo house)
        {
            transform.position = new Vector3(pos.x, house.heightCanvas, pos.z);
            text.SetText(house.name);
            obj.SetActive(true);
        }
        
    }

}