using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.ESC;
using Code.Scripts.Mono.Interactable;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace Code.Scripts.Mono.UI
{
    public class InfoPanel : MonoBehaviour
    {
        public static InfoPanel instance;
        
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private HpController hpController;

        private World _world;
        private IEnumerator coroutine;
        private bool _active = false;

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

        private void Start()
        {
            _world = World.DefaultGameObjectInjectionWorld;
        }

        public void Reset()
        {
            panel.SetActive(false);
            if(coroutine != null)
                StopCoroutine(coroutine);
            _active = false;
        }

        public void Set(int hp, InfoIndex houseInfo)
        {
             panel.SetActive(true);
             hpController.Set(hp, houseInfo.maxHp);
             textName.SetText(houseInfo.name);
        }
        
        public void Set(Entity ent, InfoIndex houseInfo)
        {
            panel.SetActive(true);
            textName.SetText(houseInfo.name);
            _active = true;
            if(coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CheckHp(ent, houseInfo.maxHp);
            StartCoroutine(coroutine);
        }

        private IEnumerator CheckHp(Entity ent, int maxHp)
        {
            while (_active)
            {
                ClickableData data = _world.EntityManager.GetComponentData<ClickableData>(ent);
                hpController.Set(data.Hp, maxHp);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
