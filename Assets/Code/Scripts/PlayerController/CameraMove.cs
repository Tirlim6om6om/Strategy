using System;
using System.Collections;
using System.Threading;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.PlayerController
{
    [System.Serializable]
    public class LimitsSizeCamera
    {
        [SerializeField] private float min;
        [SerializeField] private float max;
        
        public float minSize => min;
        public float maxSize => max;
    }
    
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private float sensivityDelta;
        [SerializeField] private bool leftButton;
        [SerializeField] private CinemachineVirtualCamera camera;
        [SerializeField] private LimitsSizeCamera limits;
        [SerializeField] private float scrollForce;
        

        private void Start()
        {
            Input.controller.Player.LeftClick.performed += context => PressLeftClickDown(context);
            Input.controller.Player.LeftClick.canceled += context => PressLeftClickDown(context);
            Input.controller.Player.Scroll.performed += context => Scroll(context);
        }

        private void PressLeftClickDown(InputAction.CallbackContext context)
        {
            leftButton = context.action.IsPressed();
            if (leftButton)
            {
                StartCoroutine(MoveDelta());
            }
        }

        private IEnumerator MoveDelta()
        {
            while (leftButton)
            {
                Vector2 delta = -Input.button.Delta.ReadValue<Vector2>()* sensivityDelta*(camera.m_Lens.OrthographicSize/limits.maxSize)/10;
                transform.position += new Vector3(delta.x, 0, delta.y);
                yield return new WaitForFixedUpdate();
            }
        }
        
        private void Scroll(InputAction.CallbackContext context)
        {
            float size = camera.m_Lens.OrthographicSize;
            
            if (context.ReadValue<float>() > 0)
            {
                size -= scrollForce;
            }
            if (context.ReadValue<float>() < 0)
            {
                
                size += scrollForce;
            }

            size = Mathf.Clamp(size, limits.minSize, limits.maxSize);
            camera.m_Lens.OrthographicSize = size;
        }

    }
}