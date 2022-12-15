using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
    public class ChangeInput : MonoBehaviour
    {
        [SerializeField] private Selectable firSelectable;
    
        private EventSystem _system;  
        void Start()
        {
            _system = EventSystem.current;
            firSelectable.Select();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift)) {
                Selectable previous = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (previous != null){
                    previous.Select();
                }
            }else if (Input.GetKeyDown(KeyCode.Tab)) {
                Selectable next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next != null){
                    next.Select();
                }
            }
        }
    }
}
