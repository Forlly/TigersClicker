using DefaultNamespace.Events;
using Project;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.TigersMechanic
{
    public class InputManager : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            EventBus.Instance.Send(new OnClickInputManager());
        }
    }
}