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
    #region Public Variables
    public PlayerController pController;//get player public class
    public Rigidbody rb;

    #endregion

    //Things to be acecessed by only this script
    #region Private Variables
    [Header("Locomotion")]
    [SerializeField] private AK.Wwise.Event pFootStep;
    [SerializeField] private AK.Wwise.Event pStopFootStep;
    //[SerializeField] private AK.Wwise.Event pJump;
    //[SerializeField] private AK.Wwise.Event pLand;
    [Header("Attacks")]
    [SerializeField] private AK.Wwise.Event pLteAttack;
    [SerializeField] private AK.Wwise.Event pChrgAttack;
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

    private float speed = 0f;

    #endregion

    public void Update()
    {
        //get and set player health RTPC
        AkSoundEngine.SetRTPCValue("PlayerHealth", pController.hp);
        var vel = rb.velocity;      //to get a Vector3 representation of the velocity
        speed = vel.magnitude;
        PlayPlayerMovement(vel);

        AkSoundEngine.SetRTPCValue("PlayerSpeed", speed);
    }

    private void PlayPlayerMovement(Vector3 vel)
    {



        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            pFootStep.Post(gameObject);
        }
        else if (vel == Vector3.zero)
            pStopFootStep.Post(gameObject);

    }



    ///////////////Begin Animation Functions////////////////


    public void PlayPlayerDeath()
    {
        pDefeated.Post(gameObject);
    }

    public void PlayTakeDamage()
    {

        pTakeDmgGeneric.Post(gameObject);
    }

    //////////END animation Functions////////////







}//END MAIN