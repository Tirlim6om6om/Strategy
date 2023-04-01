using System;
using Code.Scripts.ESC;
using Code.Scripts.Mono.Interactable;
using Code.Scripts.Mono.UI;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = Code.Scripts.Mono.PlayerController.Input;
using Ray = UnityEngine.Ray;

namespace Code.Scripts.Interactable
{
    public class RayController : MonoBehaviour
    {
        public static RayController instance;
        
        [SerializeField] private Camera cam;
        private ClickableInteractable lastClick;
        private World _world;
        private Entity _entity;
        private Entity _click;
        
        private void Start()
        {
            if (instance)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }
            Input.controller.Player.LeftClick.performed += context => PressLeftClickDown(context);
            _world = World.DefaultGameObjectInjectionWorld;
            _entity = Entity.Null;
        }

        private void PressLeftClickDown(InputAction.CallbackContext context)
        {
            Ray ray;
            ray = cam.ScreenPointToRay(Input.controller.Player.Pos.ReadValue<Vector2>());

            if (_world.IsCreated && !_world.EntityManager.Exists(_entity) && _entity == Entity.Null)
            {
                _entity = _world.EntityManager.CreateEntity();
                _world.EntityManager.AddBuffer<RayInput>(_entity);
            }

            RaycastInput input = new RaycastInput()
            {
                Start = ray.origin,
                Filter = CollisionFilter.Default,
                End = ray.GetPoint(cam.farClipPlane)
            };
            
            _world.EntityManager.GetBuffer<RayInput>(_entity).Add(new RayInput(){ Value = input});
        }

        public void SetClickable(Entity ent)
        {
            if (_world.EntityManager.HasComponent(ent, typeof(ClickableData)))
            {
                _click = ent;
                Vector3 pos = _world.EntityManager.GetComponentData<LocalToWorld>(ent).Position;
                ClickableData data = _world.EntityManager.GetComponentData<ClickableData>(ent);
                HouseInfo house = HouseInformation.instance.GetHouseIndex(data.Type);
                CanvasInfo.instance.Set(pos, house);
                InfoPanel.instance.Set(ent, house);
                Debug.Log(ent.Index + "=>" + data.Type);
            }
            else
            {
                CanvasInfo.instance.Set(false);
                InfoPanel.instance.Reset();
            }
        }


        private void DeactivateLast()
        {
            if (lastClick != null)
            {
                lastClick.Activate(false);
            }
        }

        private void OnDisable()
        {
            if (_world.IsCreated && _world.EntityManager.Exists(_entity))
            {
                _world.EntityManager.DestroyEntity(_entity);
            }
        }
    }

    public struct RayInput : IBufferElementData
    {
        public RaycastInput Value;
    }
}