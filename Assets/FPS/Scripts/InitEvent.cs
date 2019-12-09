using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
public class InitEvent : MonoBehaviourPunCallbacks, IOnEventCallback
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code < 200)  // 200 以上はシステムで使われているので処理しない
        {
            if((int) e.Code == 0)
            {
                // イベントで受け取った内容をログに出力する
                string message = "send Event";/*"OnEvent. EventCode: " + e.Code.ToString() + ", Message: " + e.CustomData.ToString() + ", From: " + e.Sender.ToString()*/
                Debug.Log(message);
            }
            if ((int)e.Code == 1)
            {
                // イベントで受け取った内容をログに出力する
                string message = "send Event2";/*"OnEvent. EventCode: " + e.Code.ToString() + ", Message: " + e.CustomData.ToString() + ", From: " + e.Sender.ToString()*/
                Debug.Log(message);
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
