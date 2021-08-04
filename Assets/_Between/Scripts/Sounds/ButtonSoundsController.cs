using UnityEngine;
using UnityEngine.EventSystems;

namespace Between.Sounds
{
    public class ButtonSoundsController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField] private AudioClip _overlapClip;
        [SerializeField] private AudioClip _clickClip;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_clickClip != null)
                AudioSource.PlayClipAtPoint(_clickClip, Vector3.zero);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_overlapClip != null)
                AudioSource.PlayClipAtPoint(_overlapClip, Vector3.zero);
        }
    }
}