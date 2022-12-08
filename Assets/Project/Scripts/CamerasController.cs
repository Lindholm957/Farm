using System;
using Cinemachine;
using Project.Scripts.Events.Base;
using Project.Scripts.Events.Systems;
using UnityEngine;

namespace Project.Scripts
{
    public class CamerasController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainMenuCam;
        [SerializeField] private CinemachineVirtualCamera gameCam;
        [SerializeField] private CinemachineVirtualCamera plantingCam;

        private void Awake()
        {
            GlobalEventSystem.I.Subscribe(EventNames.Game.Started, OnGameStarted);
            GlobalEventSystem.I.Subscribe(EventNames.Player.PlantingStarted, OnPlantingStarted);
            GlobalEventSystem.I.Subscribe(EventNames.Player.SeedWasPlanted, OnSeedWasPlanted);
        }

        private void OnGameStarted(GameEventArgs arg0)
        {
            mainMenuCam.gameObject.SetActive(false);
        }

        private void OnPlantingStarted(GameEventArgs arg0)
        {
            plantingCam.m_LookAt = arg0.Sender.PlantPlace;
            plantingCam.Follow = arg0.Sender.PlantPlace;
            
            gameCam.gameObject.SetActive(false);
        }
        
        private void OnSeedWasPlanted(GameEventArgs arg0)
        {
            gameCam.gameObject.SetActive(true);
        }
    }
}