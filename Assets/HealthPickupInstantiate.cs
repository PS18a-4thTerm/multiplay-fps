using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealthPickupInstantiate : MonoBehaviour
{
    [SerializeField] string m_healthPackPrefab = "";
    void Start()
    {
        StartCoroutine(WaitForConnect());
    }

    // Update is called once per frame
    void Update()
    {
        //if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        //{
        //    GameObject healthPack = PhotonNetwork.Instantiate(m_healthPackPrefab, this.transform.position, Quaternion.identity);
        //}
    }

    IEnumerator WaitForConnect()
    {
        while (!PhotonNetwork.IsConnected || !PhotonNetwork.IsMasterClient)
        {
            yield return null;
        }
        GameObject healthPack = PhotonNetwork.Instantiate(m_healthPackPrefab, this.transform.position, Quaternion.identity);
    }
}
