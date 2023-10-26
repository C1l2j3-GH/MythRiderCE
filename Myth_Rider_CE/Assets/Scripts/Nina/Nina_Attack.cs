using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nina_Attack : MonoBehaviour
{
    [Header("Status")]
    [Header("Preset")]
    [SerializeField] private Image _chargedAtkUI;
    [SerializeField] private Transform _leftFireport;
    [SerializeField] private Transform _rightFireport;
    [SerializeField] private GameObject _chargePE;
    public Rigidbody2D[] rangedAtk;
    public Rigidbody2D[] _chargedAtk;

    [Tooltip("Player's Types of Attack")]
    [Header("Config")]
    public float _chargeSpeed = 0;
    [SerializeField] private float _chargeMaxPercent = 100;
    [SerializeField] private float _shootingAnimCD = 0.2f;

    [Header("Debug")]
    [SerializeField] private float _chargePercent = 0;

    [SerializeField] private float currentPlayerHP;
    [SerializeField] private float currentPlayerMP;
    public static bool _isCharging = false;

    [Space(10f)]
    [SerializeField] private Animator _nina_Animator;
    /// <summary>
    /// Ranged and Melee Attacks are arranged with intergers, 0 = Default Attack
    /// </summary>

    public int currentRangedAtk;
    public int _currentChargedAtk;
    public int currentMeleeAtk;
    public int projectileSpd;
    private int projectileDirection;
    
    [Tooltip("Static")]
    public static bool isShooting;
    public static bool _isOnLeftWall;
    public static bool _isOnRightWall;


    [Tooltip("Player's Types of Attack")]
    /// <summary>
    /// Attack Statistics and Components with Referenced Objects are included here
    /// </summary>
    /// 

    public static Nina_Attack _n_A_Instance;

    void Awake() // Singeton
    {
        if (_n_A_Instance == null)
        {
            _n_A_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        _nina_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Nina_Movement._animIsShooting = false;
        //Nina_Movement._animIsMoveShooting = false;
        //Nina_Movement._animIsDashShooting = false;
        //Nina_Animation.AnimIsShooting(false);
        //Nina_Animation.AnimIsMoveShooting(false);
        //Nina_Animation.AnimIsDashShooting(false);
        //Crack
        SwitchAttackPrefab();
        ////Nina_Movement._isFacingRight = GetComponent<Nina_Movement>().Nina_Movement._isFacingRight;
        currentPlayerMP = GetComponent<Nina_Status>().currentPlayerMP;
        ////_chargedAtkUI.fillAmount = _chargePercent / _chargeMaxPercent; // Charge Attack UI

        if (Menus.gameIsPaused == false && Nina_Status.isChangingForm == false && !Nina_Effects._isKnocked)
        {
            if (Input.GetKey(KeyCode.J) && !_isCharging)
            {
                if (currentPlayerMP >= 20)
                {
                    StartCharging();
                }
            }

            if (Input.GetKeyUp(KeyCode.J) && _isCharging)
            {
                ////FireNormalAtk();
                CheckNormalPjtDir();
                isShooting = false;
                _chargePE.SetActive(false);
                ShootingAnimation();
                StartCoroutine(ShootingAnimCD(_shootingAnimCD, _nina_Animator));
            }
        }
        if(_isCharging)
        {
            UpdateCharge();
        }
        //else if(Input.GetKeyDown(KeyCode.K))
        //{
        //    FireChargedAtk();
        //}
    }

    private void CheckNormalPjtDir()
    {
        if (Nina_Movement._isFacingRight && !_isOnLeftWall && !_isOnRightWall)
        {
            FireNormalAtk(_leftFireport, 1);
        }
        else if (Nina_Movement._isFacingRight && _isOnLeftWall && !_isOnRightWall)
        {
            FireNormalAtk(_leftFireport, 1);
        }
        else if (Nina_Movement._isFacingRight && !_isOnLeftWall && _isOnRightWall)
        {
            FireNormalAtk(_rightFireport, -1);
        }
        else if (!Nina_Movement._isFacingRight && !_isOnLeftWall && !_isOnRightWall)
        {
            FireNormalAtk(_leftFireport, -1);
        }
        else if (!Nina_Movement._isFacingRight && _isOnLeftWall && !_isOnRightWall)
        {
            FireNormalAtk(_rightFireport, 1);
        }
        else if (!Nina_Movement._isFacingRight && !_isOnLeftWall && _isOnRightWall)
        {
            FireNormalAtk(_leftFireport, -1);
        }
    }

    private void CheckChargedPjtDir()
    {
        if (Nina_Movement._isFacingRight && !_isOnLeftWall && !_isOnRightWall)
        {
            FireChargedAtk(_leftFireport, 1);
        }
        else if (Nina_Movement._isFacingRight && _isOnLeftWall && !_isOnRightWall)
        {
            FireChargedAtk(_leftFireport, -1);
        }
        else if (Nina_Movement._isFacingRight && !_isOnLeftWall && _isOnRightWall)
        {
            FireChargedAtk(_rightFireport, -1);
        }
        else if (!Nina_Movement._isFacingRight && !_isOnLeftWall && !_isOnRightWall)
        {
            FireChargedAtk(_leftFireport, -1);
        }
        else if (!Nina_Movement._isFacingRight && _isOnLeftWall && !_isOnRightWall)
        {
            FireChargedAtk(_rightFireport, 1);
        }
        else if (!Nina_Movement._isFacingRight && !_isOnLeftWall && _isOnRightWall)
        {
            FireChargedAtk(_leftFireport, 1);
        }
    }

    public void FireNormalAtk(Transform fireport, int projectileDir) // shooting function, use invoke to allow continuous shooting
        //projectileDir -1 = Shoot Left, 1 = Shoot Right
    {
        Rigidbody2D projectileRB2D;
        // shooting bullet by adding force to bullet
        projectileDirection = projectileDir;
        Debug.Log(projectileDirection);
        projectileRB2D = Instantiate(rangedAtk[currentRangedAtk], fireport.position, transform.rotation); // instantiate ranged attack at fireport position
        projectileRB2D.AddForce(new Vector2(projectileDirection * projectileSpd, 0), ForceMode2D.Impulse);

        isShooting = true;
        GetComponent<Nina_Status>().currentPlayerMP -= 20f;
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Nina's shoot");
        }
        _isCharging = false;
        _chargePercent = 0;
        //if (currentRangedAtk != 0) // if current bullet type is not default
        //{
        //    WeaponAmmo--; // only default bullet has infinite ammo

        //    if (CurrentWeapon == 1)
        //    {
        //        ShootBulletSFX[1].Play(); // play bullet 1 sfx
        //    }
        //    else if (CurrentWeapon == 2)
        //    {
        //        ShootBulletSFX[2].Play(); // play bullet 2 sfx
        //    }
        //}
        //else if (CurrentWeapon == 0)
        //{
        //    ShootBulletSFX[0].Play(); // play default bullet sfx
        //}
    }

    public void FireChargedAtk(Transform fireport, int projectileDir) // shooting function, use invoke to allow continuous shooting
    {
        Rigidbody2D projectileRB2D;

        // shooting bullet by adding force to bullet
        projectileDirection = projectileDir;
        Debug.Log(projectileDirection);
        projectileRB2D = Instantiate(_chargedAtk[_currentChargedAtk], fireport.position, transform.rotation); // instantiate ranged attack at fireport position
        projectileRB2D.AddForce(new Vector2(projectileDirection * projectileSpd, 0), ForceMode2D.Impulse);

        isShooting = true;
        GetComponent<Nina_Status>().currentPlayerMP -= 30f;
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Nina's shoot");
        }
        _isCharging = false;
        _chargePercent = 0;

    }

    private void StartCharging()
    {
        _isCharging = true;
        _chargePE.SetActive(true);
        _chargePercent = 0;
        //Debug.Log("Charging started");
    }

    private void UpdateCharge()
    {
        _chargePercent += _chargeSpeed * Time.deltaTime;
        //Debug.Log(_chargePercent + "% Charged");

        if(_chargePercent >= _chargeMaxPercent)
        {
            //FireChargedAtk();
            CheckChargedPjtDir();
            isShooting = false;
            _chargePE.SetActive(false);
            ShootingAnimation();
            StartCoroutine(ShootingAnimCD(_shootingAnimCD, _nina_Animator));

        }
    }

    private void ShootingAnimation()
    {
        //Nina_Movement._animIsShooting = true;
        //Nina_Animation.AnimIsShooting(true);

        //if (Nina_Animation._animIsMoving)
        //{
        //    //Nina_Movement._animIsShooting = false;
        //    //Nina_Movement._animIsMoveShooting = true;
        //    Nina_Animation.AnimIsShooting(false);
        //    Nina_Animation.AnimIsMoveShooting(true);
        //}

        //if (Nina_Animation._animIsDashing)
        //{
        //    //Nina_Movement._animIsDashing = false;
        //    //Nina_Movement._animIsDashShooting = true;
        //    Nina_Animation.AnimIsDashing(false);
        //    Nina_Animation.AnimIsDashShooting(true);
        //}
        if (Nina_Movement._isDashing && !Nina_Movement._isMoving && !Nina_Movement._isLWallSliding && !Nina_Movement._isRWallSliding)
        {
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoveShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashShooting, true);
        }
        else if(Nina_Movement._isMoving && !Nina_Movement._isDashing && !Nina_Movement._isLWallSliding && !Nina_Movement._isRWallSliding)
        {
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoveShooting, true);
        }
        else if(!Nina_Movement._isMoving && !Nina_Movement._isDashing && !Nina_Movement._isLWallSliding && !Nina_Movement._isRWallSliding)
        {
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoveShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isShooting, true);
        }
        else if(Nina_Movement._isLWallSliding)
        {
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoveShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWSShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWSShooting, true);
        }
        else if (Nina_Movement._isRWallSliding)
        {
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoveShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWSShooting, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWSShooting, true);
        }
    }

    public static IEnumerator ShootingAnimCD(float cd, Animator nina_Animator)
    {
        yield return new WaitForSeconds(cd);
        IFNina_Animation.SetAnimatorBool(nina_Animator, IFNina_Animation._isIdle, true);
        IFNina_Animation.SetAnimatorBool(nina_Animator, IFNina_Animation._isShooting, false);
        IFNina_Animation.SetAnimatorBool(nina_Animator, IFNina_Animation._isMoveShooting, false);
        IFNina_Animation.SetAnimatorBool(nina_Animator, IFNina_Animation._isDashShooting, false);
        IFNina_Animation.SetAnimatorBool(nina_Animator, IFNina_Animation._isLWSShooting, false);
        IFNina_Animation.SetAnimatorBool(nina_Animator, IFNina_Animation._isRWSShooting, false);
    }

    public static void InitializeAttack()
    {
        _isCharging = false;
        isShooting = false;
        _isOnLeftWall = false;
        _isOnRightWall = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            _isOnLeftWall = true;
            ////Debug.Log("_isOnLeftWall:" + _isOnLeftWall);
        }

        if (collision.gameObject.CompareTag("RightWall"))
        {
            _isOnRightWall = true;
            ////Debug.Log("_isOnRightWall:" + _isOnRightWall);
        }
    }
    private void OnCollisionStay2D(Collision2D collision) // detecting if player is grounded
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            _isOnLeftWall = true;
            ////Debug.Log("_isOnLeftWall:" + _isOnLeftWall);
        }

        if (collision.gameObject.CompareTag("RightWall"))
        {
            _isOnRightWall = true;
            ////Debug.Log("_isOnRightWall:" + _isOnRightWall);
        }

        //collision.gameObject.CompareTag("Wall")
    }


    private void OnCollisionExit2D(Collision2D collision) // if player leaves ground, set player to not grounded
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            _isOnLeftWall = false;
            Debug.Log("_isOnLeftWall:" + _isOnLeftWall);
        }

        if (collision.gameObject.CompareTag("RightWall"))
        {
            _isOnRightWall = false;
            Debug.Log("_isOnRightWall:" + _isOnRightWall);
        }
    }

    #region Crack
    private void SwitchAttackPrefab()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (currentRangedAtk == 0)
            {
                currentRangedAtk = 1;
            }
            else if (currentRangedAtk == 1)
            {
                currentRangedAtk = 0;
            }
        }
    }
    #endregion
}
