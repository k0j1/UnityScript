using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimation : MonoBehaviour
{
//    [SerializeField]
//    private Animator CatAnimator = null;
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
    
    int nCatState = 0;
    int nDogState = 0;
    int nCowState = 0;
    int nBearState = 0;
    int nElephantState = 0;
    int nGiraffeState = 0;
    int nDeerState = 0;
    int nGorillaState = 0;
    int nWolfState = 0;
    int nHorseState = 0;
    int nPenguinState = 0;
    int nRabbitState = 0;
    int nSnakeState = 0;
    int nSpiderState = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        //CatAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishAnimation()
    {

    }

    public void Change_All()
    {
        Change_Cat();
        Change_Dog();
        Change_Cow();
        Change_Bear();
        Change_Elephant();
        Change_Giraffe();
        Change_Deer();
        Change_Gorilla();
        Change_Wolf();
        Change_Horse();
        Change_Penguin();
        Change_Rabbit();
        Change_Snake();
        Change_Spider();
    }
    
    public void Change_Cat()
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //String strState = CatAnimator;
        //Debug.Log();
        //bool bState = CatAnimator.GetBool("isRunning");
        //Debug.Log(bState);
        
        Animator CatAnimator = null;
        if(BlackCat.activeSelf) CatAnimator = BlackCat.GetComponent<Animator>();
        else if(OrangeCat.activeSelf) CatAnimator = OrangeCat.GetComponent<Animator>();
        else if(WhiteCat.activeSelf) CatAnimator = WhiteCat.GetComponent<Animator>();
        else return;
                
        nCatState++;
        Play_Cat(CatAnimator);
        nCatState%=6;
    }
    public void Play_Cat(Animator CatAnimator)
    {
        switch(nCatState)
        {
            case 0:
                CatAnimator.SetBool("isWalking",    true);
                CatAnimator.SetBool("isSitting",    false);
                CatAnimator.SetBool("isRunning",    false);
                CatAnimator.SetBool("isDead",       false);
                CatAnimator.SetBool("isSleeping",   false);
                CatAnimator.SetBool("isAttacking",  false);
                CatAnimator.SetBool("isIdling",     false);
                //CatAnimator.SetTrigger("isWalking");
                break;
            case 1:
                CatAnimator.SetBool("isWalking",    false);
                CatAnimator.SetBool("isSitting",    true);
                CatAnimator.SetBool("isRunning",    false);
                CatAnimator.SetBool("isDead",       false);
                CatAnimator.SetBool("isSleeping",   false);
                CatAnimator.SetBool("isAttacking",  false);
                CatAnimator.SetBool("isIdling",     false);
                //CatAnimator.SetTrigger("isSitting");
                break;
            case 2:
                CatAnimator.SetBool("isWalking",    false);
                CatAnimator.SetBool("isSitting",    false);
                CatAnimator.SetBool("isRunning",    true);
                CatAnimator.SetBool("isDead",       false);
                CatAnimator.SetBool("isSleeping",   false);
                CatAnimator.SetBool("isAttacking",  false);
                CatAnimator.SetBool("isIdling",     false);
                //CatAnimator.SetTrigger("isRunning");
                break;
            case 3:
                CatAnimator.SetBool("isWalking",    false);
                CatAnimator.SetBool("isSitting",    false);
                CatAnimator.SetBool("isRunning",    false);
                CatAnimator.SetBool("isDead",       false);
                CatAnimator.SetBool("isSleeping",   false);
                CatAnimator.SetBool("isAttacking",  false);
                CatAnimator.SetBool("isIdling",     false);
                CatAnimator.SetTrigger("isDead");
                break;
            case 4:
                CatAnimator.SetBool("isWalking",    false);
                CatAnimator.SetBool("isSitting",    false);
                CatAnimator.SetBool("isRunning",    false);
                CatAnimator.SetBool("isDead",       false);
                CatAnimator.SetBool("isSleeping",   false);
                CatAnimator.SetBool("isAttacking",  false);
                CatAnimator.SetBool("isIdling",     true);
                CatAnimator.Play("Idle");
                break;
            case 5:
                CatAnimator.SetBool("isWalking",    false);
                CatAnimator.SetBool("isSitting",    false);
                CatAnimator.SetBool("isRunning",    false);
                CatAnimator.SetBool("isDead",       false);
                CatAnimator.SetBool("isSleeping",   false);
                CatAnimator.SetBool("isAttacking",  true);
                CatAnimator.SetBool("isIdling",     false);
                CatAnimator.SetTrigger("isAttacking");
                break;
            case 6:
                CatAnimator.SetBool("isWalking",    false);
                CatAnimator.SetBool("isSitting",    false);
                CatAnimator.SetBool("isRunning",    false);
                CatAnimator.SetBool("isDead",       false);
                CatAnimator.SetBool("isSleeping",   true);
                CatAnimator.SetBool("isAttacking",  false);
                CatAnimator.SetBool("isIdling",     false);
                //CatAnimator.SetTrigger("isSleeping");
                break;
        }        
    }
    
    public void Change_Dog()
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //String strState = CatAnimator;
        //Debug.Log();
        //bool bState = CatAnimator.GetBool("isRunning");
        //Debug.Log(bState);
        
        Animator DogAnimator = null;
        if(Dog_Chihuahua.activeSelf) DogAnimator = Dog_Chihuahua.GetComponent<Animator>();
        else if(Dog_Chihuahua_Black.activeSelf) DogAnimator = Dog_Chihuahua_Black.GetComponent<Animator>();
        else if(Dog_GoldenRet.activeSelf) DogAnimator = Dog_GoldenRet.GetComponent<Animator>();
        else if(Dog_GreatDane.activeSelf) DogAnimator = Dog_GreatDane.GetComponent<Animator>();
        else if(Dog_GreatDane_Black.activeSelf) DogAnimator = Dog_GreatDane_Black.GetComponent<Animator>();
        else return;
        
        nDogState++;
        Play_Dog(DogAnimator);        
        nDogState%=4;
    }
    public void Play_Dog(Animator DogAnimator)
    {
        switch(nDogState)
        {
            case 0:
                DogAnimator.SetBool("isWalking",    true);
                DogAnimator.SetBool("isSitting",    false);
                DogAnimator.SetBool("isRunning",    false);
                DogAnimator.SetBool("isBarking",    false);
                break;
            case 1:
                DogAnimator.SetBool("isWalking",    false);
                DogAnimator.SetBool("isSitting",    true);
                DogAnimator.SetBool("isRunning",    false);
                DogAnimator.SetBool("isBarking",    false);
                break;
            case 2:
                DogAnimator.SetBool("isWalking",    false);
                DogAnimator.SetBool("isSitting",    false);
                DogAnimator.SetBool("isRunning",    true);
                DogAnimator.SetBool("isBarking",    false);
                break;
            case 3:
                DogAnimator.SetBool("isWalking",    false);
                DogAnimator.SetBool("isSitting",    false);
                DogAnimator.SetBool("isRunning",    false);
                DogAnimator.SetBool("isBarking",    true);
                break;
        }        
    }
    
    public void Change_Cow()
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //String strState = CatAnimator;
        //Debug.Log();
        //bool bState = CatAnimator.GetBool("isRunning");
        //Debug.Log(bState);
        
        Animator CowAnimator = null;
        if(Cow.activeSelf) CowAnimator = Cow.GetComponent<Animator>();
        else if(Cow_Black.activeSelf) CowAnimator = Cow_Black.GetComponent<Animator>();
        else if(Cow_BlWh.activeSelf) CowAnimator = Cow_BlWh.GetComponent<Animator>();
        else return;
        
        nCowState++;
        Play_Cow(CowAnimator);        
        nCowState%=3;
    }
    public void Play_Cow(Animator CowAnimator)
    {
        switch(nCowState)
        {
            case 0:
                CowAnimator.SetBool("isWalking",    true);
                CowAnimator.SetBool("isEating",     false);
                CowAnimator.SetBool("isRunning",    false);
                CowAnimator.SetBool("isDead",       false);
                CowAnimator.Play("Idle");
                break;
            case 1:
                CowAnimator.SetBool("isWalking",    false);
                CowAnimator.SetBool("isEating",     true);
                CowAnimator.SetBool("isRunning",    false);
                CowAnimator.SetBool("isDead",       false);
                break;
            case 2:
                CowAnimator.SetBool("isWalking",    false);
                CowAnimator.SetBool("isEating",     false);
                CowAnimator.SetBool("isRunning",    true);
                CowAnimator.SetBool("isDead",       false);
                break;
            case 3:
                CowAnimator.SetBool("isWalking",    false);
                CowAnimator.SetBool("isEating",     false);
                CowAnimator.SetBool("isRunning",    false);
                CowAnimator.SetBool("isDead",       false);
                CowAnimator.SetTrigger("isDead");
                break;
        }        
    }
    
    public void Change_Bear()
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //String strState = CatAnimator;
        //Debug.Log();
        //bool bState = CatAnimator.GetBool("isRunning");
        //Debug.Log(bState);
        
        Animator BearAnimator = null;
        if(Bear.activeSelf) BearAnimator = Bear.GetComponent<Animator>();
        else return;
        
        nBearState++;
        Play_Bear(BearAnimator);        
        nBearState%=4;
    }
    public void Play_Bear(Animator BearAnimator)
    {
        switch(nBearState)
        {
            case 0:
                BearAnimator.SetBool("isWalking",    true);
                BearAnimator.SetBool("isStanding",   false);
                BearAnimator.SetBool("isRunning",    false);
                BearAnimator.SetBool("isAttacking",  false);
                break;
            case 1:
                BearAnimator.SetBool("isWalking",    false);
                BearAnimator.SetBool("isStanding",   true);
                BearAnimator.SetBool("isRunning",    false);
                BearAnimator.SetBool("isAttacking",  false);
                break;
            case 2:
                BearAnimator.SetBool("isWalking",    false);
                BearAnimator.SetBool("isStanding",   false);
                BearAnimator.SetBool("isRunning",    true);
                BearAnimator.SetBool("isAttacking",  false);
                break;
            case 3:
                BearAnimator.SetBool("isWalking",    false);
                BearAnimator.SetBool("isStanding",   false);
                BearAnimator.SetBool("isRunning",    false);
                BearAnimator.SetBool("isAttacking",  false);
                BearAnimator.SetTrigger("isAttacking");
                break;
        }        
    }
    
    public void Change_Elephant()
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //String strState = CatAnimator;
        //Debug.Log();
        //bool bState = CatAnimator.GetBool("isRunning");
        //Debug.Log(bState);
        
        Animator ElephantAnimator = null;
        if(Elephant.activeSelf) ElephantAnimator = Elephant.GetComponent<Animator>();
        else return;
        
        nElephantState++;
        Play_Elephant(ElephantAnimator);        
        nElephantState%=4;
    }
    public void Play_Elephant(Animator ElephantAnimator)
    {
        switch(nElephantState)
        {
            case 0:
                ElephantAnimator.SetBool("isWalking",    true);
                ElephantAnimator.SetBool("isDrinking",   false);
                ElephantAnimator.SetBool("isRunning",    false);
                ElephantAnimator.SetBool("isAttacking",  false);
                break;
            case 1:
                ElephantAnimator.SetBool("isWalking",    false);
                ElephantAnimator.SetBool("isDrinking",   true);
                ElephantAnimator.SetBool("isRunning",    false);
                ElephantAnimator.SetBool("isAttacking",  false);
                break;
            case 2:
                ElephantAnimator.SetBool("isWalking",    false);
                ElephantAnimator.SetBool("isDrinking",   false);
                ElephantAnimator.SetBool("isRunning",    true);
                ElephantAnimator.SetBool("isAttacking",  false);
                break;
            case 3:
                ElephantAnimator.SetBool("isWalking",    false);
                ElephantAnimator.SetBool("isDrinking",   false);
                ElephantAnimator.SetBool("isRunning",    false);
                ElephantAnimator.SetBool("isAttacking",  false);
                ElephantAnimator.SetTrigger("isAttacking");
                break;
        }        
    }
    
    public void Change_Giraffe()
    {
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //String strState = CatAnimator;
        //Debug.Log();
        //bool bState = CatAnimator.GetBool("isRunning");
        //Debug.Log(bState);
        
        Animator GiraffeAnimator = null;
        if(Giraffe.activeSelf) GiraffeAnimator = Giraffe.GetComponent<Animator>();
        else return;
        
        nGiraffeState++;
        Play_Giraffe(GiraffeAnimator);        
        nGiraffeState%=3;
    }
    public void Play_Giraffe(Animator GiraffeAnimator)
    {
        switch(nGiraffeState)
        {
            case 0:
                GiraffeAnimator.SetBool("isWalking",    true);
                GiraffeAnimator.SetBool("isEating",     false);
                GiraffeAnimator.SetBool("isRunning",    false);
                break;
            case 1:
                GiraffeAnimator.SetBool("isWalking",    false);
                GiraffeAnimator.SetBool("isEating",     true);
                GiraffeAnimator.SetBool("isRunning",    false);
                break;
            case 2:
                GiraffeAnimator.SetBool("isWalking",    false);
                GiraffeAnimator.SetBool("isEating",     false);
                GiraffeAnimator.SetBool("isRunning",    true);
                break;
        }        
    }
    
    public void Change_Deer()
    {
        Animator DeerAnimator = null;
        if(Deer.activeSelf) DeerAnimator = Deer.GetComponent<Animator>();
        else if(Deer_Gray.activeSelf) DeerAnimator = Deer_Gray.GetComponent<Animator>();
        else return;
        
        nDeerState++;
        Play_Deer(DeerAnimator);        
        nDeerState%=3;
    }
    public void Play_Deer(Animator DeerAnimator)
    {
        switch(nDeerState)
        {
            case 0:
                DeerAnimator.SetBool("isWalking",    true);
                DeerAnimator.SetBool("isEating",     false);
                DeerAnimator.SetBool("isRunning",    false);
                break;
            case 1:
                DeerAnimator.SetBool("isWalking",    false);
                DeerAnimator.SetBool("isEating",     true);
                DeerAnimator.SetBool("isRunning",    false);
                break;
            case 2:
                DeerAnimator.SetBool("isWalking",    false);
                DeerAnimator.SetBool("isEating",     false);
                DeerAnimator.SetBool("isRunning",    true);
                break;
        }        
    }
    
    public void Change_Gorilla()
    {
        Animator GorillaAnimator = null;
        if(Gorilla.activeSelf) GorillaAnimator = Gorilla.GetComponent<Animator>();
        else if(Gorilla_Black.activeSelf) GorillaAnimator = Gorilla_Black.GetComponent<Animator>();
        else return;
        
        nGorillaState++;
        Play_Gorilla(GorillaAnimator);        
        nGorillaState%=4;
    }
    public void Play_Gorilla(Animator GorillaAnimator)
    {
        switch(nGorillaState)
        {
            case 0:
                GorillaAnimator.SetBool("isWalking",    true);
                GorillaAnimator.SetBool("isChestHit",   false);
                GorillaAnimator.SetBool("isRunning",    false);
                GorillaAnimator.SetBool("isAttacking",  false);
                break;
            case 1:
                GorillaAnimator.SetBool("isWalking",    false);
                GorillaAnimator.SetBool("isChestHit",   true);
                GorillaAnimator.SetBool("isRunning",    false);
                GorillaAnimator.SetBool("isAttacking",  false);
                break;
            case 2:
                GorillaAnimator.SetBool("isWalking",    false);
                GorillaAnimator.SetBool("isChestHit",   false);
                GorillaAnimator.SetBool("isRunning",    true);
                GorillaAnimator.SetBool("isAttacking",  false);
                break;
            case 3:
                GorillaAnimator.SetBool("isWalking",    false);
                GorillaAnimator.SetBool("isChestHit",   false);
                GorillaAnimator.SetBool("isRunning",    false);
                GorillaAnimator.SetBool("isAttacking",  true);
                break;
        }        
    }
    
    public void Change_Wolf()
    {
        Animator WolfAnimator = null;
        if(Wolf_Black.activeSelf) WolfAnimator = Wolf_Black.GetComponent<Animator>();
        else if(Wolf_Grey.activeSelf) WolfAnimator = Wolf_Grey.GetComponent<Animator>();
        else return;
        
        nWolfState++;
        Play_Wolf(WolfAnimator);        
        nWolfState%=4;
    }
    public void Play_Wolf(Animator WolfAnimator)
    {
        switch(nWolfState)
        {
            case 0:
                WolfAnimator.SetBool("isWalking",    true);
                WolfAnimator.SetBool("isHowling",    false);
                WolfAnimator.SetBool("isRunning",    false);
                WolfAnimator.SetBool("isAttack-Idle",  false);
                break;
            case 1:
                WolfAnimator.SetBool("isWalking",    false);
                WolfAnimator.SetBool("isHowling",    true);
                WolfAnimator.SetBool("isRunning",    false);
                WolfAnimator.SetBool("isAttack-Idle",  false);
                break;
            case 2:
                WolfAnimator.SetBool("isWalking",    false);
                WolfAnimator.SetBool("isHowling",    false);
                WolfAnimator.SetBool("isRunning",    true);
                WolfAnimator.SetBool("isAttack-Idle",  false);
                break;
            case 3:
                WolfAnimator.SetBool("isWalking",    false);
                WolfAnimator.SetBool("isHowling",    false);
                WolfAnimator.SetBool("isRunning",    false);
                WolfAnimator.SetBool("isAttack-Idle",  true);
                break;
        }        
    }
    
    public void Change_Horse()
    {
        Animator HorseAnimator = null;
        if(Horse.activeSelf) HorseAnimator = Horse.GetComponent<Animator>();
        else if(Horse_Black.activeSelf) HorseAnimator = Horse_Black.GetComponent<Animator>();
        else if(Horse_Grey.activeSelf) HorseAnimator = Horse_Grey.GetComponent<Animator>();
        else if(Horse_White.activeSelf) HorseAnimator = Horse_White.GetComponent<Animator>();
        else return;
        
        nHorseState++;
        Play_Horse(HorseAnimator);        
        nHorseState%=3;
    }
    public void Play_Horse(Animator HorseAnimator)
    {
        switch(nHorseState)
        {
            case 0:
                HorseAnimator.SetBool("isWalking",    true);
                HorseAnimator.SetBool("isEating",     false);
                HorseAnimator.SetBool("isRunning",    false);
                break;
            case 1:
                HorseAnimator.SetBool("isWalking",    false);
                HorseAnimator.SetBool("isEating",     true);
                HorseAnimator.SetBool("isRunning",    false);
                break;
            case 2:
                HorseAnimator.SetBool("isWalking",    false);
                HorseAnimator.SetBool("isEating",     false);
                HorseAnimator.SetBool("isRunning",    true);
                break;
        }        
    }
    
    public void Change_Penguin()
    {
        Animator PenguinAnimator = null;
        if(Penguin.activeSelf) PenguinAnimator = Penguin.GetComponent<Animator>();
        else return;
        
        nPenguinState++;
        Play_Penguin(PenguinAnimator);        
        nPenguinState%=3;
    }
    public void Play_Penguin(Animator PenguinAnimator)
    {
        switch(nPenguinState)
        {
            case 0:
                PenguinAnimator.SetBool("isWalking",    true);
                PenguinAnimator.SetBool("isShaking",    false);
                PenguinAnimator.SetBool("isRunning",    false);
                break;
            case 1:
                PenguinAnimator.SetBool("isWalking",    false);
                PenguinAnimator.SetBool("isShaking",    true);
                PenguinAnimator.SetBool("isRunning",    false);
                break;
            case 2:
                PenguinAnimator.SetBool("isWalking",    false);
                PenguinAnimator.SetBool("isShaking",    false);
                PenguinAnimator.SetBool("isRunning",    true);
                break;
        }        
    }
    
    public void Change_Rabbit()
    {
        Animator RabbitAnimator = null;
        if(Rabbit.activeSelf) RabbitAnimator = Rabbit.GetComponent<Animator>();
        else if(Rabbit_White.activeSelf) RabbitAnimator = Rabbit_White.GetComponent<Animator>();
        else return;
        
        nRabbitState++;
        Play_Rabbit(RabbitAnimator);        
        nRabbitState%=4;
    }
    public void Play_Rabbit(Animator RabbitAnimator)
    {
        switch(nRabbitState)
        {
            case 0:
                RabbitAnimator.SetBool("isJumping",    true);
                RabbitAnimator.SetBool("isLookingOut", false);
                RabbitAnimator.SetBool("isRunning",    false);
                RabbitAnimator.SetBool("isJumping_Up", false);
                break;
            case 1:
                RabbitAnimator.SetBool("isJumping",    false);
                RabbitAnimator.SetBool("isLookingOut", true);
                RabbitAnimator.SetBool("isRunning",    false);
                RabbitAnimator.SetBool("isJumping_Up", false);
                break;
            case 2:
                RabbitAnimator.SetBool("isJumping",    false);
                RabbitAnimator.SetBool("isLookingOut", false);
                RabbitAnimator.SetBool("isRunning",    true);
                RabbitAnimator.SetBool("isJumping_Up", false);
                break;
            case 3:
                RabbitAnimator.SetBool("isJumping",    false);
                RabbitAnimator.SetBool("isLookingOut", false);
                RabbitAnimator.SetBool("isRunning",    false);
                RabbitAnimator.SetBool("isJumping_Up", true);
                break;
        }        
    }
    
    public void Change_Snake()
    {
        Animator SnakeAnimator = null;
        if(Snake_Yellow.activeSelf) SnakeAnimator = Snake_Yellow.GetComponent<Animator>();
        else if(Snake_Red.activeSelf) SnakeAnimator = Snake_Red.GetComponent<Animator>();
        else return;
        
        nSnakeState++;
        Play_Snake(SnakeAnimator);        
        nSnakeState%=3;
    }
    public void Play_Snake(Animator SnakeAnimator)
    {
        switch(nSnakeState)
        {
            case 0:
                SnakeAnimator.SetBool("isWalking",    true);
                SnakeAnimator.SetBool("isAttacking",  false);
                SnakeAnimator.SetBool("isIdle",       false);
                break;
            case 1:
                SnakeAnimator.SetBool("isWalking",    false);
                SnakeAnimator.SetBool("isAttacking",  true);
                SnakeAnimator.SetBool("isIdle",       false);
                break;
            case 2:
                SnakeAnimator.SetBool("isWalking",    false);
                SnakeAnimator.SetBool("isAttacking",  false);
                SnakeAnimator.SetBool("isIdle",       true);
                break;
        }        
    }
    
    public void Change_Spider()
    {
        Animator SpiderAnimator = null;
        if(Spider_Yellow.activeSelf) SpiderAnimator = Spider_Yellow.GetComponent<Animator>();
        else if(Spider_Green.activeSelf) SpiderAnimator = Spider_Green.GetComponent<Animator>();
        else if(Snake_Red.activeSelf) SpiderAnimator = Spider_Red.GetComponent<Animator>();
        else return;
        
        nSnakeState++;
        Play_Snake(SpiderAnimator);        
        nSnakeState%=3;
    }
    public void Play_Spider(Animator SpiderAnimator)
    {
        switch(nSnakeState)
        {
            case 0:
                SpiderAnimator.SetBool("isWalking",    true);
                SpiderAnimator.SetBool("isAttacking",  false);
                SpiderAnimator.SetBool("isScared",     false);
                break;
            case 1:
                SpiderAnimator.SetBool("isWalking",    false);
                SpiderAnimator.SetBool("isAttacking",  true);
                SpiderAnimator.SetBool("isScared",     false);
                break;
            case 2:
                SpiderAnimator.SetBool("isWalking",    false);
                SpiderAnimator.SetBool("isAttacking",  false);
                SpiderAnimator.SetBool("isScared",     true);
                break;
        }        
    }
}
