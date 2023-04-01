using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Mono.Interactable
{
    [System.Serializable]
    public class InfoIndex
    {
        public int index;
        public string name;
        public int maxHp;
    }

    [System.Serializable]
    public class HouseInfo : InfoIndex
    {
        public float heightCanvas;
    }
    
    
    public class HouseInformation : MonoBehaviour
    {
        [SerializeField] private List<HouseInfo> houseInfos;
        public static HouseInformation instance;

        private void Awake()
        {
            if (instance)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }
        }

        public HouseInfo GetHouseIndex(int index)
        {
            foreach (var house in houseInfos)
            {
                if (house.index == index)
                {
                    return house;
                }
            }
            Debug.LogError("Not found house: " + index);
            return null;
        }
    }
}