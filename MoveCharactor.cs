using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCharactor : MonoBehaviour
{
    public Renderer groundRenderer;
    public Collider groundCollider;
    public GameObject mEffectJump;
    private List<GameObject> onScreenParticles = new List<GameObject>();

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
    
    private GameObject mCharactor;
    private ChangeCharactor mChangeChar;
    private CharacterController characterController;
	private Vector3 velocity;
	private Vector3 velocity2;
	[SerializeField]
    private Joystick joystick;
	[SerializeField]
    private Joystick RightJoystick;
    private Animator animator;
	[SerializeField]
    private Camera BackCamera;
    
    //private float rotateSpeed = 0.1f;
    
    private float rotateAdd = 0.0f;
    private bool m_bGameFinish = false;

    //private bool inputJumpButton = false;

    /// <summary>
    /// キャラクターごとの歩く速度
    /// </summary>
    float[] walkSpeedArray = new float[32]{
            2.25f, 2.25f, 2.25f, 2.25f, 2.25f,
            3.25f, 4.25f, 4.25f, 5.25f, 5.25f,
            5.25f, 5.25f, 5.25f, 5.25f, 5.25f,
            5.25f, 5.25f, 5.25f, 6.25f, 6.25f,
            8.25f, 8.25f, 8.25f, 8.25f, 2.25f,
            2.25f, 2.25f, 3.25f, 3.25f, 2.25f,
            2.25f, 2.25f
     };

    /// <summary>
    /// キャラクター位置を調整
    /// </summary>
    int[,] startPos = new int[5, 3]{
            {-36, 0, -42},
            {-40, 0, 50},
            {50, 0, 50},
            {50, 0, -42},
            {0, 0, 0}
        };

    // Use this for initialization
    void Start ()
    {
        // 選択中のキャラクター番号
        int nSelCharactor = SettingManager.LoadSelectCharactor();
        //nSelCharactor = 29;
        
        walkSpeed = walkSpeedArray[nSelCharactor];

        mChangeChar = GetComponent <ChangeCharactor> ();
        mChangeChar.Change(nSelCharactor);
        
        mCharactor = mChangeChar.GetCharactor(nSelCharactor);
		characterController = mCharactor.GetComponent <CharacterController> ();
		animator = mCharactor.GetComponent <Animator> ();
//		joystick = GameObject.Find("Joystick").GetComponent <Joystick> ();
//		RightJoystick = GameObject.Find("RightJoystick").GetComponent <Joystick> ();
        
        int value = Random.Range(0, 5);
        
        Vector3 pos = mCharactor.transform.position;
        pos.x = startPos[value,0];
        pos.y = startPos[value,1];
        pos.z = startPos[value,2];
        mCharactor.transform.position = pos;

        //StartCoroutine("CheckForDeletedParticles");
    }
	
	// Update is called once per frame
	void Update () 
    {        
        velocity = Vector3.zero;
        velocity2 = Vector3.zero;

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(BackCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        if (m_bGameFinish)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            if(mChangeChar)
            {
                animator.SetBool(mChangeChar.GetCharactorFinishAnime(), true);
            }
        }

        float nAdd = 0;
        // 左ジョイスティックでの移動
        if(joystick.enabled && (0 != joystick.Horizontal || 0 != joystick.Vertical))
        {
            velocity = new Vector3 (joystick.Horizontal, 0f, joystick.Vertical);
            nAdd = joystick.Vertical;
        }
        // 右ジョイスティックでの移動        
        if(RightJoystick.enabled && (0 != RightJoystick.Horizontal || 0 != RightJoystick.Vertical))
        {
            velocity2 = new Vector3 (RightJoystick.Horizontal, 0f, RightJoystick.Vertical);
            //nAdd = RightJoystick.Vertical;
        }
        // カーソルキーでの移動
        if(velocity == Vector3.zero)
        {
            velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
            nAdd = Input.GetAxis ("Vertical");
        }
        //if(0.5f < velocity.x) velocity.x = 0.5f;

        // ログ出力
        //if(velocity.magnitude > 0.1f)
        //    Debug.Log(velocity);
        
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * velocity.z + BackCamera.transform.right * velocity.x;
        //Vector3 moveForward2 = cameraForward * velocity2.z + Camera.main.transform.right * velocity2.x;
        //Vector3 moveForward = cameraForward * velocity.y + velocity.z * cameraForward * walkSpeed + velocity.x * //Camera.main.transform.right * walkSpeed;
        //moveForward = moveForward * moveSpeed;
                
        // ログ出力
        //if(moveForward.magnitude > 0.1f) Debug.Log(moveForward);
        
        // 進行方向の調整
        float walkSpeedAdd = walkSpeed;
        if(0 > nAdd) walkSpeedAdd = 0.8f;
        cameraForward = cameraForward * nAdd * walkSpeedAdd;
        cameraForward.x += moveForward.x * (walkSpeed / 2);
        cameraForward.z += moveForward.z * (walkSpeed / 2);
        //cameraForward.y += Physics.gravity.y * Time.deltaTime;

        // ログ出力
        //if(moveForward.magnitude > 0.1f) Debug.Log(cameraForward);

        //if(1 == MODE)
        //if(velocity != Vector3.zero)
        if (!m_bGameFinish)
        {
            // ジャンプ中かの判定
            //CheckJumping();
            //  アニメーションの切替
            if (0 <= jumpAdd)
            {
                animator.SetBool("isWalking", true);
            }

            // 着地中のみ動作
            if (characterController.isGrounded) 
            {
                if (velocity.magnitude > 0.2f || velocity2.magnitude > 0.2f){
                    //animator.SetFloat("Speed", velocity.magnitude);
                    float speed = nAdd * walkSpeedAdd;
                    if (speed >= 1.5f)
                    {
                        animator.SetBool("isRunning", true);
                        animator.SetBool("isWalking", true);
                        //animator.SetBool("isWalking", false);                        
                        //animator.SetTrigger("isRunning");
                    }
                    else
                    {
                        animator.SetBool("isWalking", true);
                        animator.SetBool("isRunning", false);
                        //animator.SetTrigger("isWalking");
                    }
                }
                else
                {
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRunning", false);                        
                }

                //　ジャンプキー（デフォルトではSpace）を押したらY軸方向の速度にジャンプ力を足す
                Jump();
            }                                
            //Debug.Log(Physics.gravity.y * Time.deltaTime);
            
            // 重力追加
            jumpAdd += Physics.gravity.y * Time.deltaTime;
            
            // ジャンプ＋重力で更新
            cameraForward.y = jumpAdd;
            cameraForward.y = Mathf.Max(cameraForward.y, -maxFallSpeed);
            
            //Debug.Log(cameraForward);

            // キャラクターの向きを進行方向に回転
            //if (moveForward != Vector3.zero) {
            //    transform.rotation = Quaternion.LookRotation(moveForward * rotateSpeed);
            //    //Vector3 newDir = Vector3.RotateTowards(transform.forward,AnimDir,5f*Time.deltaTime,0f);
            //    //transform.rotation = Quaternion.LookRotation(newDir);
            //}            

            characterController.Move(cameraForward * Time.deltaTime);
        }
        
        //else if(2 == MODE)
        bool bDoRotate = false;
        float rotateAbsValue = Mathf.Abs(velocity2.x);
        if(0.2f < Mathf.Abs(velocity2.x))
        {
            // 着地中のみ動作
            if(characterController.isGrounded) 
            {
                // ジョイスティックでの左右の傾きがおおきくなったらキャラクターの向きを回転させる
                if( (0.2f < velocity2.x && rotateAdd < 2.0f) || (velocity2.x < -0.2f && rotateAdd > -2.0f) )
                {
                    rotateAdd += velocity2.x * 2;
                }
                else
                {
                    // 回転率の減衰
                    if(0.0f < rotateAdd) rotateAdd += -1.0f;
                    else if(0.0f > rotateAdd) rotateAdd += 1.0f;
                    // 最小以下なら値をクリア
                    if(Mathf.Abs(rotateAdd) < 1.0f)
                    {
                        rotateAdd = 0.0f;
                        //inputJumpButton = false;
                    }
                }
               
                // 回転する必要がある場合だけ実行
                float rotateAbsAdd = Mathf.Abs(rotateAdd);
                if(0.01f < rotateAbsAdd)
                {
                    mCharactor.transform.Rotate(0.0f, rotateAdd, 0.0f);
                    bDoRotate = true;
                }
            }
        }
        
        if(!bDoRotate)
        {
            rotateAdd = 0.0f;
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
        JumpValue(5f);
    }

    public void JumpValue(float nValue)
    {
        jumpPower = nValue;

        GameObject particle = spawnParticle(mEffectJump);
        particle.transform.position = mCharactor.transform.position;// + particle.transform.position;
    }

    //-------------------------------------------------------------
    // SYSTEM

    private GameObject spawnParticle(GameObject instObj)
    {
        GameObject particles = (GameObject)Instantiate(instObj);
        particles.transform.position = new Vector3(0, particles.transform.position.y, 0);
#if UNITY_3_5
			particles.SetActiveRecursively(true);
#else
        particles.SetActive(true);
        //			for(int i = 0; i < particles.transform.childCount; i++)
        //				particles.transform.GetChild(i).gameObject.SetActive(true);
#endif

        ParticleSystem ps = particles.GetComponent<ParticleSystem>();

#if UNITY_5_5_OR_NEWER
        if (ps != null)
        {
            var main = ps.main;
            if (main.loop)
            {
                ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
                ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
            }
        }
#else
		if(ps != null && ps.loop)
		{
			ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
			ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
#endif

        onScreenParticles.Add(particles);

        return particles;
    }
    IEnumerator CheckForDeletedParticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            for (int i = onScreenParticles.Count - 1; i >= 0; i--)
            {
                if (onScreenParticles[i] == null)
                {
                    onScreenParticles.RemoveAt(i);
                }
            }
        }
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

    public void FinishGame()
    {
        m_bGameFinish = true;
    }
}