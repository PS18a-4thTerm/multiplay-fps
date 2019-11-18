using UnityEngine;

//作成者: 小川フィリップ優人
//HUD共通のメソッドを実装するためのインターフェース
namespace HUDInterfaces
{
	
	//初期化処理を行う
	public interface IHUDInitialize
	{
		void InitializeHUD(GameObject player);
	}

	//毎フレームの処理を行う
	public interface IHUDUpdate
	{
		void UpdateHUD(in float deltaTime);
	}

}