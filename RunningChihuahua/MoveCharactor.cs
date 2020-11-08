using UnityEngine;
using System.Collections;

public class MoveCharactor : MonoBehaviour
{
    public GameObject mCharactor;
    public GameObject CameraObj;
    public GameObject RotateCenterObj;

    //[SerializeField]
    //protected int MODE = 1; // 1:左スティック、2:右スティック
    // おちる最大値
    [SerializeField]
    protected float maxFallSpeed = 20f;
    //  歩くスピード
    [SerializeField]
    public float walkSpeed = 50f;
    //　ジャンプ力
    //[SerializeField]
    //static public float jumpPower = 0f;
    //private float jumpAdd = 0f;

    private CharacterController characterController;
    //private Vector3 velocity;
    private Vector3 moveDirection = Vector3.zero;  //移動方向
    [SerializeField]
    //private Joystick joystick;
    //[SerializeField]
    //private Animator animator;

    //private float rotateSpeed = 0.1f;
    private float gravityValue = -9.81f;

    //private float rotateAdd = 0.0f;

    //private bool inputJumpButton = false;

    // Use this for initialization
    void Start()
    {
        characterController = mCharactor.GetComponent<CharacterController>();
        //mCharactor = GetComponent<GameObject>();
        //characterController = GetComponent <CharacterController> ();
        //animator = GetComponent <Animator> ();
        //joystick = GameObject.Find("Joystick").GetComponent <Joystick> ();
    }

    // Update is called once per frame
    void Update()
    {
        //CharacterController controller = GetComponent<CharacterController>();

        //CharacterControllerのisGroundedで接地判定
        moveDirection = Vector3.zero;  //移動方向
        AutoControl(1);
        Debug.Log(moveDirection);

        //移動方向に向けてキャラを回転させる
        //mCharactor.transform.rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
        if (!characterController.isGrounded)
        {
            //y軸方向への移動に重力を加える
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
            //CharacterControllerを移動させる
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    //標準的なコントロール
    void NormalControl()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);    //?必要ない気もする
            moveDirection *= walkSpeed;
        }
    }
    void AutoControl(int nMode)
    {
        if (characterController.isGrounded)
        {
            switch(nMode)
            {
                case 0:
                    moveDirection = Quaternion.Euler(0, CameraObj.transform.localEulerAngles.y, 0) * new Vector3(1f, 0, 1f);
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= walkSpeed;
                    break;
                case 1:
                    //RotateAround(円運動の中心,進行方向,速度)
                    transform.RotateAround(RotateCenterObj.transform.position, Vector3.up, walkSpeed * Time.deltaTime);
                    //float nX = Mathf.Abs(mCharactor.transform.position.x - RotateCenterObj.transform.position.x) * Math;
                    //moveDirection = new Vector3(1f, 0, 1f);
                    break;
            }
        }
    }


    //public void Jump()
    //{
    //    //Spaceキーorジャンプボタンが押されている場合
    //    //if (inputJumpButton)
    //    if (0 < jumpPower)
    //    {
    //        jumpAdd = jumpPower;
    //        jumpPower = 0;
    //        //jumpPower--;
    //        //if(0 == jumpPower) inputJumpButton = false;                
    //    }
    //}

    //void SkillJumping()
    //{
    //    jumpPower = 5f;
    //}

    //public void JumpValue(float nValue)
    //{
    //    jumpPower = 5f;
    //}

}