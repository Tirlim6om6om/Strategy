using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Code.Scripts.ESC
{
    public struct ClickableData : IComponentData
    {
        public int Type;
        public int Hp;
    }


    public class Clickable : MonoBehaviour
    {
        public int type;
        public int hp;
    }

    public class ClickableBaker : Baker<Clickable>
    {
        public override void Bake(Clickable authoring)
        {
            AddComponent(new ClickableData {
                Type = authoring.type,
                Hp = authoring.hp,
            });
        }
    }
}