using UnityEngine;
using UnityEngine.InputSystem;
using Input = Code.Scripts.PlayerController.Input;

namespace Code.Scripts.Interactable
{
    public class RayController : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        private ClickableInteractable lastClick;
        
        private void Start()
        {
            Input.controller.Player.LeftClick.performed += context => PressLeftClickDown(context);
        }

        private void PressLeftClickDown(InputAction.CallbackContext context)
        {
            Ray ray;

            // if (Touchscreen.current != null)
            // {
            //     ray = cam.ScreenPointToRay(Input.GetActiveTouch().position.value);
            //     point.transform.position = Input.GetActiveTouch().position.value;
            // }
            // else
            // {
            //     ray = cam.ScreenPointToRay(Input.controller.Player.Pos.ReadValue<Vector2>());
            // }
            ray = cam.ScreenPointToRay(Input.controller.Player.Pos.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                print(hit.collider.name);
                if (hit.collider.gameObject.TryGetComponent(out ClickableInteractable clicked))
                {
                    clicked.Activate(true);
                    if (lastClick != null)
                    {
                        if (lastClick != clicked)
                        {
                            lastClick.Activate(false);
                            lastClick = clicked;
                        }
                    }
                    else
                    {
                        lastClick = clicked;
                    }
                }
                else
                {
                    DeactivateLast();
                }
            }
            else
            {
                DeactivateLast();
            }
        }

        private void DeactivateLast()
        {
            if (lastClick != null)
            {
                lastClick.Activate(false);
            }
        }
    }
}