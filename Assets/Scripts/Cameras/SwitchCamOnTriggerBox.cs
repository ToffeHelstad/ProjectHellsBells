using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamOnTriggerBox : MonoBehaviour
{
    //The camera that should be panned to
    public CinemachineVirtualCamera panCam;

    private void OnTriggerEnter(Collider other)
    {
        //Sets the pancam to higher priority than normal cam (normal is on 10)
        if (other.CompareTag("Sword")) panCam.Priority = 11;
    }

    private void OnTriggerExit(Collider other)
    {
        //Sets the pancam to lower priority than normal cam (normal is on 10)
        if (other.CompareTag("Sword")) panCam.Priority = 9;
    }
}
