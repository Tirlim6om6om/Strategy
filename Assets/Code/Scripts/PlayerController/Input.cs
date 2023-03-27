using UnityEngine;

namespace Code.Scripts.PlayerController
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
    }
}
