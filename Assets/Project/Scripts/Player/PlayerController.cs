using System;
using System.Collections;
using Project.Scripts.Events.Base;
using Project.Scripts.Events.Systems;
using Project.Scripts.Garden;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMesh;

        
        private State _curState = State.Awaiting;
        private Task _curTask = Task.Free;
        
        private Vector3 _startPos;
        private Vector3 _target;
        
        private float _destinationReachedTreshold = 0.5f;

        private BedController _curBedController;

        private const string Await = "Idle";
        private const string Run = "Run";
        private const string Plant = "Dig And Plant";
        private const string PickUpPlant = "Pull Plant";
        private const string MowPlant = "Mow Plant";

        private enum State
        {
            Awaiting,
            Running,
            Pulling,
            Planting
        }
        
        private enum Task
        {
            Free,
            Plant,
            Pull,
        }

        private void Awake()
        {
            _startPos = transform.localPosition;
            
            GlobalEventSystem.I.Subscribe(EventNames.Game.SeedHasChosen, OnSeedHasChosen);
            GlobalEventSystem.I.Subscribe(EventNames.Game.PullingPlantHasChosen, OnPullingPlantHasChosen);
        }

        private void OnSeedHasChosen(GameEventArgs arg0)
        {
            if (_curTask == Task.Free)
            {
                _curTask = Task.Plant;
                
                _curBedController = arg0.Sender;
                RunToTarget(_curBedController.PlantPlace.transform.position);   
            }
        }

        private void OnPullingPlantHasChosen(GameEventArgs arg0)
        {
            _curTask = Task.Pull;
            
            _curBedController = arg0.Sender;
            RunToTarget(_curBedController.PlantPlace.transform.position);
        }

        private void RunToTarget(Vector3 targetPos)
        {
            _curState = State.Running;
            
            animator.SetTrigger(Run);
            _target = targetPos;
            navMesh.SetDestination(_target);
        }
        
        private void Update()
        {
            if (_curState == State.Running)
            {
                float distanceToTarget = Vector3.Distance(transform.position, _target);
   
                if (distanceToTarget < _destinationReachedTreshold)
                {
                    if (_curTask == Task.Plant)
                    {
                        StartCoroutine(Planting());
                    } else if (_curTask == Task.Pull)
                    {
                        StartCoroutine(Pulling());
                    } else if (_curTask == Task.Free)
                    {
                        animator.SetTrigger(Await);
                    }
                }
            }
        }

        private IEnumerator Planting()
        {
            _curState = State.Planting;
            animator.SetTrigger(Plant);
            
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f)
            {
                yield return null;
            }
            _curTask = Task.Free;
            
            GlobalEventSystem.I.SendEvent(EventNames.Player.SeedWasPlanted,
                new GameEventArgs(_curBedController));

            RunToTarget(_startPos);
        }
        
        private IEnumerator Pulling()
        {
            _curState = State.Pulling;
            switch (_curBedController.PlantCollectionType)
            {
                case Plants.Plant.CollectionType.Mowing:
                    animator.SetTrigger(MowPlant);
                    break;
                case Plants.Plant.CollectionType.PickUp:
                    animator.SetTrigger(PickUpPlant);
                    break;
            }
            
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f)
            {
                yield return null;
            }
            
            _curTask = Task.Free;
            
            GlobalEventSystem.I.SendEvent(EventNames.Player.PlantWasPulled,
                new GameEventArgs(_curBedController));

            RunToTarget(_startPos);
        }

        private void OnDestroy()
        {
            GlobalEventSystem.I.Unsubscribe(EventNames.Game.SeedHasChosen, OnSeedHasChosen);
            GlobalEventSystem.I.Unsubscribe(EventNames.Game.PullingPlantHasChosen, OnPullingPlantHasChosen);
        }
    }
}