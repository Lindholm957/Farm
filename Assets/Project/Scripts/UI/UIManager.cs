using Project.Scripts.Garden;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager I { get; private set; }

        [SerializeField] private GameObject mainMenuScreen;
        [SerializeField] private GameObject gameScreen;
        [Space]
        [SerializeField] private GameObject popUpPrefab;
        [SerializeField] private GameObject plantTimerPrefab;

        private void Awake()
        {
            I = this;
        }

        public void CreatePopUp(Vector3 worldPos, BedController parentBed)
        {
            var popUp = Instantiate(popUpPrefab, gameScreen.transform);
            popUp.GetComponent<UIPopUpController>().Init(parentBed);
            popUp.transform.position = Camera.main.WorldToScreenPoint(worldPos);
        }
        
        public PlantTimerController CreatePlantTimer(Vector3 worldPos, BedController parentBed)
        {
            var timer = Instantiate(plantTimerPrefab, gameScreen.transform);
            timer.transform.position = Camera.main.WorldToScreenPoint(worldPos);
            
            var timerController = timer.GetComponent<PlantTimerController>();
            
            return timerController;
        }
    }
}