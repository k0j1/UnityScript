using UnityEngine;
using System.Collections;
 
public class MoveCharactor2 : MonoBehaviour
{  
    // モード
    [SerializeField]
    protected int MODE = 1; // 1:左スティック、2:右スティック
    // おちる最大値
    [SerializeField]
    protected float maxFallSpeed = 20f;
    //  歩くスピード
	[SerializeField]
	private float walkSpeed = 0f;
    //　ジャンプ力
	[SerializeField]
	static public float jumpPower = 0f;
    private float jumpAdd = 0f;
    
	[SerializeField]
	private CharacterController characterController;
	private Vector3 velocity;
	[SerializeField]
    private Joystick joystick;
	[SerializeField]
    private Animator animator;
    
    //private float rotateSpeed = 0.1f;
    
    private float rotateAdd = 0.0f;
    
    //private bool inputJumpButton = false;
    
	// Use this for initialization
	void Start () {
		//characterController = GetComponent <CharacterController> ();
		//animator = GetComponent <Animator> ();
		//joystick = GameObject.Find("Joystick").GetComponent <Joystick> ();
	}
	
	// Update is called once per frame
	void Update () 
    {        
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                
        float nAdd = 0;
        if(0 != joystick.Horizontal || 0 != joystick.Vertical)
        {
            // ジョイスティックでの移動
            velocity = new Vector3 (joystick.Horizontal, 0f, joystick.Vertical);
            nAdd = joystick.Vertical;
        }
        else
        {
            // カーソルキーでの移動
            velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
            nAdd = Input.GetAxis ("Vertical");
        }
        //if(0.5f < velocity.x) velocity.x = 0.5f;

        // ログ出力
        //if(velocity.magnitude > 0.1f)
        //    Debug.Log(velocity);
        
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * velocity.z + Camera.main.transform.right * velocity.x;
        //Vector3 moveForward = cameraForward * velocity.y + velocity.z * cameraForward * walkSpeed + velocity.x * //Camera.main.transform.right * walkSpeed;
        //moveForward = moveForward * moveSpeed;
        // ログ出力
        //if(moveForward.magnitude > 0.1f) Debug.Log(moveForward);
        
        // 進行方向の調整
        cameraForward = cameraForward * nAdd * walkSpeed;
        cameraForward.x += moveForward.x;
        //cameraForward.y += Physics.gravity.y * Time.deltaTime;
        
        if(1 == MODE)
        {
            // ジャンプ中かの判定
            //CheckJumping();
            
            if(characterController.isGrounded) 
            {
                //  アニメーションの切替
                if(velocity.magnitude > 0.1f){
                    animator.SetFloat("Speed", velocity.magnitude);
                }
                //else if(nAdd > 0.1f)
                //{
                //    animator.SetFloat("Speed", nAdd);
                //}
                else
                {
                    animator.SetFloat("Speed", 0f);
                }

                //　ジャンプキー（デフォルトではSpace）を押したらY軸方向の速度にジャンプ力を足す
                Jump();

                //if(Input.GetButtonDown("Jump"))
                //{
                //	velocity.y += jumpPower;
                //}
            }            
                    
            //Debug.Log(Physics.gravity.y * Time.deltaTime);
            // 重力追加
            jumpAdd += Physics.gravity.y * Time.deltaTime;
            // ジャンプ＋重力で更新
            cameraForward.y = jumpAdd;
            cameraForward.y = Mathf.Max(cameraForward.y, -maxFallSpeed);
            //velocity.y += Physics.gravity.y * Time.deltaTime;
            //characterController.Move(moveForward * walkSpeed * Time.deltaTime);     
            //Debug.Log(cameraForward);

            // キャラクターの向きを進行方向に
            //if (moveForward != Vector3.zero) {
            //    transform.rotation = Quaternion.LookRotation(moveForward * rotateSpeed);
            //    //Vector3 newDir = Vector3.RotateTowards(transform.forward,AnimDir,5f*Time.deltaTime,0f);
            //    //transform.rotation = Quaternion.LookRotation(newDir);
            //}            

            characterController.Move(cameraForward * Time.deltaTime);
        }
        else if(2 == MODE)
        {
            if(characterController.isGrounded) 
            {
                // ジョイスティックでの左右の傾きがおおきくなったらキャラクターの向きを回転させる
                if( (0.2f < velocity.x && rotateAdd < 2.0f) || (velocity.x < -0.2f && rotateAdd > -2.0f) )
                {
                    rotateAdd += velocity.x / 10;
                }
                else
                {
                    // 回転率の減衰
                    if(0.0f < rotateAdd) rotateAdd += -0.4f;
                    else if(0.0f > rotateAdd) rotateAdd += 0.4f;
                    // 最小以下なら値をクリア
                    if(Mathf.Abs(rotateAdd) < 0.4f) rotateAdd = 0.0f;
                }
                transform.Rotate(0.0f, rotateAdd, 0.0f);
            }
        }
        
	}
    
    public void Jump()
    {
        //Spaceキーorジャンプボタンが押されている場合
        //if (inputJumpButton)
        if(0 < jumpPower)
        {
            jumpAdd = jumpPower;
            jumpPower = 0;
            //jumpPower--;
            //if(0 == jumpPower) inputJumpButton = false;                
        }
    }
    
    void SkillJumping()
    {
        jumpPower = 5f;
    }
    
    public void JumpValue(float nValue)
    {
        jumpPower = 5f;
    }

    public void DoSkill()
    {
        // 選択中のキャラクター番号
        int nSelCharactor = SettingManager.LoadSelectCharactor();

        switch (nSelCharactor)
        {
            case (int)ChangeCharactor.ANIMAL_KIND.CAT_BLACK: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.CAT_ORANGE: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.CAT_WHITE: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.DOG_CHIFUAHUA: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.DOG_CHIFUAHUA_BLACK: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.DOG_GOLDENRET: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.DOG_GREATDANE: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.DOG_GREATDANE_BLACK: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.COW: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.COW_BLACK: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.COW_BL_WH: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.BEAR: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.ELEPHANT: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.GIRAFFE: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.DEER: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.DEER_GRAY: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.GORILLA: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.GORILLA_BLACK: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.WOLF_BLACK: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.WOLF_GREY: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.HORSE: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.HORSE_BLACK: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.HORSE_GREY: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.HORSE_WHITE: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.PENGUIN: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.RABBIT: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.RABBIT_WHITE: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.SNAKE_YELLOW: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.SNAKE_RED: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.SPIDER_YELLOW: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.SPIDER_GREEN: SkillJumping(); break;
            case (int)ChangeCharactor.ANIMAL_KIND.SPIDER_RED: SkillJumping(); break;
        }
    }
}