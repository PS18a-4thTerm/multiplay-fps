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

        //カメラを切り替えるために使う
        public enum eCameraMode
        {
            FPS,
            TPS
        }

        [SerializeField, Header("Default Camera Mode")]
        private eCameraMode m_cameraMode;

        [Header("Rotation")]
        [SerializeField, Tooltip("Rotation speed for moving the camera")]
        private float m_rotationSpeed = 200f;
        [Range(0.1f, 1f)]
        [SerializeField, Tooltip("Rotation speed multiplier when aiming")]
        private float m_aimingRotationMultiplier = 0.4f;

        private bool m_isAiming;

        private float RotationMultiplier
        {
            get
            {
                if (m_isAiming)
                {
                    return m_aimingRotationMultiplier;
                }

                return 1f;
            }
        }

        [Header("Stance")]
        [SerializeField, Tooltip("Ratio (0-1) of the character height where the camera will be at")]
        private float m_cameraHeightRatio = 0.9f;

        private float m_cameraVerticalAngle = 0f;

        private float m_cameraHorizontalAngle = 0f;

        //プレイヤーの目の高さにあたるソケット
        [SerializeField]
        private Transform m_eyeSocket;

        [SerializeField]
        private float m_cameraDistance;

        public float GetFOV
        {
            get
            {
                return m_playerCamera.fieldOfView;
            }
        }

        ////////////////メソッド////////////////

        //初期化
        private void Awake()
        {
            m_playerCamera = this.gameObject.GetComponent<Camera>();
            m_weaponCamera = this.gameObject.GetComponent<Camera>();

            if(m_cameraDistance == 0f) m_cameraDistance = 10f;
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
                m_updateCameraPosition = updateFPSCameraPosition;
            }
            //TPSに切り替わる場合の処理
            else
            {
                //武器カメラは不要なので切る
                m_weaponCamera.gameObject.SetActive(false);
                m_updateCameraPosition = updateTPSCameraPosition;
            }
        }


        ///PlayerCameraの挙動は基本的にPlayerChracterControllerのUpdateの中から呼ばれているため
        /// そこの処理をこちらのUpdateに移植する
        /// WeaponCameraは追っていくとUpdateで呼ばれているが、少し面倒なのでちょっと考えてから移植させる

        //カメラの回転を更新する
        public void UpdateCameraRotation(in float horizontalAxis, in float verticalAxis)
        {
            // horizontal camera rotation
            // rotate the transform with the input speed around its local Y axis
            m_cameraHorizontalAngle += horizontalAxis * m_rotationSpeed * RotationMultiplier;

            this.transform.Rotate(new Vector3(0f, m_cameraHorizontalAngle, 0f), Space.Self);

            // vertical camera rotation
            // add vertical inputs to the camera's vertical angle
            m_cameraVerticalAngle += verticalAxis * m_rotationSpeed * RotationMultiplier;

            // limit the camera's vertical angle to min/max
            m_cameraVerticalAngle = Mathf.Clamp(m_cameraVerticalAngle, -89f, 89f);

            // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
            this.transform.localEulerAngles = new Vector3(m_cameraVerticalAngle, 0, 0);
        }

        //カメラの座標更新用のデリゲート
        private delegate void updateCameraPos(in PlayerCharacterController controller);
        private updateCameraPos m_updateCameraPosition;

        //カメラの座標を更新する
        public void UpdateCameraPosition(in PlayerCharacterController controller)
        {
            m_updateCameraPosition?.Invoke(controller);
        }

        private void updateFPSCameraPosition(in PlayerCharacterController controller)
        {
            if(m_eyeSocket != null)
            {
                this.transform.position = m_eyeSocket.position;
            }
        }

        private void updateTPSCameraPosition(in PlayerCharacterController controller)
        {
            if(m_eyeSocket)
            {
                //カメラの回転情報を利用して一を判定する
                var radHorizontal = m_cameraHorizontalAngle * Mathf.Deg2Rad;
                var radVertical = m_cameraVerticalAngle * Mathf.Deg2Rad;

                //水平角でxとzを確定。垂直角でyを確定
                var x = Mathf.Sin(radHorizontal);
                var z = Mathf.Cos(radHorizontal);
                var y = Mathf.Sin(radVertical);

                transform.position = m_eyeSocket.position + new Vector3(x, y, z) * -m_cameraDistance;
            }
        }

        //カメラの基準となる高さを更新する
        public void UpdateBaseHeight(in float targetHeight, in float crouchingSharpness, in bool force)
        {
            if (force) 
                this.transform.localPosition = Vector3.up * targetHeight * m_cameraHeightRatio;
            else
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, Vector3.up * targetHeight * m_cameraHeightRatio, crouchingSharpness * Time.deltaTime);

        }

        //FOVを設定する
        public void SetCameraFOV(in float fov)
        {
            m_playerCamera.fieldOfView = fov;            
        }


    }

}