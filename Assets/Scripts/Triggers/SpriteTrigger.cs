using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrigger : MonoBehaviour
{
    [SerializeField] SpriteTriggerControl spriteTriggerControl;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            CubeControl cubeControl = other.GetComponent<CubeControl>();           
            spriteTriggerControl.CheckTriggerExit(cubeControl);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            CubeControl cubeControl = other.GetComponent<CubeControl>();
            spriteTriggerControl.CheckTriggerEnter(cubeControl);
        }
    }
}
