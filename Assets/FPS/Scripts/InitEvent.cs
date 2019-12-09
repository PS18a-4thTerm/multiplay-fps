using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
public class InitEvent : MonoBehaviourPunCallbacks, IOnEventCallback
{

    [SerializeField] Text m_Text;
    [SerializeField] int m_DelayTime = 2;
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
               m_Text.text = e.Sender.ToString()+"番目のプレイヤーが参加しました";
                StartCoroutine(HiddenText());
               // Debug.Log(message);
            }
            
        }
    }
    IEnumerator HiddenText()
    {
        yield return new WaitForSeconds(m_DelayTime);
        m_Text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
