using System.Collections;
using Project.Scripts.Events.Base;
using Project.Scripts.Events.Systems;
using Project.Scripts.Plants;
using Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Garden
{
    public class BedController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private Transform plantPlace;
        [SerializeField] private TMP_Text plantTimer;

        private BedState _state = BedState.Empty;
        private Plant _curPlant;
        private GameObject _plantModel;
        private Plant _selectedSeed;

        public Transform PlantPlace => plantPlace;
        public Plant SelectedSeed => _selectedSeed;

        public enum BedState
        {
            Empty,
            Busy,
            Ready
        }

        public Vector3 Size => renderer.bounds.size;

        private void Awake()
        {
            GlobalEventSystem.I.Subscribe(EventNames.Player.SeedWasPlanted, OnSeedWasPlanted);
            GlobalEventSystem.I.Subscribe(EventNames.Player.PlantWasPulled, OnPlantWasPulled);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_state == BedState.Empty)
            {
              ShowSeeds();  
            } else if (_state == BedState.Ready)
            {
                if (_selectedSeed.CollectionMethod != Plant.CollectionType.Missing)
                {
                    GlobalEventSystem.I.SendEvent(EventNames.Game.PullingPlantHasChosen,
                        new GameEventArgs(this));
                }
            }
        }
        
        private void OnSeedWasPlanted(GameEventArgs arg0)
        {
            if (arg0.Sender == this)
            {
                StartGrowing();
            }
        }
        
        private void OnPlantWasPulled(GameEventArgs arg0)
        {
            if (arg0.Sender == this)
            {
                ClearBed();
            }
        }

        private void ShowSeeds()
        {
            UIManager.I.CreatePopUp(transform.position, this);
        }

        public void SetPlantSeed(Plant plantObject)
        {
            _selectedSeed = plantObject;
        }

        private void StartGrowing()
        {
            _state = BedState.Busy;
            
            _curPlant = _selectedSeed;
            
            StartCoroutine(PlantGrowing());
        }

        private void ClearBed()
        {
            _state = BedState.Empty;
            
            Destroy(_plantModel);
            _selectedSeed = null;
        }

        private IEnumerator PlantGrowing()
        {
            StartCoroutine(StartTimer());

            var growingPhaseNum = _curPlant.Models.Count;
            for (var i = 0; i < growingPhaseNum; i++)
            {
                yield return new WaitForSeconds(_curPlant.MaturationTime / growingPhaseNum);   

                if (_plantModel != null)
                {
                    Destroy(_plantModel);
                }
                _plantModel = Instantiate(_curPlant.Models[i], plantPlace);
            }
        }
        
        private IEnumerator StartTimer()
        {
            var timeLeft = _curPlant.MaturationTime;
            
            while (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                plantTimer.text = Mathf.FloorToInt(timeLeft).ToString();
                yield return null;
            }
            plantTimer.text = "";
            
            GlobalEventSystem.I.SendEvent(EventNames.Plant.HasGrown,
                new GameEventArgs(this));

            _state = BedState.Ready;
        }
    }
}