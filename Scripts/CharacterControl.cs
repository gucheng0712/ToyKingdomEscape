using UnityEngine;
using System.Collections;

enum TouchDir
{
    None,
    Left,
    Right,
    Up,
    Down,
    Attack
   
}

public class CharacterControl : MonoBehaviour
{

    // Use this for initialization
 
    CharacterController cc;
    private Vector3 moveDir;

    public DeathMenu deathMenu;
    public Transform firePos;
    public GameObject magnet;
    public GameObject sword;
    public GameObject swordLight;
    public GameObject shield;


    private float moveLength;
    private float moveHorizontal;
    private float shootSpeed = 20;
    private float forwardSpeed = 10.0f;
    private float jumpSpeed = 20.0f;
    private float gravity = 60.0f;

    private int h_speed = 15;
    private int laneIndex = 0;
    private int nowIndex = 0;
    private int jumpTimes = 0;

    //Lock player's Horizontal move;
    private bool controlLocked = false;
    private bool canShoot = false;
    private bool canDoubleJump = false;
    private bool isJumping = false;
    private bool isDead = false;
    private bool isShrinking = false;

    // For sound
    private AudioSource source;
    public AudioClip[] audioClip;
    private bool alreadyPlayed = false;

    //For Animation
    private Animator anim;
    private bool shrinkAnim;
    private bool dieAnim;
    private bool attackAnim;
    //    private bool leftAnim;
    //    private bool rightAnim;

    private Vector3 mouseDown = Vector3.zero;

    void Awake()
    {

        // Get audio and animator components when unity is launched
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        //Get CharacterController Component when press play
        cc = GetComponent<CharacterController>();

        // each 20 seconds, player's speed increase 0.5;
        StartCoroutine("SpeedUp");

    }


    void Update()
    {
//        anim.SetBool("LeftSet", leftAnim);
//        anim.SetBool("RightSet", rightAnim);
        anim.SetBool("DieSet", dieAnim);
        anim.SetBool("ShrinkSet", shrinkAnim);
        anim.SetBool("AttackSet", attackAnim);


        // Judge if the player is dead, stop doing anything in the updata function
        if (isDead)
        {
            //Play die animation
            dieAnim = true;
            StartCoroutine("DeathTime");
            return;
        }
        else
        {
            dieAnim = false;
            // increasing score by the time
            Globals.IncreasingScore();
        }
            
        //Player moves forward
        cc.Move(moveDir * Time.deltaTime);
        moveDir.z = forwardSpeed;

        //Control player's movement
        MoveCtrl();
        HorizontalMove();
    }

