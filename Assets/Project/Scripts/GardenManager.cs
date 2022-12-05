using UnityEngine;

namespace Project.Scripts
{
    public class GardenManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnRoot;
        [SerializeField] private GameObject dirtPrefab;
        [SerializeField] private int width;
        [SerializeField] private int height;

        private void Awake()
        {
            
        }
    }
}