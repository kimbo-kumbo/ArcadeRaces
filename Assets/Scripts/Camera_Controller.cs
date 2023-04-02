using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacePrototype
{
    public class Camera_Controller : MonoBehaviour
    {
        [SerializeField] private float _speedRotate;
        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.up * Time.deltaTime * _speedRotate);
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(-Vector3.up * Time.deltaTime * _speedRotate);
        }
    }
}