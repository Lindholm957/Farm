using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Garden
{
    public class GardenManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnRoot;
        [SerializeField] private GameObject bedPrefab;
        [Header("Size Properties")]
        [SerializeField] private int width;
        [SerializeField] private int height;

        private List<GameObject> _bedGameObjects = new List<GameObject>();

        private void Awake()
        {
            CreateGarden();
        }

        private void CreateGarden()
        {
            var dirtSize = bedPrefab.GetComponent<BedController>().Size;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var instantiationPos = new Vector3(-dirtSize.x * x, dirtSize.y, -dirtSize.z * y);

                    var bed = Instantiate(bedPrefab, spawnRoot);   

                    bed.transform.localPosition = instantiationPos;
                    
                    _bedGameObjects.Add(bed);
                } 
            }
        }
    }
}