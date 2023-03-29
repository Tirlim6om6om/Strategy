using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ClickableData : IComponentData
{
    public char[] Name;
}


public class Clickable : MonoBehaviour
{
    [SerializeField]private char[] name;
}

public class ClickableBaker : Baker<Clickable>
{
    public override void Bake(Clickable authoring)
    {
        // AddComponent(new ClickableData {
        //     //Name = authoring.name
        // });
    }
}
