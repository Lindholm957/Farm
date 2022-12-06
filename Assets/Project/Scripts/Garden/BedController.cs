using System.Collections.Generic;
using Project.Scripts.Plants;
using Project.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Garden
{
    public class BedController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private List<Plant> plantsObjects;
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

        public void SetPlantSeed(Seed.Type type)
        {
            switch (type)
            {
                case Seed.Type.Carrot:
                    _curPlant = plantsObjects[0];
                    break;
                case Seed.Type.Grass:
                    _curPlant = plantsObjects[1];
                    break;
                case Seed.Type.Tree:
                    _curPlant = plantsObjects[2];
                    break;
            }
            SowBed();
        }

        private void SowBed()
        {
            _state = BedState.Busy;
            _plantModel = Instantiate(_curPlant.Models[0], plantPlace);
        }
    }
}