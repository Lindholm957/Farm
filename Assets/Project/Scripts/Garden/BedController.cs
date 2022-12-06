using Project.Scripts.Plants;
using Project.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Garden
{
    public class BedController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private Transform plantPlace;

        private BedState _state = BedState.Empty;
        private Plant _curPlant;
        private GameObject _plantModel;

        public enum BedState
        {
            Empty,
            Busy,
            Ready
        }

        public Vector3 Size => renderer.bounds.size;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_state == BedState.Empty)
            {
              ShowSeeds();  
            } else if (_state == BedState.Ready)
            {
                
            }
        }

        private void ShowSeeds()
        {
            UIManager.I.CreatePopUp(transform.position, this);
        }

        public void SetPlantSeed(Plant plantObject)
        {
            SowBed(plantObject);
        }

        private void SowBed(Plant plantObject)
        {
            _state = BedState.Busy;
            _curPlant = plantObject;
            _plantModel = Instantiate(_curPlant.Models[0], plantPlace);
        }
    }
}