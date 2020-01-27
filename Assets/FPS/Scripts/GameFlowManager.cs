using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [Header("Parameters")]
    //[Tooltip("Duration of the fade-to-black at the end of the game")]
    [Tooltip("ゲーム終了時のフェード時間")]
    public float endSceneLoadDelay = 3f;
    [Tooltip("ゲーム終了時のフェードに使うCanvas")]
    public CanvasGroup endGameFadeCanvasGroup;

    [Header("Win")]
    [Tooltip("勝利シーン")]
    public string winSceneName = "WinScene";
    [Tooltip("Duration of delay before the fade-to-black, if winning")]
    public float delayBeforeFadeToBlack = 4f;
    [Tooltip("Duration of delay before the win message")]
    public float delayBeforeWinMessage = 2f;
    [Tooltip("Sound played on win")]
    public AudioClip victorySound;
    [Tooltip("Prefab for the win game message")]
    public GameObject WinGameMessagePrefab;

    [Header("Lose")]
    [Tooltip("This string has to be the name of the scene you want to load when losing")]
    public string loseSceneName = "LoseScene";


    public bool gameIsEnding { get; private set; }

    bool m_isMasterClient = false;
    PhotonView m_view;

    PlayerCharacterController m_Player;
    List<PlayerCharacterController> m_Players = new List<PlayerCharacterController>();
    NotificationHUDManager m_NotificationHUDManager;
    ObjectiveManager m_ObjectiveManager;
    float m_TimeLoadEndGameScene;
    string m_SceneToLoad;

    public Dictionary<int, GameObject> PlayerObjects = new Dictionary<int, GameObject>();


    void Start()
    {
        m_Player = FindObjectOfType<PlayerCharacterController>();
        //DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, GameFlowManager>(m_Player, this);

        m_ObjectiveManager = FindObjectOfType<ObjectiveManager>();
        DebugUtility.HandleErrorIfNullFindObject<ObjectiveManager, GameFlowManager>(m_ObjectiveManager, this);

        AudioUtility.SetMasterVolume(1);

        //PhotonViewの取得
        m_view = GetComponent<PhotonView>();
        m_isMasterClient = PhotonNetwork.IsMasterClient;
    }

    void Update()
    {
        //MasterClient以外では勝敗判定をしない
        if (!m_isMasterClient) return;

        if (gameIsEnding)
        {
            float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / endSceneLoadDelay;
            endGameFadeCanvasGroup.alpha = timeRatio;

            AudioUtility.SetMasterVolume(1 - timeRatio);

            // See if it's time to load the end scene (after the delay)
            if (Time.time >= m_TimeLoadEndGameScene)
            {
                SceneManager.LoadScene(m_SceneToLoad);
                gameIsEnding = false;
            }
        }
        else
        {
            //UpdatePlayersVariable();

            if (m_ObjectiveManager.AreAllObjectivesCompleted())
                m_view.RPC(nameof(EndGame), RpcTarget.All, true);

            // Test if player died
            if (0 < m_Players.Count && m_Players.All((x) => x.isDead))
                m_view.RPC(nameof(EndGame), RpcTarget.All, false);
        }
    }
    [PunRPC]
    void EndGame(bool win)
    {
        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Remember that we need to load the appropriate end scene after a delay
        gameIsEnding = true;
        endGameFadeCanvasGroup.gameObject.SetActive(true);
        if (win)
        {
            m_SceneToLoad = winSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // play a sound on win
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = victorySound;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
            audioSource.PlayScheduled(AudioSettings.dspTime + delayBeforeWinMessage);

            // create a game message
            var message = Instantiate(WinGameMessagePrefab).GetComponent<DisplayMessage>();
            if (message)
            {
                message.delayBeforeShowing = delayBeforeWinMessage;
                message.GetComponent<Transform>().SetAsLastSibling();
            }
        }
        else
        {
            m_SceneToLoad = loseSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay;
        }
    }
    [PunRPC]
    public void OnPlayerSpwned()
    {
        var players = FindObjectsOfType<PlayerCharacterController>().Select(x=>x.gameObject);
        foreach (var item in players)
        {
            int an= PhotonView.Get(item).Owner.ActorNumber;
            if (PlayerObjects.ContainsKey(an)) PlayerObjects[an] = item;
            else PlayerObjects.Add(an, item);
            Debug.Log($"OnSpawned - {an} , {item.name}");
        }
    }
    public void UpdatePlayersVariable()
    {
        m_Players = FindObjectsOfType<PlayerCharacterController>().ToList();
    }

    [PunRPC]
    public void OnPlayerSpwned()
    {
        var players = FindObjectsOfType<PlayerCharacterController>();
        foreach (var item in players)
        {
            Debug.Log(item.GetComponent<PlayerCharacterController>().enabled);
        }
    }
}
