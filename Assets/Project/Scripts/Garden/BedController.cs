using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Garden
{
    public class BedController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MeshRenderer renderer;

        public enum BedState
        {
            Empty,
            Busy,
            Ready
        }

        public Vector3 Size => renderer.bounds.size;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ShowBedActivities();
        }

        private void ShowBedActivities()
        {
            
        }
    }
}