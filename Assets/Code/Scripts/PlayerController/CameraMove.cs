using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.PlayerController
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private float sensivityDelta;
        [SerializeField] private bool leftButton;

        private void Start()
        {
            Input.controller.Player.LeftClick.performed += context => PressLeftClickDown(context);
            Input.controller.Player.LeftClick.canceled += context => PressLeftClickDown(context);
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
                Vector2 delta = -Input.button.Delta.ReadValue<Vector2>()* sensivityDelta/10;
                transform.position += new Vector3(delta.x, 0, delta.y);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}