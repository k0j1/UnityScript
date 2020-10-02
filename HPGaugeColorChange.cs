using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGaugeColorChange : MonoBehaviour
{
    [SerializeField] Image image_HPgauge = null;
    [SerializeField] GameSystem GameSystem = null;
    [SerializeField] GameObject GameSetting = null;
    [SerializeField] GameObject GameScript = null;
    [SerializeField] GameObject Timer = null;
    [SerializeField] GameObject m_Charactor = null;
    [SerializeField] GameObject Button = null;

    public float maxHP = 100;
    [Range(0, 100)]  public float hp = 100;
    [Range(0, 1)] public float color_h,  color_s, color_l;
    //private Image image_HPgauge;
    private float hp_ratio;
    
    private float seconds;
    private float oldSeconds;
    private GameSetting scriptSetting;
    private ChangeCamera script;
    private Timer scriptTimer;
    private bool bFinish = false;
    private bool bEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        //image_HPgauge = gameObject.GetComponent<Image>();
        color_s = 1.0f;
        color_l = 0.5f;
        bFinish = false;
    }
 
    Color HSLtoRGB(float H, float S, float L)
    {
        Color col = new Color(1f, 1f, 1f, 1f);
 
        float max = L + S * (1 - Mathf.Abs(2f * L - 1f)) * 0.5f;
        float min = L - S * (1 - Mathf.Abs(2f * L - 1f)) * 0.5f;
 
        int i = (int)Mathf.Floor(6.0f * H);
 
        switch (i)
        {
            case 0:
                col.r = max;
                col.g = min + (max - min) * 6.0f * H;
                col.b = min;
                break;
            case 1:
                col.r = min + (max - min) * (2.0f - 6.0f * H);
                col.g = max;
                col.b = min;
                break;
            case 2:
                col.r = min;
                col.g = max;
                col.b = min + (max - min) * (6.0f * H - 2.0f);
                break;
            case 3:
                col.r = min;
                col.g = min + (max - min) * (4.0f - 6.0f * H);
                col.b = max;
                break;
            case 4:
                col.r = min + (max - min) * (6.0f * H - 4.0f);
                col.g = min;
                col.b = max;
                break;
            case 5:
                col.r = max;
                col.g = min;
                col.b = min + (max - min) * (6.0f - 6.0f * H);
                break;
            default:
                col.r = max;
                col.g = max;
                col.b = max;
                break;
        }
        return col;
    }
 
    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        //hp = maxHP - (100 / 60) * seconds * 2;
        //　値が変わった時だけHP減らす
        if ((int)seconds != (int)oldSeconds)
        {
            hp -= 2.5f;
            oldSeconds = seconds;
        }

        //if (0.0f < hp)
        {
            hp_ratio = hp / maxHP;

            image_HPgauge.fillAmount = hp_ratio;
            color_h = Mathf.Lerp(0f, 0.4f, hp_ratio);

            image_HPgauge.color = HSLtoRGB(color_h, color_s, color_l);
        }

        // HP0、ゲームオーバー
        if(0.0f >= hp && !bFinish)
        {
            // 繰り返しここが実行されないように先にセットしておく
            bFinish = true;

            script = GameScript.GetComponent<ChangeCamera>();
            script.ChangeEndingCamera();
            scriptTimer = Timer.GetComponent<Timer>();
            scriptTimer.FinishTimer();

            Button.SetActive(false);

            //int nSelCharactor = SettingManager.LoadSelectCharactor();
            //ChangeCharactor ChangeChar = m_Charactor.GetComponent<ChangeCharactor>();
            //GameObject Charactor = ChangeChar.GetCharactor(nSelCharactor);

            MoveCharactor scriptMoveChar = m_Charactor.GetComponent<MoveCharactor>();
            scriptMoveChar.FinishGame();

            StartCoroutine("DispResult");
        }

    }

    IEnumerator DispResult()
    {
        yield return new WaitForSeconds(7); // xx秒待つ
        if(!bEnd)
        {
            // 結果表示処理は一度だけ
            bEnd = true;
            GameSystem.DispResult();
            //scriptSetting = GameSetting.GetComponent<GameSetting>();
            //scriptSetting.GoSelect();
        }
    }

    public void RecoverHP(float nAddHP)
    {
        hp += nAddHP;
        if (maxHP < hp) hp = maxHP;
    }
}