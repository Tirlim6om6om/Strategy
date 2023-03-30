using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Code.Scripts.ESC
{
    public struct ClickableData : IComponentData
    {
        public int Type;
    }


    public class Clickable : MonoBehaviour
    {
        public int type;
    }

    public class ClickableBaker : Baker<Clickable>
    {
        public override void Bake(Clickable authoring)
        {
            AddComponent(new ClickableData {
                Type = authoring.type,
            });
        }
    }
}