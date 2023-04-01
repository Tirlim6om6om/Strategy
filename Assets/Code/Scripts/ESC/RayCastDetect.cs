using System;
using Code.Scripts.Interactable;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace Code.Scripts.ESC
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    public partial struct RayCastDetect : ISystem
    {
        
        public void OnUpdate(ref SystemState state)
        {
            PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            foreach (var input in SystemAPI.Query<DynamicBuffer<RayInput>>())
            {
                foreach (var placementInput in input)
                {
                    if (physicsWorld.CastRay(placementInput.Value, out var hit))
                    {
                        RayController.instance.SetClickable(hit.Entity);
                    }
                }
                input.Clear();
            }
        }
    }
}