using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharactor : MonoBehaviour
{
    [SerializeField]
    public enum ANIMAL_KIND
    {
        CAT_BLACK   = 0,
        CAT_ORANGE  = 1,
        CAT_WHITE   = 2,
        DOG_CHIFUAHUA           = 3,
        DOG_CHIFUAHUA_BLACK     = 4,
        DOG_GOLDENRET           = 5,
        DOG_GREATDANE           = 6,
        DOG_GREATDANE_BLACK     = 7,
        COW         = 8,
        COW_BLACK   = 9,
        COW_BL_WH   = 10,
        BEAR        = 11,
        ELEPHANT    = 12,
        GIRAFFE     = 13,
        DEER        = 14,
        DEER_GRAY   = 15,
        GORILLA         = 16,
        GORILLA_BLACK   = 17,
        WOLF_BLACK  = 18,
        WOLF_GREY   = 19,
        HORSE       = 20,
        HORSE_BLACK = 21,
        HORSE_GREY  = 22,
        HORSE_WHITE = 23,
        PENGUIN     = 24,
        RABBIT          = 25,
        RABBIT_WHITE    = 26,
        SNAKE_YELLOW    = 27,
        SNAKE_RED       = 28,
        SPIDER_YELLOW   = 29,
        SPIDER_GREEN    = 30,
        SPIDER_RED      = 31
    }
    
    [SerializeField] private GameObject BlackCat = null;
    [SerializeField] private GameObject OrangeCat = null;
    [SerializeField] private GameObject WhiteCat = null;
    [SerializeField] private GameObject Dog_Chihuahua = null;
    [SerializeField] private GameObject Dog_Chihuahua_Black = null;
    [SerializeField] private GameObject Dog_GoldenRet = null;
    [SerializeField] private GameObject Dog_GreatDane = null;
    [SerializeField] private GameObject Dog_GreatDane_Black = null;
    [SerializeField] private GameObject Cow = null;
    [SerializeField] private GameObject Cow_Black = null;
    [SerializeField] private GameObject Cow_BlWh = null;
    [SerializeField] private GameObject Bear = null;
    [SerializeField] private GameObject Elephant = null;
    [SerializeField] private GameObject Giraffe = null;
    [SerializeField] private GameObject Deer = null;
    [SerializeField] private GameObject Deer_Gray = null;
    [SerializeField] private GameObject Gorilla = null;
    [SerializeField] private GameObject Gorilla_Black = null;
    [SerializeField] private GameObject Wolf_Black = null;
    [SerializeField] private GameObject Wolf_Grey = null;
    [SerializeField] private GameObject Horse = null;
    [SerializeField] private GameObject Horse_Black = null;
    [SerializeField] private GameObject Horse_Grey = null;
    [SerializeField] private GameObject Horse_White = null;
    [SerializeField] private GameObject Penguin = null;
    [SerializeField] private GameObject Rabbit = null;
    [SerializeField] private GameObject Rabbit_White = null;
    [SerializeField] private GameObject Snake_Yellow = null;
    [SerializeField] private GameObject Snake_Red = null;
    [SerializeField] private GameObject Spider_Yellow = null;
    [SerializeField] private GameObject Spider_Green = null;
    [SerializeField] private GameObject Spider_Red = null;

    //[SerializeField] private ANIMAL_KIND nCharactorValue = 0;

    public GameSystem mGameSystem;
    public Text SkillText = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public GameObject GetCharactor(int nValue)
    {
        switch(nValue)
        {
            case (int)ANIMAL_KIND.CAT_BLACK:  return BlackCat;
            case (int)ANIMAL_KIND.CAT_ORANGE: return OrangeCat;
            case (int)ANIMAL_KIND.CAT_WHITE:  return WhiteCat;
            case (int)ANIMAL_KIND.DOG_CHIFUAHUA: return Dog_Chihuahua;
            case (int)ANIMAL_KIND.DOG_CHIFUAHUA_BLACK: return Dog_Chihuahua_Black;
            case (int)ANIMAL_KIND.DOG_GOLDENRET: return Dog_GoldenRet;
            case (int)ANIMAL_KIND.DOG_GREATDANE: return Dog_GreatDane;
            case (int)ANIMAL_KIND.DOG_GREATDANE_BLACK: return Dog_GreatDane_Black;
            case (int)ANIMAL_KIND.COW: return Cow;
            case (int)ANIMAL_KIND.COW_BLACK: return Cow_Black;
            case (int)ANIMAL_KIND.COW_BL_WH: return Cow_BlWh;
            case (int)ANIMAL_KIND.BEAR: return Bear;
            case (int)ANIMAL_KIND.ELEPHANT: return Elephant;
            case (int)ANIMAL_KIND.GIRAFFE: return Giraffe;
            case (int)ANIMAL_KIND.DEER: return Deer;
            case (int)ANIMAL_KIND.DEER_GRAY: return Deer_Gray;
            case (int)ANIMAL_KIND.GORILLA: return Gorilla;
            case (int)ANIMAL_KIND.GORILLA_BLACK: return Gorilla_Black;
            case (int)ANIMAL_KIND.WOLF_BLACK: return Wolf_Black;
            case (int)ANIMAL_KIND.WOLF_GREY: return Wolf_Grey;
            case (int)ANIMAL_KIND.HORSE: return Horse;
            case (int)ANIMAL_KIND.HORSE_BLACK: return Horse_Black;
            case (int)ANIMAL_KIND.HORSE_GREY: return Horse_Grey;
            case (int)ANIMAL_KIND.HORSE_WHITE: return Horse_White;
            case (int)ANIMAL_KIND.PENGUIN: return Penguin;
            case (int)ANIMAL_KIND.RABBIT: return Rabbit;
            case (int)ANIMAL_KIND.RABBIT_WHITE: return Rabbit_White;
            case (int)ANIMAL_KIND.SNAKE_YELLOW: return Snake_Yellow;
            case (int)ANIMAL_KIND.SNAKE_RED: return Snake_Red;
            case (int)ANIMAL_KIND.SPIDER_YELLOW: return Spider_Yellow;
            case (int)ANIMAL_KIND.SPIDER_GREEN: return Spider_Green;
            case (int)ANIMAL_KIND.SPIDER_RED: return Spider_Red;
        }
        return null;
    }
    
    //[EnumAction(typeof(ChangeCharactor.ANIMAL_KIND))]
    public void Change(int nValue)
    {
        // 選択前のキャラを非表示
        int nSelCharactor = SettingManager.LoadSelectCharactor();
        GameObject HideObj = GetCharactor(nSelCharactor);
        if(HideObj) HideObj.SetActive(false);
        
        // 選択したキャラを表示
        GameObject DispObj = GetCharactor(nValue);
        if(DispObj) DispObj.SetActive(true);

        // 選択したキャラクターを保存
        SettingManager.SaveSelectCharactor(nValue);

        if(SkillText)
        {
            SkillText.text = GetCharactorSkill();
        }
        if(mGameSystem)
        {
            mGameSystem.ChangeCharactorDisp();
        }
    }

    public string GetCharactorSkill()
    {
        int nValue = SettingManager.LoadSelectCharactor();

        switch (nValue)
        {
            case (int)ANIMAL_KIND.CAT_BLACK:            return "ジャンプ…高いところに登れる";
            case (int)ANIMAL_KIND.CAT_ORANGE:           return "ジャンプ…高いところに登れる";
            case (int)ANIMAL_KIND.CAT_WHITE:            return "ジャンプ…高いところに登れる";
            case (int)ANIMAL_KIND.DOG_CHIFUAHUA:        return "嗅ぐ…一定時間食べ物が光る";
            case (int)ANIMAL_KIND.DOG_CHIFUAHUA_BLACK:  return "嗅ぐ…一定時間食べ物が光る";
            case (int)ANIMAL_KIND.DOG_GOLDENRET:        return "嗅ぐ…一定時間食べ物が光る";
            case (int)ANIMAL_KIND.DOG_GREATDANE:        return "嗅ぐ…一定時間食べ物が光る";
            case (int)ANIMAL_KIND.DOG_GREATDANE_BLACK:  return "嗅ぐ…一定時間食べ物が光る";
            case (int)ANIMAL_KIND.COW:                  return "興奮…短距離直線ダッシュする";
            case (int)ANIMAL_KIND.COW_BLACK:            return "興奮…短距離直線ダッシュする";
            case (int)ANIMAL_KIND.COW_BL_WH:            return "興奮…短距離直線ダッシュする";
            case (int)ANIMAL_KIND.BEAR:                 return "";
            case (int)ANIMAL_KIND.ELEPHANT:             return "";
            case (int)ANIMAL_KIND.GIRAFFE:              return "";
            case (int)ANIMAL_KIND.DEER:                 return "";
            case (int)ANIMAL_KIND.DEER_GRAY:            return "";
            case (int)ANIMAL_KIND.GORILLA:              return "";
            case (int)ANIMAL_KIND.GORILLA_BLACK:        return "";
            case (int)ANIMAL_KIND.WOLF_BLACK:           return "";
            case (int)ANIMAL_KIND.WOLF_GREY:            return "";
            case (int)ANIMAL_KIND.HORSE:                return "";
            case (int)ANIMAL_KIND.HORSE_BLACK:          return "";
            case (int)ANIMAL_KIND.HORSE_GREY:           return "";
            case (int)ANIMAL_KIND.HORSE_WHITE:          return "";
            case (int)ANIMAL_KIND.PENGUIN:              return "";
            case (int)ANIMAL_KIND.RABBIT:               return "";
            case (int)ANIMAL_KIND.RABBIT_WHITE:         return "";
            case (int)ANIMAL_KIND.SNAKE_YELLOW:         return "";
            case (int)ANIMAL_KIND.SNAKE_RED:            return "";
            case (int)ANIMAL_KIND.SPIDER_YELLOW:        return "";
            case (int)ANIMAL_KIND.SPIDER_GREEN:         return "";
            case (int)ANIMAL_KIND.SPIDER_RED:           return "";
        }
        return "";
    }

    public string GetCharactorFinishAnime()
    {
        int nValue = SettingManager.LoadSelectCharactor();

        switch (nValue)
        {
            case (int)ANIMAL_KIND.CAT_BLACK: return "isSleeping";
            case (int)ANIMAL_KIND.CAT_ORANGE: return "isSleeping";
            case (int)ANIMAL_KIND.CAT_WHITE: return "isSleeping";
            case (int)ANIMAL_KIND.DOG_CHIFUAHUA: return "isBarking";
            case (int)ANIMAL_KIND.DOG_CHIFUAHUA_BLACK: return "isBarking";
            case (int)ANIMAL_KIND.DOG_GOLDENRET: return "isBarking";
            case (int)ANIMAL_KIND.DOG_GREATDANE: return "isBarking";
            case (int)ANIMAL_KIND.DOG_GREATDANE_BLACK: return "isBarking";
            case (int)ANIMAL_KIND.COW: return "isDead";
            case (int)ANIMAL_KIND.COW_BLACK: return "isDead";
            case (int)ANIMAL_KIND.COW_BL_WH: return "isDead";
            case (int)ANIMAL_KIND.BEAR: return "isDead";
            case (int)ANIMAL_KIND.ELEPHANT: return "isDead";
            case (int)ANIMAL_KIND.GIRAFFE: return "isDead";
            case (int)ANIMAL_KIND.DEER: return "isDead";
            case (int)ANIMAL_KIND.DEER_GRAY: return "isDead";
            case (int)ANIMAL_KIND.GORILLA: return "isDead";
            case (int)ANIMAL_KIND.GORILLA_BLACK: return "isDead";
            case (int)ANIMAL_KIND.WOLF_BLACK: return "isHowling";
            case (int)ANIMAL_KIND.WOLF_GREY: return "isHowling";
            case (int)ANIMAL_KIND.HORSE: return "isDead";
            case (int)ANIMAL_KIND.HORSE_BLACK: return "isDead";
            case (int)ANIMAL_KIND.HORSE_GREY: return "isDead";
            case (int)ANIMAL_KIND.HORSE_WHITE: return "isDead";
            case (int)ANIMAL_KIND.PENGUIN: return "isDead";
            case (int)ANIMAL_KIND.RABBIT: return "isDead_0";
            case (int)ANIMAL_KIND.RABBIT_WHITE: return "isDead_1";
            case (int)ANIMAL_KIND.SNAKE_YELLOW: return "";
            case (int)ANIMAL_KIND.SNAKE_RED: return "";
            case (int)ANIMAL_KIND.SPIDER_YELLOW: return "";
            case (int)ANIMAL_KIND.SPIDER_GREEN: return "";
            case (int)ANIMAL_KIND.SPIDER_RED: return "";
        }
        return "";
    }
}
