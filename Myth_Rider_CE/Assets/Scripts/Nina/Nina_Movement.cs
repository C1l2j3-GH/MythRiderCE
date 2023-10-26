using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nina_Movement : MonoBehaviour
{
    [Header("Preset")]
    //[SerializeField] private Nina_Status _nina_Status;
    //[SerializeField] private Menus _Menus;
    public Rigidbody2D _playerRB2D;

    [Header("Config")]
    [Tooltip ("Player's Statistics and Components Config")]
    /// <summary>
    /// Change the Player's Statistics here
    /// </summary>
    public float _playerLMoveSpeed;
    public float _playerRMoveSpeed;
    public float playerJumpHeight;
    [SerializeField] private float _slideSpeed;
    public float _playerDashVel;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _totalDashCD;
    [SerializeField] private float _kbForce;
    [SerializeField] private float _kbCD;
    [SerializeField] private float _kbInviTime;

    //public Animator anim; //By Khai Wen, apply animator


    [Header("Debug")]
    [Tooltip("L/R Movement Debug")]
    [SerializeField] private Animator _nina_Animator;
    [Space(10f)]
    [SerializeField] private float _horiDir;
    [SerializeField] private float _vertiDir;
    [SerializeField] Vector2 _enemyDir;
    public static bool _isMoving;
    public static bool _isJumping;
    public static bool _isDropping;
    public static bool _isDashing;
    public static bool _isLWallSliding;
    public static bool _isRWallSliding;


    [Tooltip("Condition Check Debug")]
    public static bool isGrounded;
    public static bool _isOnWall;
    public static bool _isFacingRight;
    public static bool startFollow;

    [Tooltip("Dash Debug")]
    [SerializeField] private float _dashTimerCount;
    [SerializeField] private float _dashCDCount;
    public static bool _isDashCD = false;
    public static bool _canDash = true;
    [SerializeField] private Vector2 _dashDir;

    //public static bool isDashing; //By Khai Wen, apply animator
    public static Nina_Movement _n_M_Instance;

    void Awake() // Singeton
    {
        if (_n_M_Instance == null)
        {
            _n_M_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _nina_Animator = GetComponent<Animator>();
        _isFacingRight = false;
        _isMoving = false;
        _isJumping = false;
        _isDashing = false;
        IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Nina_Effects._isIdle)
        {
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, true);
        }
        //Debug.Log("_isKnocked = " + Nina_Effects._isKnocked);
        if (!Menus.gameIsPaused && Nina_Effects._isKnocked && !Nina_Status.isChangingForm)
        {
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isTakeDamage, true);
            //Debug.Log("Hitttttt");
        }
        ////if (Menus.gameIsPaused == false && Nina_Status.isChangingForm == false)
        else if (!Menus.gameIsPaused && !Nina_Effects._isKnocked && !Nina_Status.isChangingForm)
        {
            PlayerMovement();
            PlayerDash();
            ExtraAnim();
            DashingCheck();
            OnWall();
            //Debug.Log("_canDash:" + _canDash);
            //Debug.Log("_isDashCD:" + _isDashCD);
            //Debug.Log("_isDashing:" + _isDashing);

        }
    }

    private void PlayerMovement()
    {
        if (!Nina_Attack._isOnLeftWall)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _isMoving = true;

                _horiDir = -1;
                _playerRB2D.velocity = new Vector2(_horiDir * _playerLMoveSpeed, _playerRB2D.velocity.y);

                if (_isFacingRight)
                {
                    FlipPlayer();
                }

                if (_isJumping || _isDropping || _isOnWall)
                {
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, false);
                }
                else if (_isDashing)
                {
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashing, false);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, true);
                    _isDashing = false;
                    _isDashCD = true;
                }
                else if (!_isJumping && !_isDashing && !_isDropping)
                {
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, true);
                    
                    if (AudioManager.amInstance != null)
                    {
                        AudioManager.amInstance.PlaySFLooped("Nina's run");
                    }
                }
            }
        }
        if (!Nina_Attack._isOnRightWall)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _isMoving = true;

                _horiDir = 1;
                _playerRB2D.velocity = new Vector2(_horiDir * _playerRMoveSpeed, _playerRB2D.velocity.y);

                if (!_isFacingRight)
                {
                    FlipPlayer();
                }

                if (_isJumping || _isDropping || _isOnWall)
                {
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, false);
                }
                else if (_isDashing)
                {
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashing, false);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, true);
                    _isDashing = false;
                    _isDashCD = true;
                }
                else if (!_isJumping && !_isDashing && !_isDropping)
                {
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, true);

                    if (AudioManager.amInstance != null)
                    {
                        AudioManager.amInstance.PlaySFLooped("Nina's run");
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _isMoving = false;
            _playerRB2D.velocity = new Vector2(0, _playerRB2D.velocity.y);

            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, true);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, false);

            if (AudioManager.amInstance != null)
            {
                AudioManager.amInstance.StopSF("Nina's run");
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            _isMoving = false;
            _playerRB2D.velocity = new Vector2(0, _playerRB2D.velocity.y);

            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, true);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isMoving, false);
            
            if (AudioManager.amInstance != null)
            {
                AudioManager.amInstance.StopSF("Nina's run");
            }
        }

        if (isGrounded || _isOnWall)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _isJumping = true;
                if (AudioManager.amInstance != null)
                {
                    AudioManager.amInstance.PlaySF("Nina's jump");
                }
                _playerRB2D.AddForce(new Vector2(0f, playerJumpHeight));
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isJumping, true);
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDropping, false);
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWallSliding, false);
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWallSliding, false);
            }
        }
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.K) && _canDash)
        {
            if (isGrounded)
            {
                _isDashing = true;
                _canDash = false;
                //_dashDir = new Vector2(_horiDir, _vertiDir);
                _dashDir = new Vector2(_horiDir, 0);

                _playerRB2D.velocity = _dashDir * _playerDashVel;
                //anim.SetBool("Dash", true); //By khai wen, play dash in animator
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashing, true);
            }
        }
    }
    private void FlipPlayer()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void DashingCheck()
    {
        if (_isDashing)
        {
            _dashTimerCount += Time.deltaTime;
            //Debug.Log("_dashTimerCount" + _dashTimerCount);

            if (_dashTimerCount >= _dashDuration)
            {
                _isDashing = false;
                ////_playerRB2D.velocity = new Vector2(0, 0);
                _playerRB2D.velocity = new Vector2(0, _playerRB2D.velocity.y);
                _isDashCD = true;
                _dashTimerCount = 0;
                IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, true);
            }
        }
        else if (!_isDashing && _isDashCD)
        {
            DashCD();
            //IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, true);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashing, false);
        }
    }
    public void DashCD()
    {
        _dashCDCount += Time.deltaTime;
        //Debug.Log("_dashCDCount" + _dashCDCount);

        if (_dashCDCount >= _totalDashCD)
        {
            _isDashCD = false;
            _canDash = true;
            _dashCDCount = 0;
        }
    }

    private void OnWall()
    {
        if(_isOnWall && !_isJumping && !isGrounded)
        {
            _playerRB2D.velocity = new Vector2(_playerRB2D.velocity.x, -_slideSpeed * Time.deltaTime);
        }
    }
    public void ExtraAnim()
    {
        if (_playerRB2D.velocity.y < 0 && !isGrounded && !_isOnWall)
        {
            _isJumping = false;
            _isDropping = true;
            _isLWallSliding = false;
            _isRWallSliding = false;
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isJumping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDropping, true);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWallSliding, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWallSliding, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
        }
        else if (_playerRB2D.velocity.y <= 0 && !isGrounded && Nina_Attack._isOnLeftWall)
        {
            _isJumping = false;
            _isDropping = false;
            _isLWallSliding = true;
            _isRWallSliding = false;
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isJumping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDropping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWallSliding, true);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWallSliding, false); 
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
        }
        else if (_playerRB2D.velocity.y <= 0 && !isGrounded && Nina_Attack._isOnRightWall)
        {
            _isJumping = false;
            _isDropping = false;
            _isLWallSliding = false;
            _isRWallSliding = true;
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isJumping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDropping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWallSliding, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWallSliding, true);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
        }
        else if(isGrounded)
        {
            _isJumping = false;
            _isDropping = false;
            _isLWallSliding = false;
            _isRWallSliding = false;
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isJumping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDropping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWallSliding, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWallSliding, false);
        }
        else if(_playerRB2D.velocity.y > 0 && !isGrounded)
        {
            _isJumping = true;
            _isDropping = false;
            _isLWallSliding = false;
            _isRWallSliding = false;

            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isJumping, true);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDropping, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isLWallSliding, false);
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isRWallSliding, false); 
            IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, false);
        }

        IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isTakeDamage, false);
    }

    public static void InitializeMovement()
    {
        _isMoving = false;
        _isJumping = false;
        _isDropping = false;
        _isDashing = false;
        _isLWallSliding = false;
        _isRWallSliding = false;
        //isGrounded = false;
        _isOnWall = false;
        //_isFacingRight = false;
        startFollow = false;
        _isDashCD = false;
        _canDash = true;
        Nina_Effects._isKnocked = false;
        Nina_Effects._isKnocking = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }


        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            if (isGrounded != true)
            {
                _isOnWall = true;
            }
            else
            {
                if (_isDashing)
                {
                    _playerRB2D.velocity = new Vector2(0, _playerRB2D.velocity.y);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isIdle, true);
                    IFNina_Animation.SetAnimatorBool(_nina_Animator, IFNina_Animation._isDashing, false);
                    _isDashing = false;
                    _isDashCD = true;
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision) // detecting if player is grounded
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }


        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            if (isGrounded != true)
            {
                _isOnWall = true;
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision) // if player leaves ground, set player to not grounded
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            _isOnWall = false;
        }
        //if (collision.gameObject.CompareTag("EnemyPlatform"))
        //{
        //    isGrounded = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPlatform"))
        {
            //isGrounded = true;
            startFollow = true;
        }

        //if (collision.gameObject.CompareTag("EnemyPjt")
        //    || collision.gameObject.CompareTag("EnemyPjtFront")
        //    || collision.gameObject.CompareTag("EnemyDroppedPjt"))
        if(collision.GetComponent<DealDamageOnce_E>() != null)
        {
            if (!Nina_Effects._isKnocking)
            {
                Nina_Effects._isKnocking = true;

                _enemyDir = collision.gameObject.transform.position - transform.position;
                //Debug.Log("Direction = " + _enemyDir);
                _enemyDir.Normalize();
                //Debug.Log("Normalized Direction = " + _enemyDir);

                if (_enemyDir.x >= 0f)
                {
                    _enemyDir.x = 0.5f;
                }

                if (_enemyDir.x < 0f)
                {
                    _enemyDir.x = -0.5f;
                }

                if (_enemyDir.y >= 0f)
                {
                    _enemyDir.y = 0.2f;
                }

                if (_enemyDir.y < 0f)
                {
                    _enemyDir.y = -0.2f;
                }

                ////playerRB2D.AddForce(5 * -_kbForce * _enemyDir, ForceMode2D.Force);
                StartCoroutine(Nina_Effects.KnockbackCD(_playerRB2D, _kbForce, _enemyDir, _kbCD, _kbInviTime));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Enemy1")
        //    || collision.gameObject.CompareTag("Enemy2")
        //    || collision.gameObject.CompareTag("SpawnedCreep"))
        if (collision.GetComponent<DealDamageCont_E>() != null)
        {
            if (!Nina_Effects._isKnocking)
            {
                Nina_Effects._isKnocking = true;

                _enemyDir = collision.gameObject.transform.position - transform.position;
                //Debug.Log("Direction = " + _enemyDir);
                _enemyDir.Normalize();
                //Debug.Log("Normalized Direction = " + _enemyDir);

                if (_enemyDir.x >= 0f)
                {
                    _enemyDir.x = 0.5f;
                }

                if (_enemyDir.x < 0f)
                {
                    _enemyDir.x = -0.5f;
                }

                if (_enemyDir.y >= 0f)
                {
                    _enemyDir.y = 0.2f;
                }

                if (_enemyDir.y < 0f)
                {
                    _enemyDir.y = -0.2f;
                }

                ////playerRB2D.AddForce(_enemyDir * -_kbForce, ForceMode2D.Force);
                StartCoroutine(Nina_Effects.KnockbackCD(_playerRB2D, _kbForce, _enemyDir, _kbCD, _kbInviTime));
            }
        }
    }
}
