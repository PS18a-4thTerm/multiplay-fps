using UnityEngine;
using UnityEngine.UI;
using HUDInterfaces;

public class StanceHUD : MonoBehaviour, IHUDInitialize
{
    [Tooltip("Image component for the stance sprites")]
    public Image stanceImage;
    [Tooltip("Sprite to display when standing")]
    public Sprite standingSprite;
    [Tooltip("Sprite to display when crouching")]
    public Sprite crouchingSprite;

	public void InitializeHUD(GameObject player)
	{
		PlayerCharacterController character = player.GetComponent<PlayerCharacterController>();
		DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, StanceHUD>(character, this);
		character.onStanceChanged += OnStanceChanged;

		OnStanceChanged(character.isCrouching);
	}

	void OnStanceChanged(bool crouched)
    {
        stanceImage.sprite = crouched ? crouchingSprite : standingSprite;
    }
}
