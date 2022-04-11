using UnityEngine;

/*
Sounds that the player makes

Sounds:
-Footsteps
-Jump
-Land
-Basic Attack
-Fire/Water/Electrical Attacks
---Small
---Charge
-Take Damage
--Generic
--Fire
--Electrical
RTPCs:
--Health
States:
-Surface type
*/
public class A_Player : MonoBehaviour
{

    //Things to be accessed by anyone

    PlayerController pController;//get player public class
  


    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pFootStep;
    [SerializeField] private AK.Wwise.Event pStopFootStep;

    [Header("Attacks")]
    [SerializeField] private AK.Wwise.Event pLteAttack;
    [SerializeField] private AK.Wwise.Event pChrgAttackStartLoop;
    [SerializeField] private AK.Wwise.Event pChrgAttackRelease;
    /*    [SerializeField] private AK.Wwise.Event pFireAtkkLte;
        [SerializeField] private AK.Wwise.Event pFireAtkChrg;
        [SerializeField] private AK.Wwise.Event pWaterAtkLte;
        [SerializeField] private AK.Wwise.Event pWaterAtkChrg;
        [SerializeField] private AK.Wwise.Event pElecAtkLte;
        [SerializeField] private AK.Wwise.Event pElecAtkChrg;*/
    [Header("Receive Damage")]
    [SerializeField] private AK.Wwise.Event pTakeDmgGeneric;
    [SerializeField] private AK.Wwise.Event pTakeDmgFire;
    // [SerializeField] private AK.Wwise.Event pTakeDmgElec;
    [SerializeField] private AK.Wwise.Event pDefeated;
    [SerializeField] private AK.Wwise.Event pTorchWave;



    #endregion

    public void Start()
    {
 
        pController = GetComponentInParent<PlayerController>();
        GameEventsController.playerChargedAttackStartEvent += PlayChargeAttackStart;
        GameEventsController.playerChargedAttackEndEvent += PlayChargeAttackCast;
        GameEventsController.playerAttackEvent += PlayBasicAttack;
        GameEventsController.playerDamageEvent += PlayTakeDamage;
        GameEventsController.gameOverEvent += PlayPlayerDeath;
        GameEventsController.playerMeleeAttackEvent += PlayTorchWave;

        pFootStep.Post(gameObject); //start the float sound and use the RTPC to choose the volume of it
    }

    public void Update()
    {
        //get and set player health RTPC
        float lastHp = pController.hp;
        if (pController.hp != lastHp)
            AkSoundEngine.SetRTPCValue("PlayerHealth", lastHp);

        AkSoundEngine.SetRTPCValue("PlayerSpeed", GameEventsController.getPlayerSpeed());
    }


/*    void PlayPotionPu()
    {
        if(pController.Interact)   
    }
*/

    void PlayBasicAttack()
    {
        pLteAttack.Post(gameObject);
    }


    void PlayChargeAttackStart()
    {
        pChrgAttackStartLoop.Post(gameObject);
    }

    void PlayChargeAttackCast()
    {
        pChrgAttackRelease.Post(gameObject);
    }

    public void PlayPlayerDeath()
    {
        pDefeated.Post(gameObject);
    }

    public void PlayTakeDamage(Damage dmg)
    {
        if (dmg.damageType == DamageType.Constant)//take fire damage
            pTakeDmgFire.Post(gameObject);
        else
            pTakeDmgGeneric.Post(gameObject);
    }

    public void PlayTorchWave()
    {
        pTorchWave.Post(gameObject);
    }
  







}//END MAIN