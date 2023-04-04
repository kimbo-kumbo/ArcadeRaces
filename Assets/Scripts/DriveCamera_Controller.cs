using UnityEngine;

namespace RacePrototype
{
    public class DriveCamera_Controller : MonoBehaviour
    {
        [SerializeField] private Transform targetRotate;

        private void Update()
        {
            transform.LookAt(targetRotate);
        }
    }
}