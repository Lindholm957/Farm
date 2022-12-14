using System.Collections.Generic;
using Project.Scripts.Plants;
using UnityEngine;

namespace Project.Scripts.Garden
{
    public class GardenManager : MonoBehaviour
    {
        public static GardenManager I { get; private set; }

        [SerializeField] private Transform spawnRoot;
        [SerializeField] private GameObject bedPrefab;
        [SerializeField] private List<Plant> plantObjects;
        [Header("Size Properties")]
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float spaceBetweenBeds;

        private List<GameObject> _bedGameObjects = new List<GameObject>();
        public List<Plant> PlantObjects => plantObjects;

        private void Awake()
        {
            I = this;

            CreateGarden();
        }

        private void CreateGarden()
        {
            var bedSize = bedPrefab.GetComponent<BedController>().Size;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var instantiationPos = new Vector3(-bedSize.x * x - spaceBetweenBeds * x,
                        bedSize.y, -bedSize.z * y - spaceBetweenBeds * y);

                    var bed = Instantiate(bedPrefab, spawnRoot);
                    bed.transform.localPosition = instantiationPos;
                    
                    _bedGameObjects.Add(bed);
                } 
            }
        }
    }
}