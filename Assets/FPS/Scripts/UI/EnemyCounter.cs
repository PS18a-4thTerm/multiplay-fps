using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour, HUDInterfaces.IHUDUpdate
{
    [Header("Enemies")]
    [Tooltip("Text component for displaying enemy objective progress")]
    public Text enemiesText;

    EnemyManager m_EnemyManager;

    void Awake()
    {
        m_EnemyManager = FindObjectOfType<EnemyManager>();
        DebugUtility.HandleErrorIfNullFindObject<EnemyManager, EnemyCounter>(m_EnemyManager, this);
    }

    public void UpdateHUD(in float deltaTime)
    {
        enemiesText.text = m_EnemyManager.numberOfEnemiesRemaining + "/" + m_EnemyManager.numberOfEnemiesTotal;
    }
}
