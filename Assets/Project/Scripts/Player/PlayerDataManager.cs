using Project.Scripts.Events.Base;
using Project.Scripts.Events.Systems;
using Project.Scripts.Plants;
using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerDataManager : MonoBehaviour
    {
        private float _expValue;
        private int _carrotValue;

        private void Awake()
        {
            GlobalEventSystem.I.Subscribe(EventNames.Plant.HasGrown, OnHasGrown);
            GlobalEventSystem.I.Subscribe(EventNames.Player.PlantWasPulled, OnPlantWasPulled);
        }

        private void OnHasGrown(GameEventArgs arg0)
        {
            _expValue += arg0.Sender.SelectedSeed.MaturationTime / 2;
            UIManager.I.ExperienceText = _expValue.ToString();
        }

        private void OnPlantWasPulled(GameEventArgs arg0)
        {
            var plantObject = arg0.Sender.SelectedSeed;

            if (plantObject.Type == Seed.Type.Carrot)
            {
                _carrotValue += 1;
                UIManager.I.CarrotsText = _carrotValue.ToString();
            }
            
        }
    }
}