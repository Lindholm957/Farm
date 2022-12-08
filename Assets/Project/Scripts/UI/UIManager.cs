using Project.Scripts.Events.Base;
using Project.Scripts.Events.Systems;
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

        private void Awake()
        {
            I = this;
        }

        public void StartButtonClicked()
        {
            GlobalEventSystem.I.SendEvent(EventNames.Game.Started, new GameEventArgs(null));
            mainMenuScreen.SetActive(false);
            gameScreen.SetActive(true);
        }

        public void CreatePopUp(Vector3 worldPos, BedController parentBed)
        {
            var popUp = Instantiate(popUpPrefab, gameScreen.transform);
            popUp.GetComponent<UIPopUpController>().Init(parentBed);
            popUp.transform.position = Camera.main.WorldToScreenPoint(worldPos);
        }
    }
}