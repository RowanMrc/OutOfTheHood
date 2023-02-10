using UnityEngine;
using UnityEngine.InputSystem;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;
        public KeyCode aimInput = KeyCode.Mouse1;
        public GameObject footstepsSound;
        public AudioSource jumpSound;
        public AudioSource shotSound;


        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        [Header("Weapon Related")]
        public GameObject aimCam;
        public GameObject aimHUDCrosshair;
        public bool aiming = false;
        [SerializeField] public Transform pfBulletProjectile;
        [SerializeField] public Transform spawnBulletProjectile;
        [SerializeField] public LayerMask aimColliderLayerMask = new LayerMask();
        [SerializeField] public Transform debugTransform;
        public GameObject sphereCrosshair;
        Vector3 mouseWorldPosition = Vector3.zero;

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
            VisualTarget();
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();
            jumpSound = GetComponent<AudioSource>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            if (!aiming)
            {
                MoveInput();
                MoveInputSound();
            }
            
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
            AimInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
          
        }

        public virtual void MoveInputSound()
        {
            if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                footstepsSound.SetActive(true);
            }else
            {
                footstepsSound.SetActive(false);
            }
        }


        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions())
            {
                cc.Jump();
                jumpSound.Play();
            }
                

            
        }

        protected virtual void AimInput()
        {
            

            if (Input.GetMouseButtonDown(1)){
                aiming = true;
                cc.Aim();
                cc.input.x = 0;
                cc.input.z = 0;
                //aimCam.SetActive(true);
                //aimHUDCrosshair.SetActive(true);
                sphereCrosshair.SetActive(true);
                Debug.Log("Aim");
            }

            if (Input.GetMouseButtonDown(0) && aiming)
            {
                Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
                Vector3 aimDir = (mouseWorldPosition - spawnBulletProjectile.position).normalized;
                Instantiate(pfBulletProjectile,spawnBulletProjectile.position, Quaternion.LookRotation(aimDir, Vector3.up));
                shotSound.Play();
                ResetAim();
                /*aiming = false;
                cc.AimReset();
                aimCam.SetActive(false);
                aimHUDCrosshair.SetActive(false);
                Debug.Log("AimReset");*/
            }


        }

        protected virtual void ResetAim()
        {
            aiming = false;
            cc.AimReset();
            //aimCam.SetActive(false);
            //aimHUDCrosshair.SetActive(false);
            sphereCrosshair.SetActive(false);
            Debug.Log("AimReset");
        }

        protected virtual void VisualTarget()
        {
          
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast( ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                debugTransform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
            }
        }

        #endregion       
    }
}