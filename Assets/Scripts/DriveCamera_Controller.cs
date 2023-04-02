using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCamera_Controller : MonoBehaviour
{   
    public Transform targetRotate; 

    private void Update()
    {     
        transform.LookAt(targetRotate);        
    }
}