using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerController : MonoBehaviour
{
    [SerializeField] Camera m_mainCamera;

    public void Initialize()
    {
        m_mainCamera.gameObject.SetActive(true);
        GetComponent<PlayerInputHandler>().enabled = true;
        GetComponent<PlayerCharacterController>().enabled = true;
    }
}
