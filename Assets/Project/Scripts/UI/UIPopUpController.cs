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

        private void Awake()
        {
            carrotButton.onClick.AddListener(OnCarrotTapped);
            grassButton.onClick.AddListener(OnGrassTapped);
            treeButton.onClick.AddListener(OnTreeTapped);
        }
        
        private void OnCarrotTapped()
        {
            _curBedController.SetPlantSeed(Seed.Type.Carrot);
            Close();
        }
        
        private void OnGrassTapped()
        {
            _curBedController.SetPlantSeed(Seed.Type.Grass);
            Close();
        }

        private void OnTreeTapped()
        {
            _curBedController.SetPlantSeed(Seed.Type.Tree);
            Close();
        }
        
        public void Init(BedController bedController)
        {
            _curBedController = bedController;
        }


        public void Close()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            carrotButton.onClick.RemoveListener(OnCarrotTapped);
            grassButton.onClick.RemoveListener(OnGrassTapped);
            treeButton.onClick.RemoveListener(OnTreeTapped);
        }
    }
}