using System.Collections.Generic;
using Project.Scripts.Events.Base;
using Project.Scripts.Events.Systems;
using Project.Scripts.Garden;
using Project.Scripts.Plants;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class UIPopUpController : MonoBehaviour
    {
        [SerializeField] private Button carrotButton;
        [SerializeField] private Button grassButton;
        [SerializeField] private Button treeButton;

        private BedController _curBedController;
        private List<Plant> _plantObjects;

        private void Awake()
        {
            _plantObjects = GardenManager.I.PlantObjects;
            carrotButton.onClick.AddListener(delegate {OnSeedTapped(_plantObjects[0]); });
            grassButton.onClick.AddListener(delegate {OnSeedTapped(_plantObjects[1]); });
            treeButton.onClick.AddListener(delegate {OnSeedTapped(_plantObjects[2]); });
        }
        
        public void Init(BedController bedController)
        {
            _curBedController = bedController;
        }

        private void OnSeedTapped(Plant plantObject)
        {
            GlobalEventSystem.I.SendEvent(EventNames.Game.SeedHasChosen,
                new GameEventArgs(_curBedController.PlantPlace.gameObject));
            
            _curBedController.SetPlantSeed(plantObject);
            Close();
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            carrotButton.onClick.RemoveListener(delegate {OnSeedTapped(_plantObjects[0]); });
            grassButton.onClick.RemoveListener(delegate {OnSeedTapped(_plantObjects[1]); });
            treeButton.onClick.RemoveListener(delegate {OnSeedTapped(_plantObjects[2]); });
        }
    }
}