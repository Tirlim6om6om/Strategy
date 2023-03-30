using UnityEngine;

namespace Code.Scripts.Mono.Interactable
{
    public class ClickableInteractable : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        
        public virtual void Activate(bool active)
        {
            menu?.SetActive(active);
        }
    }
}