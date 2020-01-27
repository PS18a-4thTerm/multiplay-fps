using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LootHealthPickupInstance : MonoBehaviour
{
    [SerializeField] string m_lootHealthPackPrefab = "";
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
        Debug.Log("lootfirstHealDrop");
        while (!PhotonNetwork.IsConnected)
        {
            Debug.Log("lootwaitinghealDrop");
            yield return null;
        }
        GameObject healthPack = PhotonNetwork.Instantiate(m_lootHealthPackPrefab, this.transform.position, Quaternion.identity);
        Debug.Log("lootSecondhealDrop");
    }
}
