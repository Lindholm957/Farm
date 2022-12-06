using System.Collections;
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
        [SerializeField] private Transform plantTimerPos;

        private BedState _state = BedState.Empty;
        private Plant _curPlant;
        private GameObject _plantModel;
        private PlantTimerController _plantTimer;

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
            ShowPlantTimer();
            StartCoroutine(PlantGrowing());
        }
        
        private void ShowPlantTimer()
        {
            _plantTimer = UIManager.I.CreatePlantTimer(plantTimerPos.position, this);
            StartCoroutine(StartTimer());
        }

        private IEnumerator PlantGrowing()
        {
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
                _plantTimer.CurValue = Mathf.FloorToInt(timeLeft).ToString();
                yield return null;
            }
            _state = BedState.Ready;
            _plantTimer.Hide();
        }
    }
}