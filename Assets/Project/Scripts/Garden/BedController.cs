using UnityEngine;

namespace Project.Scripts.Garden
{
    public class BedController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer renderer;

        public Vector3 Size => renderer.bounds.size;
    }
}