    TouchDir GetTouchDir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouseUp = Input.mousePosition;
            Vector3 touchOffSet = mouseUp - mouseDown;
            if ((Mathf.Abs(touchOffSet.x) > 50) || (Mathf.Abs(touchOffSet.y) > 50))
            {
                if ((Mathf.Abs(touchOffSet.x) > Mathf.Abs(touchOffSet.y)) && (touchOffSet.x > 0))
                {
                    return TouchDir.Right;
                }
                else if ((Mathf.Abs(touchOffSet.x) > Mathf.Abs(touchOffSet.y)) && (touchOffSet.x < 0))
                {
                    return TouchDir.Left;
                }
                else if ((Mathf.Abs(touchOffSet.x) < Mathf.Abs(touchOffSet.y)) && (touchOffSet.y > 0))
                {
                    return TouchDir.Up;
                }
                else if ((Mathf.Abs(touchOffSet.x) < Mathf.Abs(touchOffSet.y)) && (touchOffSet.y < 0))
                {
                    return TouchDir.Down;
                }
            }
            if (((Mathf.Abs(touchOffSet.x) < 50)) || (Mathf.Abs(touchOffSet.y) < 50))
            {
                return TouchDir.Attack;
            }
        }
 
        return TouchDir.None;
    }

    void MoveCtrl()
    {
        TouchDir dir = GetTouchDir();
        Debug.Log(dir);
        // if player is grounded, he can turn left right or slide
        if ((cc.isGrounded))
        { 
            isJumping = false;
            if ((dir == TouchDir.Left) && (laneIndex > -1) && (!controlLocked))
            {
                PlayAudio(1, 2f);
//              leftAnim = true;
                moveHorizontal = -4;
                laneIndex--;
                controlLocked = true;
            }
//            else
//            {
//                leftAnim = false;
//            }
      
            if ((dir == TouchDir.Right) && (laneIndex < 1) && (!controlLocked))
            {
                PlayAudio(1, 2f);
//              rightAnim = true;
                moveHorizontal = 4;
                laneIndex++;
                controlLocked = true;  
            }
//            else
//            {
//                rightAnim = false;
//            }
            if ((dir == TouchDir.Down) && (!isShrinking))
            {
                PlayAudio(9, 1f);
                //Play animation to let player shrink
                shrinkAnim = true;
                Debug.Log("SLIDE");

                //Change the height of charactercontroller
                cc.height = 1f;

                //Change the center of charactercontroller
                Vector3 tempCenter = cc.center;
                tempCenter.y -= 0.5f;
                cc.center = tempCenter;
                isShrinking = true;

                // Use Coroutine to make the charactercontroller resume
                StartCoroutine("ShrinkTimer");
            }
        }
        else
        {
            //Let player have gravity
            moveDir.y -= gravity * Time.deltaTime;
        }

        //Jump control, double jump condition
        if ((dir == TouchDir.Up) && (!isJumping))
        {
            PlayAudio(0, 1f);
            moveDir.y = jumpSpeed;
            jumpTimes++;
            anim.SetTrigger("JumpSet");

            if (canDoubleJump && (jumpTimes < 2))
            {
                isJumping = false;
            }
            else
            {
                isJumping = true;
                jumpTimes = 0;
            }
        }

        // Attack control, Attack condition
        if ((dir == TouchDir.Attack) && (canShoot))
        {   
            Debug.Log("Shoot it");
            attackAnim = true;
            PlayAudio(8, 1f);

            // Instantiate a prefab to attack
            GameObject shootPrefab = Instantiate(swordLight, firePos.position, Quaternion.identity) as GameObject;
            shootPrefab.GetComponent<Rigidbody>().velocity = shootPrefab.transform.forward * shootSpeed;

            // Destroy this prefab after 1 second
            Destroy(shootPrefab, 1.0f);
        }
        else
        {
            attackAnim = false;
        }
    }


    void HorizontalMove()
    {
        // Change player's position to move left and right at the same distance,
        if ((laneIndex != nowIndex) && (controlLocked))
        {
            // Change player position from 0, to move direction by time
            moveLength = Mathf.Lerp(0, moveHorizontal, h_speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + moveLength, transform.position.y, transform.position.z);
            moveHorizontal -= moveLength;

            /* when the absolute value of the move length is smaller than 0.3f, 
            change the player's position to the ideal position to make sure whatever turn left and turn right at the same distance */
            if (Mathf.Abs(moveHorizontal) < 0.3f)
            {
                transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
                moveHorizontal = 0;
                nowIndex = laneIndex;
                controlLocked = false;
            }
        } 
    }


    // A funtion to use the array's audioClip
    void PlayAudio(int clip, float vol)
    {
        //source.clip = audioClip[clip];
        source.PlayOneShot(audioClip[clip], vol);
    }


    // Death function
    void Dead()
    {
        isDead = true;

        // To make sure the death audio only play once, when the player collide with an obstacle
        if (!alreadyPlayed)
        {
            PlayAudio(4, 0.9f);
            alreadyPlayed = true;
        }

        // Call Globals's GameOver function
        Globals.GameOver();

        // Set a high score when the score is bigger than last time
        if (PlayerPrefs.GetFloat("HighScore") < (Globals.score + Globals.bunus))
        {
            PlayerPrefs.SetFloat("HighScore", (Globals.score + Globals.bunus));
        }    

        // call DeathMenu's EndMenu function
        deathMenu.EndMenu();
    }


    /*  
Since I have used the method of change player's position to turn left and turn right,
I should use OnTriggerEnter instead of use OnControllercColliderHit,
because OnControllerColliderHit only can be called when using charactercontroller.move to hit something
*/
    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Invincible":
                Debug.Log("Invincible");
                PlayAudio(6, 2f);
                Destroy(col.gameObject);
                StartCoroutine("Invincible");
                break;

            case "DoubleJump":
                Debug.Log("Double Jump!");
                PlayAudio(6, 2f);
                Destroy(col.gameObject); 
                StartCoroutine("DoubleJumpTimer");
                break;

            case "ShootAbility":
                Debug.Log("You Can Shoot!");
                PlayAudio(7, 2f);
                Destroy(col.gameObject);
                StartCoroutine("GainShootSkill");
                break;

            case "Magnet":
                PlayAudio(5, 1f);
                Debug.Log("Get Magnet!");
                Destroy(col.gameObject);
                StartCoroutine("GetMagnet");
                break;

            case "Coin":
                Debug.Log("Get 10 Points");
                PlayAudio(2, 0.5f);
                Globals.GetPoint(10);
                Destroy(col.gameObject);
                break;

            case "Diamond":
                Debug.Log("Get 50 Points");
                PlayAudio(3, 3.0f);
                Globals.GetPoint(50);
                Destroy(col.gameObject);
                break;

            case "Animal":
                Debug.Log("Hit an Animal");
                Dead();
                break;

            case "Obstacle":
                Debug.Log("Collide an obstacle!");
                Dead();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Gets the magnet.
    /// </summary>
    /// <returns>The magnet.</returns>
    IEnumerator GetMagnet()
    {
        magnet.SetActive(true);
        Debug.Log("SetTRUE");
        yield return new WaitForSeconds(10f);
        magnet.SetActive(false);
    }

    /// <summary>
    /// Gains the shoot skill.
    /// </summary>
    /// <returns>The shoot skill.</returns>
    IEnumerator GainShootSkill()
    {
        canShoot = true;
        sword.SetActive(true);
        Debug.Log("enable shoot");
        yield return new WaitForSeconds(20f);
        sword.SetActive(false);
        canShoot = false;
    }


    IEnumerator ShrinkTimer()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 tempCenter = cc.center;
        tempCenter.y += 0.5f;
        cc.center = tempCenter;
        cc.height = 2.1f;
        isShrinking = false;
        shrinkAnim = false;
    }

    /// <summary>
    /// Invincible this instance.
    /// </summary>
    IEnumerator Invincible()
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(8f);
        shield.SetActive(false);
    }

    /// <summary>
    /// Doubles the jump timer.
    /// </summary>
    /// <returns>The jump timer.</returns>
    IEnumerator DoubleJumpTimer()
    {
        canDoubleJump = true;
        Debug.Log("CanDoubleJump");
        yield return new WaitForSeconds(30f);
        canDoubleJump = false;
    }

    /// <summary>
    /// Speeds up.
    /// </summary>
    /// <returns>The up.</returns>
    IEnumerator SpeedUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            forwardSpeed += 0.5f;
            gravity += 0.1f;
        }

    }

    /// <summary>
    /// Deaths the time.
    /// </summary>
    /// <returns>The time.</returns>
    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(1f);
    }

   



}
