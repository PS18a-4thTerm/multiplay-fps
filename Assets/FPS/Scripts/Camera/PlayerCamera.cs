using UnityEngine;

namespace Multiplayer_Camera
{
    //作成者：小川フィリップ優人
    //プレイヤーのカメラ挙動
    public class PlayerCamera : MonoBehaviour
    {
        ////////////////フィールド////////////////
        
        //プレイヤーのカメラ
        private Camera m_playerCamera;

        //武器のカメラ
        private Camera m_weaponCamera;

        //参照するプレイヤーコントローラー
        private PlayerCharacterController m_playerController;

        //カメラを切り替えるために使う
        public enum eCameraMode
        {
            FPS,
            TPS
        }

        [SerializeField, Header("Default Camera Mode")]
        private eCameraMode m_cameraMode;

        ////////////////メソッド////////////////

        //初期化
        private void Awake()
        {
            m_playerCamera = this.gameObject.GetComponent<Camera>();
            m_weaponCamera = this.gameObject.GetComponent<Camera>();

            m_playerController = this.transform.parent.GetComponent<PlayerCharacterController>();
        }

        //カメラモードを切り替える
        // eCameraMode mode: カメラモードを指定する
        public void SwitchCameraMode(eCameraMode mode)
        {
            m_cameraMode = mode;
            //FPSに切り替わる場合の処理
            if(m_cameraMode == eCameraMode.FPS)
            {
                //武器カメラをアクティブ化する
                m_weaponCamera.gameObject.SetActive(true);
            }
            //TPSに切り替わる場合の処理
            else
            {
                //武器カメラは不要なので切る
                m_weaponCamera.gameObject.SetActive(false);
            }
        }


        ///PlayerCameraの挙動は基本的にPlayerChracterControllerのUpdateの中から呼ばれているため
        /// そこの処理をこちらのUpdateに移植する
        /// WeaponCameraは追っていくとUpdateで呼ばれているが、少し面倒なのでちょっと考えてから移植させる

        //カメラの回転を更新する
        public void UpdateCameraRotation()
        {
        }

        //カメラの座標を更新する
        public void UpdateCameraPosition()
        {
        }


    }

}