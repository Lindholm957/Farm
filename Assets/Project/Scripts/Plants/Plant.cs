using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Plants
{
    [CreateAssetMenu(fileName = "Plant Object", menuName = "Plant Object", order = 51)]
    public class Plant : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private List<GameObject> models;
        [Space] 
        [SerializeField] private float maturationTime;

        [SerializeField] private Collection collectionMethod;

        public enum Collection
        {
            PickUp,
            Mowing,
            Missing
        };
        
        public string Id => id;
        public List<GameObject> Models => models;
        public float MaturationTime => maturationTime;
        public Collection CollectionMethod => collectionMethod;
    }
}