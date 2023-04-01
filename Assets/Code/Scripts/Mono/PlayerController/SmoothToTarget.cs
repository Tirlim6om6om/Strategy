using UnityEngine;

namespace Code.Scripts.Mono.PlayerController
{
    public class SmoothToTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float force;
        
        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, force);
        }
    }

}