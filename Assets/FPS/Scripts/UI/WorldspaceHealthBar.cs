using UnityEngine;
using UnityEngine.UI;

public class WorldspaceHealthBar : MonoBehaviour, HUDInterfaces.IHUDUpdate
{
    [Tooltip("Health component to track")]
    public Health health;
    [Tooltip("Image component displaying health left")]
    public Image healthBarImage;
    [Tooltip("The floating healthbar pivot transform")]
    public Transform healthBarPivot;
    [Tooltip("Whether the health bar is visible when at full health or not")]
    public bool hideFullHealthBar = true;

    public void UpdateHUD(in float deltaTime)
    {
        // update health bar value
        healthBarImage.fillAmount = health.currentHealth / health.maxHealth;
        
        // rotate health bar to face the camera/player
        healthBarPivot.LookAt(Camera.main.transform.position);

        // hide health bar if needed
        if (hideFullHealthBar)
            healthBarPivot.gameObject.SetActive(healthBarImage.fillAmount != 1);
    }
}
