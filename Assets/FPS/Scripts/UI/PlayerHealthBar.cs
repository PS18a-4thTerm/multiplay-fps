using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HUDInterfaces;

public class PlayerHealthBar : MonoBehaviour, IHUDInitialize
{
    [Tooltip("Image component dispplaying current health")]
    public Image healthFillImage;

    Health m_PlayerHealth;

	public void InitializeHUD(GameObject player)
	{
		PlayerCharacterController playerCharacterController = player.GetComponent<PlayerCharacterController>();
		DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, PlayerHealthBar>(playerCharacterController, this);

		m_PlayerHealth = playerCharacterController.GetComponent<Health>();
		DebugUtility.HandleErrorIfNullGetComponent<Health, PlayerHealthBar>(m_PlayerHealth, this, playerCharacterController.gameObject);
	}

	void Update()
    {
        // update health bar value
        healthFillImage.fillAmount = m_PlayerHealth.currentHealth / m_PlayerHealth.maxHealth;
    }
}
