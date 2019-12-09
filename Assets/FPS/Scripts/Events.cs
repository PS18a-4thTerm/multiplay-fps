using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
public class Events : MonoBehaviour
{
    [SerializeField] string m_message;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InitEvent()
    {
        //イベントとして送るものを作る
        byte eventCode = 0; // イベントコード 0~199 まで指定できる。200 以上はシステムで使われているので使えない。
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,  // 全体に送る 他に MasterClient, Others が指定できる
        };  // イベントの起こし方
        SendOptions sendOptions = new SendOptions(); // オプションだが、特に何も指定しない
       
        // イベントを起こす
        PhotonNetwork.RaiseEvent(eventCode, m_message, raiseEventOptions, sendOptions);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
