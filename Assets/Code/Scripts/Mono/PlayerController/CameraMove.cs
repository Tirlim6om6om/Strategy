using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Code.Scripts.Mono.PlayerController
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
        [SerializeField] private Camera cam;
        [SerializeField] private LimitsSizeCamera limits;
        [SerializeField] private float scrollForce;
        [SerializeField] private TextMeshProUGUI text;

        private Touchscreen touchscreen;
        private float _startDist;
        private float _oldDist;
        private bool _scrollActive;

        private void Start()
        {
            Input.controller.Player.LeftClick.performed += context => PressLeftClickDown(context);
            Input.controller.Player.LeftClick.canceled += context => PressLeftClickDown(context);
            Input.controller.Player.Scroll.performed += context => Scroll(context);
#if PLATFORM_ANDROID
            touchscreen = Touchscreen.current;
#endif
        }

        private void PressLeftClickDown(InputAction.CallbackContext context)
        {
            if(touchscreen != null) return;
            leftButton = context.action.IsPressed();
            if(_scrollActive) return;

            if (leftButton)
            {
                StartCoroutine(MoveDelta());
            }
        }

        private IEnumerator MoveDelta()
        {
            while (leftButton)
            {
                Vector2 delta = -Input.button.Delta.ReadValue<Vector2>()
                    * sensivityDelta*(cam.orthographicSize/limits.maxSize)/10;
                transform.position += new Vector3(delta.x, 0, delta.y);
                yield return new WaitForFixedUpdate();
            }
        }

        private void Scroll(InputAction.CallbackContext context)
        {
            float size = cam.orthographicSize;
            
            if (context.ReadValue<float>() > 0)
            {
                size -= scrollForce;
            }
            if (context.ReadValue<float>() < 0)
            {
                
                size += scrollForce;
            }

            size = Mathf.Clamp(size, limits.minSize, limits.maxSize);
            cam.orthographicSize = size;
        }


#if PLATFORM_ANDROID
        private void FixedUpdate()
        {
            if(touchscreen==null) return;
            string str = touchscreen.touches[0].isInProgress + " : " + touchscreen.touches[1].isInProgress + "\n";
            text.SetText(str);
            str += _scrollActive+"\n";
            text.SetText(str);
            if (!touchscreen.touches[0].isInProgress || !touchscreen.touches[1].isInProgress)
            {
                str += "Reset\n";
                text.SetText(str);
                _startDist = 0;
                MoveTouch();
                return;
            }
            Vector2 touch_0 = touchscreen.touches[0].position.ReadValue();
            Vector2 touch_1 = touchscreen.touches[1].position.ReadValue();
            if (_startDist == 0)
            {
                _startDist = Vector2.Distance(touch_0, touch_1);
                _oldDist = 0;
            }
            float dist = Vector2.Distance(touch_0, touch_1) - _startDist - _oldDist;
            dist = dist / 100 * scrollForce;
            str += "DIST: " +dist + "\n";
            text.SetText(str);
            float size = cam.orthographicSize - dist;
            size = Mathf.Clamp(size, limits.minSize, limits.maxSize);
            cam.orthographicSize = size;
            _oldDist = Vector2.Distance(touch_0, touch_1)- _startDist;
        }

        private void MoveTouch()
        {
            TouchControl touch = Input.GetActiveTouch();
            if(touch == null) return;
            Vector2 delta = -touch.delta.value * sensivityDelta*(cam.orthographicSize/limits.maxSize)/10;
            transform.position += new Vector3(delta.x, 0, delta.y);
        }
        
#endif
    }

}