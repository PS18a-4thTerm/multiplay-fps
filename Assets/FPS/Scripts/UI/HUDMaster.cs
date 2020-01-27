using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//作業者: 小川フィリップ優人
//HUDを一括管理するクラス
[RequireComponent(typeof(NotificationHUDManager), typeof(WeaponHUDManager), typeof(ObjectiveHUDManger))]
[RequireComponent(typeof(StanceHUD), typeof(CrosshairManager), typeof(JetpackCounter))]
[RequireComponent(typeof(PlayerHealthBar), typeof(FeedbackFlashHUD), typeof(FramerateCounter))]
[RequireComponent(typeof(DisplayMessageManager))]
public class HUDMaster : MonoBehaviour
{

	//初期化
	public void InitializeHUD(GameObject player)
	{
		var huds = FindObjectsOfType<MonoBehaviour>().OfType<HUDInterfaces.IHUDInitialize>();

		foreach (var hud in huds) hud.InitializeHUD(player);

	}

    public List<HUDInterfaces.IHUDUpdate> GetHUDUpdates() => FindObjectsOfType<MonoBehaviour>().OfType<HUDInterfaces.IHUDUpdate>().ToList();

}
