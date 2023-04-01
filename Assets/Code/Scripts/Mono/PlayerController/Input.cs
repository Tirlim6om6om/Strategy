using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Code.Scripts.Mono.PlayerController
{
    public class Input : MonoBehaviour
    {
        public static MainController controller;
        public static MainController.PlayerActions button;
        
        private void Awake()
        {
            controller = new MainController();
            button = controller.Player;
        }
        
        private void OnEnable()
        {
            controller.Enable();
        }

        private void OnDisable()
        {
            controller.Disable();
        }
        
        public static TouchControl GetActiveTouch()
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.isInProgress)
                {
                    return touch;
                }
            }
            return null;
        }
    }
}
