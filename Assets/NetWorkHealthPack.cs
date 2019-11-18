using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Photon用の名前空間を参照
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkHealthPack : NetworkGameManager
{
    [SerializeField] Transform[] m_spawnPoint;
    [SerializeField] string m_healthPackPrefab = "";
    private void Start()
    {
        
    }

    private void SpawnHealthPack()
    {
        foreach (var spawnPoint in m_spawnPoint)
        {
            GameObject healthPack = PhotonNetwork.Instantiate(m_healthPackPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        SpawnHealthPack();
    }
}
