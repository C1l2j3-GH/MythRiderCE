using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2BossAI : DealDamageCont_E, IFTakeDamage_E
{
    #region Preset
    [Header("Preset")]
    [Header("Level 2 Boss Preset")]
    [Tooltip("Level 2 Boss Preset")]
    [SerializeField] private GameObject _ninaGO;
    [SerializeField] private Transform _frontFP;
    [SerializeField] private Transform _rearFP;
    [SerializeField] private Transform _bottomFP;
    [SerializeField] private Transform _leftPos;
    [SerializeField] private Transform _rightPos;
    [SerializeField] private Animator _l2BossAnimtr;
    [SerializeField] private GameObject _cutSceneGO;
    [SerializeField] private GameObject _deathLight;


    //[Space(5f)]
    [Header("Attack 1 Preset")]
    [Tooltip("Attack 1 Preset")]
    [SerializeField] private GameObject _atk1TornadoPrefab;

    [Header("Attack 2 Preset")]
    [Tooltip("Attack 2 Preset")]
    [SerializeField] private GameObject _shockwavePrefab;
    [SerializeField] private Transform _atk2TopLeftPos;
    [SerializeField] private Transform _atk2BottomLeftPos;
    [SerializeField] private Transform _atk2TopRightPos;
    [SerializeField] private Transform _atk2BottomRightPos;
    [SerializeField] private Transform _atk2LCheckPos;
    [SerializeField] private Transform _atk2M1CheckPos;
    [SerializeField] private Transform _atk2M2CheckPos;
    [SerializeField] private Transform _atk2RCheckPos;
    [SerializeField] private Transform _atk2LLandPos;
    [SerializeField] private Transform _atk2MLandPos;
    [SerializeField] private Transform _atk2RLandPos;

    [Header("Attack 4 Preset")]
    [Tooltip("Attack 4 Preset")]
    [SerializeField] private GameObject _dropBombPrefab;
    [SerializeField] private Transform _atk4TopLeftPos;
    [SerializeField] private Transform _atk4TopRightPos;
    
    [Header("Limit Break Preset")]
    [Tooltip("Limit Break Preset")]
    [SerializeField] private Transform _lbTopMidPos;
    [SerializeField] private GameObject _thunderPrefab;
    [SerializeField] private GameObject _lbWarnSignPrefab;

    [Header("Attack 5 Preset")]
    [Tooltip("Attack 5 Preset")]
    [SerializeField] private Transform _atk5TopLeftPos;
    [SerializeField] private Transform _atk5TopMidPos;
    [SerializeField] private Transform _atk5TopRightPos;
    [SerializeField] private Transform _atk5MidLeftPos;
    [SerializeField] private Transform _atk5MidPos;
    [SerializeField] private Transform _atk5MidRightPos;
    [SerializeField] private Transform _atk5BotLeftPos;
    [SerializeField] private Transform _atk5BotMidPos;
    [SerializeField] private Transform _atk5BotRightPos;
    [SerializeField] private GameObject _atk5TornadoPrefab;

    #endregion

    #region Config
    [Header("Config")]
    [Header("L2Boss Statistics Config")]
    [Tooltip("L2Boss Statistics Config")]
    public float _maxL2BossHP;
    public float _currentL2BossHP;
    public float _l1BossHPRegenRate;
    [SerializeField] private int _atk6Threshold = 3;

    [Header("Level 2 Boss Config")]
    [Tooltip("Level 2 Boss Config")]
    [SerializeField] private bool _isResetAnim;
    [SerializeField] private bool _startAtk;
    [SerializeField] private float _l2BossMoveSpd;
    [SerializeField] private float _l1BossRotateSpd;
    [SerializeField] private float _l2BossMinDistance;
    [Space(5f)]
    [SerializeField] private float _atk1CD = 2f;
    [SerializeField] private float _atk2CD = 2f;
    [SerializeField] private float _atk3CD = 2f;
    [SerializeField] private float _atk4CD = 2f;
    [SerializeField] private float _atk5CD = 2f;
    [SerializeField] private float _atk6CD = 2f;

    //[Space(5f)]
    [Header("Attack 2 Config")]
    [Tooltip("Attack 2 Config")]
    [SerializeField] private float _l2BossAtk2LRMoveSpd = 50f;
    [SerializeField] private float _l2BossAtk2UpMoveSpd = 10f;
    [SerializeField] private float _l2BossAtk2SlamMoveSpd = 70f;

    [Header("Attack 3 Config")]
    [Tooltip("Attack 3 Config")]
    [SerializeField] private float _atk3NinaMinDist = 5f;
    [SerializeField] private float _atk3DownXOffSet = -2f;
    [SerializeField] private float _atk3DownYOffSet = 2f;
    [SerializeField] private float _l2BossAtk3LRMoveSpd = 10f;
    [SerializeField] private float _l2BossAtk3UpMoveSpd = 10f;

    [Header("Attack 4 Config")]
    [Tooltip("Attack 4 Config")]
    [SerializeField] private float _l2BossAtk4MoveSpd = 10f;
    [SerializeField] private float _atk4DropBombCD = 0.2f;
    
    [Header("Limit Break Config")]
    [Tooltip("Limit Break Config")]
    [SerializeField] private float _lbThunderCD = 3f;
    [SerializeField] private float _lbThunderDuration = 0.5f;
    [SerializeField] private float _lbThunderDelay = 0.5f;

    [Header("Attack 5 Config")]
    [Tooltip("Attack 5 Config")]
    [SerializeField] private float _l2BossAtk5MoveSpd = 10f;
    [SerializeField] private float _atk5MovementCD = 0.5f;

    #endregion


    #region Debug
    [Header("Debug")]
    [Tooltip("L2Boss Debug")]
    [SerializeField] private bool _isSpawningL2Boss;
    [SerializeField] private Rigidbody2D _l2BossRb2D;
    [SerializeField] private int _currentAtkNum;
    [SerializeField] private int _atkLaunchedNum;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private bool _halfHPLBLaunched;
    [SerializeField] private bool _isInitializedAnim;
    [SerializeField] private float _l2BossCurrentDist;
    [SerializeField] private List<int> _atkArrangement = new() { 1, 2, 3, 4 };


    [Header("Attack 1 Debug")]
    [Tooltip("Attack 1 Debug")]
    [SerializeField] private GameObject _tornadoGO;
    [SerializeField] private bool _isFireAtk1Tornado;

    [Header("Attack 2 Debug")]
    [Tooltip("Attack 2 Debug")]
    public int _atk2Movement;
    [SerializeField] private Vector2 _atk2PlayerDir;
    [SerializeField] private Vector2 _landingPos;
    [SerializeField] private bool _isSpawnAtk2ShockWave;
    [SerializeField] private GameObject _shockwaveGO;

    [Header("Attack 3 Debug")]
    [Tooltip("Attack 3 Debug")]
    public int _atk3Movement;
    [SerializeField] private float _atk3CurrentDistX;
    [SerializeField] private Vector2 _atk3PlayerDir;
    [SerializeField] private Vector2 _posBeforeHit;
    [SerializeField] private Vector2 _onAirPos;
    [SerializeField] private bool _isFireAtk3Tornado;

    [Header("Attack 4 Debug")]
    [Tooltip("Attack 4 Debug")]
    [SerializeField] private int _atk4Movement;
    [SerializeField] private Vector2 _atk4PlayerDir;
    [SerializeField] private float _atk4CurrentDistY;
    [SerializeField] private float _atk4DBTimeCount;
    [SerializeField] private bool _isDropAtk4Bomb;
    //[SerializeField] private Coroutine _atk4DBCoroutine;
    
    [Header("Limit Break Debug")]
    [Tooltip("Limit Break Debug")]
    public int _lbMovement;
    [SerializeField] private Coroutine _thunderCoroutine;
    [SerializeField] private bool _isDropLBThunder;

    [Header("Attack 5 Debug")]
    [Tooltip("Attack 5 Debug")]
    public int _atk5Movement;
    [SerializeField] private bool _atk5IsTeleported = false;
    [SerializeField] private bool _atk5GetNinaPos = false;
    [SerializeField] private bool _atk5IsMoving = false;
    [SerializeField] private bool _isFireAtk5Tornado = false;
    [SerializeField] private float _atk5MovementCDCount;
    [SerializeField] private Vector3 _ninaTempPos;
    [SerializeField] private GameObject[] _atk5TornadoGOs;

    [Header("Static")]
    [Tooltip("L2Boss Static")]
    public static bool _isDoneAtking;
    public static bool _isFlippedL2Boss;

    //[Tooltip("Attack 6 Static")]

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _l2BossRb2D = GetComponent<Rigidbody2D>();
        _ninaGO = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        _isSpawningL2Boss = true;
        _ninaGO = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ResetBoolParameters(); // TBR: To reset animation booleans in editor
        InitializeAnim();

        SpawnBoss();
        
        if (_startAtk)
        {
            Check_SwitchAttack(_isAttacking);
            _l2BossAnimtr.SetInteger("currentAttack", _currentAtkNum);
        }

        if (_currentL2BossHP <= 0)
        {
            //_l1BossIsDead = true;
            Menus.PauseGame(true, 0f);
            Menus._otherMenuOn = true;
            _deathLight.SetActive(true);
            //_cutSceneGO.SetActive(true);
            ////Destroy(gameObject);
        }

        if (_isDropLBThunder)
        {
            StartCoroutine(LBDropThunder(_lbThunderDelay, _lbThunderDuration, _lbThunderCD));
        }
    }
    private void Check_SwitchAttack(bool isAttacking)
    {
        if (!isAttacking)
        {
            switch (_currentAtkNum)
            {
                default:
                    if (!_isFireAtk1Tornado)
                    {
                        _l2BossAnimtr.SetBool("isIdle", false);
                        _l2BossAnimtr.SetBool("isShooting", true);
                        _tornadoGO = Instantiate(_atk1TornadoPrefab, _frontFP.position, _frontFP.rotation, transform);
                        _isFireAtk1Tornado = true;
                    }

                    _isDoneAtking = true;

                    break;

                case 2:
                    
                    Atk2Movement();

                    if(_isSpawnAtk2ShockWave)
                    {
                        _shockwaveGO = Instantiate(_shockwavePrefab, _bottomFP.position, _bottomFP.rotation, transform);
                        _isSpawnAtk2ShockWave = false;
                    }
                    
                    _isDoneAtking = true;

                    break;

                case 3:

                    Atk3Movement();
                    
                    if(_isFireAtk3Tornado)
                    {
                        _tornadoGO = Instantiate(_atk1TornadoPrefab, _frontFP.position, _frontFP.rotation, transform);
                        _isFireAtk3Tornado = false;
                    }

                    _isDoneAtking = true;


                    break;

                case 4:

                    Atk4Movement();

                    if(_isDropAtk4Bomb)
                    {
                        if(_atk4DBTimeCount < _atk4DropBombCD)
                        {
                            _atk4DBTimeCount += Time.deltaTime;
                        }
                        else
                        {
                            Instantiate(_dropBombPrefab, _bottomFP.position, _bottomFP.rotation);
                            _atk4DBTimeCount = 0;
                        }
                    }

                    _isDoneAtking = true;


                    break;

                case 5:
                    Atk5Movement();

                    if (_isFireAtk5Tornado)
                    {
                        _l2BossAnimtr.SetBool("isIdle", false);
                        _l2BossAnimtr.SetBool("isShooting", true);
                        _isFireAtk5Tornado = false;
                        _atk5TornadoGOs[0] = Instantiate(_atk5TornadoPrefab, _frontFP.position, _frontFP.rotation, transform);
                        Quaternion rotation = Quaternion.Euler(0, 180, 0);
                        _atk5TornadoGOs[1] = Instantiate(_atk5TornadoPrefab, _rearFP.position, rotation, transform);
                    }

                    _isDoneAtking = true;


                    break;

                case 6:
                    Atk6Movement();

                    _isDoneAtking = true;

                    break;
                
                case 7:
                    LimitBreakMovement();

                    _isDoneAtking = true;

                    break;
            }
        }
    }
    private void SpawnBoss()
    {
        if (_isSpawningL2Boss)
        {
            _currentL2BossHP += _maxL2BossHP / 2 * Time.deltaTime;

            if (_currentL2BossHP >= _maxL2BossHP)
            {
                _currentL2BossHP = _maxL2BossHP;
                _currentAtkNum = RandomAttack();
                _startAtk = true;
                _isSpawningL2Boss = false;
            }
        }
    }
    private int RandomAttack()
    {
        int randomAtkNum = 4;
        int randomIndex;

        if (_currentL2BossHP > _maxL2BossHP / 2)
        {
            if (_atkArrangement.Count > 0)
            {
                // Generate a random index to pick a number from the list
                randomIndex = Random.Range(0, _atkArrangement.Count);

                // Get the random number from the list
                randomAtkNum = _atkArrangement[randomIndex];

                // Remove the picked number from the list
                _atkArrangement.RemoveAt(randomIndex);

                ////Debug.Log("Picked number: " + randomAtkNum);

                return randomAtkNum;
            }
            else
            {
                //Debug.Log("Number list is empty.");
                _atkArrangement = new() { 1, 2, 3, 4 };

                //Debug.Log("_atkArrangement = " + _atkArrangement);

                // Generate a random index to pick a number from the list
                randomIndex = Random.Range(0, _atkArrangement.Count);

                // Get the random number from the list
                randomAtkNum = _atkArrangement[randomIndex];

                // Remove the picked number from the list
                _atkArrangement.RemoveAt(randomIndex);

                ////Debug.Log("Picked number: " + randomAtkNum);

                return randomAtkNum;
            }
        }
        else if (_currentL2BossHP <= _maxL2BossHP / 2)
        {
            if (!_halfHPLBLaunched)
            {
                randomAtkNum = 7;
                _atkArrangement = new() { 1, 2, 3, 4, 5 };

                return randomAtkNum;
            }
            else if (_halfHPLBLaunched && _atkArrangement.Count > 0)
            {
                // Generate a random index to pick a number from the list
                randomIndex = Random.Range(0, _atkArrangement.Count);

                // Get the random number from the list
                randomAtkNum = _atkArrangement[randomIndex];

                // Remove the picked number from the list
                _atkArrangement.RemoveAt(randomIndex);

                ////Debug.Log("Picked number after Limit Break: " + randomAtkNum);

                return randomAtkNum;
            }
            else
            {
                //Debug.Log("Number list is empty after Limit Break.");
                _atkArrangement = new() { 1, 2, 3, 4, 5 };

                // Generate a random index to pick a number from the list
                randomIndex = Random.Range(0, _atkArrangement.Count);

                // Get the random number from the list
                randomAtkNum = _atkArrangement[randomIndex];

                // Remove the picked number from the list
                _atkArrangement.RemoveAt(randomIndex);

                ////Debug.Log("Picked number: " + randomAtkNum);

                return randomAtkNum;
            }
        }

        return randomAtkNum;
    }
    private IEnumerator StartAtkCD(float cd)
    {
        ////Debug.Log("Waiting for CD");
        _isAttacking = true;
        _isDoneAtking = false;
        yield return new WaitForSeconds(cd);
        ////Debug.Log("CD done");

        _currentAtkNum = RandomAttack();
        //Debug.Log("_currentAtkNum = " + _currentAtkNum);
        _atk2Movement = 0;
        _atk3Movement = 0;
        _atk4Movement = 0;
        _atk5Movement = 0;
        _lbMovement = 0;
        _isAttacking = false;
    }

    ////private void StopAtkCDCoroutine()
    ////{
    ////    if (_atkCDCoroutine != null)
    ////    {
    ////        StopCoroutine(_atkCDCoroutine);
    ////        _atkCDCoroutine = null;
    ////    }
    ////}
    private void AtkCDCheck(float atkCD)
    {
        if (_isDoneAtking)
        {
            StartCoroutine(StartAtkCD(atkCD));
        }
    }
    public void FlipBoss()
    {
        Vector3 newScale = new(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localScale = newScale;
    }

    public void Atk2Movement()
    {
        if (_atk2Movement == 0)
        {
            if (!_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2BottomLeftPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isIdle", false);
                    _l2BossAnimtr.SetBool("isDashing", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2BottomLeftPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk2LRMoveSpd, playerDir01.y * _l2BossAtk2LRMoveSpd);
                    //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                }

                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    //Debug.Log("Atk5 Stop Flying");
                    _atk2Movement = 1;
                }
            }
            else if (_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2BottomRightPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isIdle", false);
                    _l2BossAnimtr.SetBool("isDashing", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2BottomRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk2LRMoveSpd, playerDir01.y * _l2BossAtk2LRMoveSpd);
                    //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                }

                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    //Debug.Log("Atk5 Stop Flying");
                    _atk2Movement = 1;
                }
            }
        }
        else if (_atk2Movement == 1)
        {
            if (!_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopLeftPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isDashing", false);
                    _l2BossAnimtr.SetBool("isInAir", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopLeftPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk2UpMoveSpd, playerDir01.y * _l2BossAtk2UpMoveSpd);
                }

                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _atk2Movement = 2;
                }
            }
            else if (_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopRightPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isDashing", false);
                    _l2BossAnimtr.SetBool("isInAir", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk2UpMoveSpd, playerDir01.y * _l2BossAtk2UpMoveSpd);
                }
                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _atk2Movement = 2;
                }
            }
        }
        else if (_atk2Movement == 2)
        {
            if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
            {
                FlipBoss();
            }
            Vector2 ninaTempPos = _ninaGO.transform.position;

            if((ninaTempPos.x < _atk2LCheckPos.position.x)
                || (ninaTempPos.x >= _atk2LCheckPos.position.x && ninaTempPos.x < _atk2M1CheckPos.position.x))
            {
                _landingPos = _atk2LLandPos.position;
            }
            else if (ninaTempPos.x >= _atk2M1CheckPos.position.x && ninaTempPos.x < _atk2M2CheckPos.position.x)
            {
                _landingPos = _atk2MLandPos.position;
            }
            else if (ninaTempPos.x >= _atk2M2CheckPos.position.x && ninaTempPos.x < _atk2RCheckPos.position.x)
            {
                _landingPos = _atk2RLandPos.position;
            }

            _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_landingPos, transform.position);

            if (_l2BossCurrentDist > _l2BossMinDistance)
            {
                _l2BossAnimtr.SetBool("isInAir", false);
                _l2BossAnimtr.SetBool("isFGround", true);
                Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_landingPos, transform.position);
                _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk2SlamMoveSpd, playerDir01.y * _l2BossAtk2SlamMoveSpd);
            }

            else if (_l2BossCurrentDist <= _l2BossMinDistance)
            {
                _l2BossRb2D.velocity = Vector2.zero;
                _l2BossAnimtr.SetBool("isFGround", false);
                _l2BossAnimtr.SetBool("isHitGround", true);
                _atk2Movement = 3;
            }
        }
        else if(_atk2Movement == 3)
        {
            _isSpawnAtk2ShockWave = true;
            _atk2Movement = 4;
        }
        else if (_atk2Movement == 4)
        {

        }
    }
    public void Atk3Movement()
    {
        if (_atk3Movement == 0)
        {
            _atk3CurrentDistX = Mathf.Abs(_ninaGO.transform.position.x - transform.position.x);
            Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_ninaGO.transform.position, transform.position);

            if (!_isFlippedL2Boss)
            {
                if (playerDir01.x <= 0)
                {
                    if (_atk3CurrentDistX > _atk3NinaMinDist)
                    {
                        _l2BossAnimtr.SetBool("isIdle", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk3LRMoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }
                    else if (_atk3CurrentDistX <= _atk3NinaMinDist)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        //Play atk animation
                        _l2BossAnimtr.SetBool("isDashing", false);
                        _l2BossAnimtr.SetBool("isDashAttack1", true);
                        //Debug.Log("Atk5 Stop Flying");
                    }
                }
                else
                {
                    if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
                    {
                        FlipBoss();
                    }

                    if (_atk3CurrentDistX > _atk3NinaMinDist)
                    {
                        _l2BossAnimtr.SetBool("isIdle", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk3LRMoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }
                    else if (_atk3CurrentDistX <= _atk3NinaMinDist)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        //Play atk animation
                        _l2BossAnimtr.SetBool("isDashing", false);
                        _l2BossAnimtr.SetBool("isDashAttack1", true);
                        //Debug.Log("Atk5 Stop Flying");
                        _isFlippedL2Boss = !_isFlippedL2Boss;
                    }
                }
            }
            else if (_isFlippedL2Boss)
            {
                if (playerDir01.x > 0)
                {
                    if (_atk3CurrentDistX > _atk3NinaMinDist)
                    {
                        _l2BossAnimtr.SetBool("isIdle", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk3LRMoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }
                    else if (_atk3CurrentDistX <= _atk3NinaMinDist)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        //Play atk animation
                        //Debug.Log("Atk5 Stop Flying");
                        _l2BossAnimtr.SetBool("isDashing", false);
                        _l2BossAnimtr.SetBool("isDashAttack1", true);
                    }
                }
                else
                {
                    if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
                    {
                        FlipBoss();
                    }

                    if (_atk3CurrentDistX > _atk3NinaMinDist)
                    {
                        _l2BossAnimtr.SetBool("isIdle", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk3LRMoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }
                    else if (_atk3CurrentDistX <= _atk3NinaMinDist)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        //Play atk animation
                        _l2BossAnimtr.SetBool("isDashing", false);
                        _l2BossAnimtr.SetBool("isDashAttack1", true);
                        //Debug.Log("Atk5 Stop Flying");
                        _isFlippedL2Boss = !_isFlippedL2Boss;
                    }
                }
            }
        }
        else if (_atk3Movement == 1)
        {
            _l2BossAnimtr.SetBool("isDashAttack1", false);
            _l2BossAnimtr.SetBool("isInAir", true);

            if (_posBeforeHit == Vector2.zero)
            {
                _posBeforeHit = new(transform.position.x, transform.position.y);
            }

            if (!_isFlippedL2Boss)
            {
                if (_onAirPos == Vector2.zero)
                {
                    _onAirPos = new(transform.position.x + _atk3DownXOffSet, transform.position.y + _atk3DownYOffSet);
                }

                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_onAirPos, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isDashing", false);
                    _l2BossAnimtr.SetBool("isInAir", true);

                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_onAirPos, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk3UpMoveSpd, playerDir01.y * _l2BossAtk3UpMoveSpd);
                }

                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _l2BossAnimtr.SetBool("isInAir", false);
                    _l2BossAnimtr.SetBool("isDashAttack2", true);
                }
            }
            else if (_isFlippedL2Boss)
            {
                if (_onAirPos == Vector2.zero)
                {
                    _onAirPos = new(transform.position.x - _atk3DownXOffSet, transform.position.y + _atk3DownYOffSet);
                }

                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_onAirPos, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isDashing", false);
                    _l2BossAnimtr.SetBool("isInAir", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_onAirPos, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk3UpMoveSpd, playerDir01.y * _l2BossAtk3UpMoveSpd);
                }

                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _l2BossAnimtr.SetBool("isInAir", false);
                    _l2BossAnimtr.SetBool("isDashAttack2", true);
                }
            }
        }
        else if (_atk3Movement == 2)
        {
            _l2BossAnimtr.SetBool("isDashAttack2", false);
            _l2BossAnimtr.SetBool("isInAir", true);

            _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_posBeforeHit, transform.position);

            if (_l2BossCurrentDist > 0.001)
            {
                //_l2BossAnimtr.SetBool("isJumpingAtk5", false);
                //_l2BossAnimtr.SetBool("isGShooting", true);
                Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_posBeforeHit, transform.position);
                _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk3UpMoveSpd, playerDir01.y * _l2BossAtk3UpMoveSpd);
            }

            else if (_l2BossCurrentDist <= 0.001)
            {
                _l2BossRb2D.velocity = Vector2.zero;

                _posBeforeHit = Vector2.zero;
                _onAirPos = Vector2.zero;
                _atk3Movement = 3;
            }
        }
        else if (_atk3Movement == 3)
        {
            _l2BossAnimtr.SetBool("isInAir", false);
            _l2BossAnimtr.SetBool("isShooting", true);

            if (!_isFlippedL2Boss)
            {
                Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_ninaGO.transform.position, transform.position);

                if (playerDir01.x <= 0)
                {

                    _isFireAtk3Tornado = true;
                    _atk3Movement = 4;
                }
                else
                {
                    FlipBoss(); 
                    _isFireAtk3Tornado = true;
                    _atk3Movement = 4;
                    _isFlippedL2Boss = !_isFlippedL2Boss;
                }
            }
            else if (_isFlippedL2Boss)
            {
                Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_ninaGO.transform.position, transform.position);

                if (playerDir01.x > 0)
                {
                    _isFireAtk3Tornado = true;
                    _atk3Movement = 4;
                }
                else
                {
                    FlipBoss();
                    _isFireAtk3Tornado = true;
                    _atk3Movement = 4;
                    _isFlippedL2Boss = !_isFlippedL2Boss;
                }
            }
        }
        else if(_atk3Movement == 4)
        {

        }
        ////else if (_atk3Movement == 4)
        ////{
        ////    if (!_isFlippedL2Boss)
        ////    {
        ////        _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_rightPos.position, transform.position);

        ////        if (_l2BossCurrentDist > 0.001)
        ////        {
        ////            //_l2BossAnimtr.SetBool("isJumpingAtk5", false);
        ////            //_l2BossAnimtr.SetBool("isGShooting", true);
        ////            Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_rightPos.position, transform.position);
        ////            _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossMoveSpd, 0);
        ////        }

        ////        else if (_l2BossCurrentDist <= 0.001)
        ////        {
        ////            _l2BossRb2D.velocity = Vector2.zero;
        ////            _atk3Movement = 4;
        ////        }
        ////    }
        ////    else if (_isFlippedL2Boss)
        ////    {
        ////        _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_leftPos.position, transform.position);

        ////        if (_l2BossCurrentDist > 0.001)
        ////        {
        ////            //_l2BossAnimtr.SetBool("isJumpingAtk5", false);
        ////            //_l2BossAnimtr.SetBool("isGShooting", true);
        ////            Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_leftPos.position, transform.position);
        ////            _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossMoveSpd, 0);
        ////        }

        ////        else if (_l2BossCurrentDist <= 0.001)
        ////        {
        ////            FlipBoss();
        ////            _l2BossRb2D.velocity = Vector2.zero;
        ////            _atk3Movement = 4;
        ////        }
        ////    }
        ////}
    }

    public void Atk4Movement()
    {
        if (_atk4Movement == 0)
        {
            _atk4CurrentDistY = Mathf.Abs(_atk2TopRightPos.transform.position.y - transform.position.y);

            if (!_isFlippedL2Boss)
            {
                if (_atk4CurrentDistY > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isIdle", false);
                    _l2BossAnimtr.SetBool("isInAir", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(0, playerDir01.y * _l2BossAtk4MoveSpd);
                }

                else if (_atk4CurrentDistY <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _atk4Movement = 1;
                }
            }
            else if (_isFlippedL2Boss)
            {
                if (_atk4CurrentDistY > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isIdle", false);
                    _l2BossAnimtr.SetBool("isInAir", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(0, playerDir01.y * _l2BossAtk4MoveSpd);
                }
                else if (_atk4CurrentDistY <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _atk4Movement = 1;
                }
            }
        }
        else if (_atk4Movement == 1)
        {
            if (_atk4PlayerDir == Vector2.zero)
            {
                _atk4PlayerDir = L2BossFunctions.NormalizedDir(_ninaGO.transform.position, transform.position);
            }

            if (!_isFlippedL2Boss)
            {
                if (_atk4PlayerDir.x <= 0)
                {
                    _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopLeftPos.position, transform.position);

                    if (_l2BossCurrentDist > _l2BossMinDistance)
                    {
                        _l2BossAnimtr.SetBool("isInAir", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopLeftPos.position, transform.position);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }

                    else if (_l2BossCurrentDist <= _l2BossMinDistance)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        _atk4PlayerDir = Vector2.zero;
                        //Debug.Log("Atk5 Stop Flying");
                        _atk4Movement = 2;
                    }
                }
                else
                {
                    if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
                    {
                        FlipBoss();
                    }
                    
                    _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopRightPos.position, transform.position);

                    if (_l2BossCurrentDist > _l2BossMinDistance)
                    {
                        _l2BossAnimtr.SetBool("isInAir", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopRightPos.position, transform.position);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }

                    else if (_l2BossCurrentDist <= _l2BossMinDistance)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        _atk4PlayerDir = Vector2.zero;
                        _isFlippedL2Boss = !_isFlippedL2Boss;
                        //Debug.Log("Atk5 Stop Flying");
                        _atk4Movement = 2;
                    }
                }
            }
            else if (_isFlippedL2Boss)
            {
                if (_atk4PlayerDir.x > 0)
                {
                    _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopRightPos.position, transform.position);

                    if (_l2BossCurrentDist > _l2BossMinDistance)
                    {
                        _l2BossAnimtr.SetBool("isInAir", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopRightPos.position, transform.position);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }

                    else if (_l2BossCurrentDist <= _l2BossMinDistance)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        _atk4PlayerDir = Vector2.zero;
                        //Debug.Log("Atk5 Stop Flying");
                        _atk4Movement = 2;
                    }
                }
                else
                {
                    if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
                    {
                        FlipBoss();
                    }

                    _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopLeftPos.position, transform.position);

                    if (_l2BossCurrentDist > _l2BossMinDistance)
                    {
                        _l2BossAnimtr.SetBool("isInAir", false);
                        _l2BossAnimtr.SetBool("isDashing", true);
                        Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopLeftPos.position, transform.position);
                        _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                        //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
                    }

                    else if (_l2BossCurrentDist <= _l2BossMinDistance)
                    {
                        _l2BossRb2D.velocity = Vector2.zero;
                        _atk4PlayerDir = Vector2.zero;
                        _isFlippedL2Boss = !_isFlippedL2Boss;
                        //Debug.Log("Atk5 Stop Flying");
                        _atk4Movement = 2;
                    }
                }
            }
        }
        else if (_atk4Movement == 2)
        {
            if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
            {
                FlipBoss();
            }

            if (!_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopRightPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = true;
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                }

                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = false;
                    _l2BossRb2D.velocity = Vector2.zero;
                    _isFlippedL2Boss = !_isFlippedL2Boss;
                    _atk4Movement = 3;
                }
            }
            else if (_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk2TopLeftPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = true;
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2TopLeftPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                }
                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = false;
                    _l2BossRb2D.velocity = Vector2.zero;
                    _isFlippedL2Boss = !_isFlippedL2Boss;
                    _atk4Movement = 3;
                }
            }
        }
        else if (_atk4Movement == 3)
        {
            if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
            {
                FlipBoss();
            }

            if (!_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk4TopRightPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = true;
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk4TopRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                }

                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = false;
                    _l2BossRb2D.velocity = Vector2.zero;
                    if (transform.position.x > 0)
                    {
                        FlipBoss();
                    }
                    else
                    {
                        _isFlippedL2Boss = !_isFlippedL2Boss;
                    }

                    _atk4Movement = 4;
                }
            }
            else if (_isFlippedL2Boss)
            {
                _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk4TopLeftPos.position, transform.position);

                if (_l2BossCurrentDist > _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = true;
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk4TopLeftPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk4MoveSpd, _l2BossRb2D.velocity.y);
                }
                else if (_l2BossCurrentDist <= _l2BossMinDistance)
                {
                    _isDropAtk4Bomb = false;
                    _l2BossRb2D.velocity = Vector2.zero;
                    if (transform.position.x < 0)
                    {
                        FlipBoss();
                    }
                    else
                    {
                        _isFlippedL2Boss = !_isFlippedL2Boss;
                    }
                    _atk4Movement = 4;
                }
            }
        }
        else if (_atk4Movement == 4)
        {
            _atk4CurrentDistY = Mathf.Abs(_atk2BottomRightPos.transform.position.y - transform.position.y);

            if (!_isFlippedL2Boss)
            {
                if (_atk4CurrentDistY > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isDashing", false);
                    _l2BossAnimtr.SetBool("isInAir", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2BottomRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(0, playerDir01.y * _l2BossAtk4MoveSpd);
                }

                else if (_atk4CurrentDistY <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _isInitializedAnim = false;
                    _atk4Movement = 5;
                    AtkCDCheck(_atk4CD);
                }
            }
            else if (_isFlippedL2Boss)
            {
                if (_atk4CurrentDistY > _l2BossMinDistance)
                {
                    _l2BossAnimtr.SetBool("isDashing", false);
                    _l2BossAnimtr.SetBool("isInAir", true);
                    Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk2BottomRightPos.position, transform.position);
                    _l2BossRb2D.velocity = new Vector2(0, playerDir01.y * _l2BossAtk4MoveSpd);
                }
                else if (_atk4CurrentDistY <= _l2BossMinDistance)
                {
                    _l2BossRb2D.velocity = Vector2.zero;
                    _isInitializedAnim = false;
                    _atk4Movement = 5;
                    AtkCDCheck(_atk4CD);
                }
            }
        }
        else if(_atk4Movement == 5)
        {

        }
    }
    private void LimitBreakMovement()
    {
        if (_lbMovement == 0)
        {
            _l2BossAnimtr.SetBool("isIdle", false);
            _l2BossAnimtr.SetBool("isStartLB", true);
        }
        else if (_lbMovement == 1)
        {
            _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_lbTopMidPos.position, transform.position);

            if (_l2BossCurrentDist > _l2BossMinDistance)
            {
                Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_lbTopMidPos.position, transform.position);
                _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk2LRMoveSpd, playerDir01.y * _l2BossAtk2LRMoveSpd);
                //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
            }

            else if (_l2BossCurrentDist <= _l2BossMinDistance)
            {
                _l2BossRb2D.velocity = Vector2.zero;
            }
        }
    }

    private void Atk5Movement()
    {
        _l2BossAnimtr.SetBool("isIdle", false);
        _l2BossAnimtr.SetBool("isStartLB", false);
        _l2BossAnimtr.SetBool("isLBDash", true);
        _l2BossAnimtr.SetInteger("atk5Movement", _atk5Movement);

        if (_atk5Movement == 0)
        {
            Atk5MovementFunc(_atk5MidLeftPos);
        }
        else if (_atk5Movement == 1)
        {
            Atk5MovementFunc(_atk5TopRightPos);
        }
        else if (_atk5Movement == 2)
        {
            Atk5MovementFunc(_atk5BotMidPos);
        }
        else if (_atk5Movement == 3)
        {
            Atk5MovementFunc(_atk5TopLeftPos);
        }
        else if (_atk5Movement == 4)
        {
            Atk5MovementFunc(_atk5MidRightPos);
        }
        else if (_atk5Movement == 5)
        {
            Atk5MovementFunc(_atk5BotLeftPos);
        }
        else if (_atk5Movement == 6)
        {
            Atk5MovementFunc(_atk5TopMidPos);
        }
        else if (_atk5Movement == 7)
        {
            Atk5MovementFunc(_atk5BotRightPos);
        }
        else if (_atk5Movement == 8)
        {
            if (!_atk5IsTeleported)
            {
                transform.position = _atk5TopMidPos.position;
                _atk5IsTeleported = true;
            }

            if (!_atk5GetNinaPos)
            {
                _ninaTempPos = _ninaGO.transform.position;
                _atk5GetNinaPos = true;
            }

            _l2BossCurrentDist = L2BossFunctions.CalculateDistance(_atk5MidPos.position, transform.position);

            if (Mathf.Abs(_l2BossRb2D.velocity.x) < 1)
            {
                if (!_isFlippedL2Boss && transform.position.x < _ninaTempPos.x)
                {
                    FlipBoss();
                    _isFlippedL2Boss = !_isFlippedL2Boss;
                }
                else if (_isFlippedL2Boss && transform.position.x >= _ninaTempPos.x)
                {
                    FlipBoss();
                    _isFlippedL2Boss = !_isFlippedL2Boss;
                }
            }

            if (_l2BossCurrentDist > _l2BossMinDistance)
            {
                Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_atk5MidPos.position, transform.position);
                _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk2LRMoveSpd, playerDir01.y * _l2BossAtk2LRMoveSpd);
                //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
            }

            else if (_l2BossCurrentDist <= _l2BossMinDistance)
            {
                _l2BossRb2D.velocity = Vector2.zero;
                //Debug.Log("Atk5 Stop Flying");
                _isFireAtk5Tornado = true;
                _atk5GetNinaPos = false; 
                _atk5IsTeleported = false;
                _atk5Movement = 9;
            }
        }
        else if (_atk5Movement == 9)
        {

        }
    }
    private void Atk5MovementFunc(Transform startPos)
    {
        if(!_atk5GetNinaPos)
        {
            _ninaTempPos = _ninaGO.transform.position;
            _atk5GetNinaPos = true;
        }

        if (!_atk5IsTeleported)
        {
            transform.position = startPos.position;
            _atk5IsTeleported = true;
        }

        if (!_atk5IsMoving)
        {
            _atk5IsMoving = true;

            Vector3 playerDir01 = L2BossFunctions.NormalizedDir(_ninaTempPos, transform.position);
            _l2BossRb2D.velocity = new Vector2(playerDir01.x * _l2BossAtk5MoveSpd, playerDir01.y * _l2BossAtk5MoveSpd);
            //Debug.Log("Atk5 Flying" + _l1BossRb2D.velocity);
        }

        if (_atk5MovementCDCount < _atk5MovementCD)
        {
            _atk5MovementCDCount += Time.deltaTime;
        }
        else
        {
            _l2BossRb2D.velocity = Vector2.zero;
            //Debug.Log("Atk5 Stop Flying");
            _atk5IsTeleported = false;
            _atk5GetNinaPos = false;
            _atk5IsMoving = false;
            _atk5MovementCDCount = 0;
            _atk5Movement += 1;
        }
    }

    private void Atk6Movement()
    {

    }
    public void Attack1TornadoEnd() //Animation Event for atk1Movement
    {
        _tornadoGO.SetActive(false);
        _isInitializedAnim = false;
        AtkCDCheck(_atk1CD);
        _isFireAtk1Tornado = false;
    }
    public void Attack2ShockwaveEnd()
    {
        _shockwaveGO.SetActive(false);
        _isInitializedAnim = false;
        AtkCDCheck(_atk2CD);
    }
    public void Attack3MeleeUpStart() //Animation Event for atk3Movement 0
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Qinglong's 3.1 slash attack");
        }
    }
    public void Attack3MeleeUpEnd() //Animation Event for atk3Movement 0
    {
        _atk3Movement = 1;
    }
    public void Attack3MeleeDownStart() //Animation Event for atk3Movement 1
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Qinglong's 3.2 slash attack");
        }
    }
    public void Attack3MeleeDownEnd() //Animation Event for atk3Movement 1
    {
        _atk3Movement = 2;
    }

    public void Attack3TornadoEnd() //Animation Event for atk3Movement 3
    {
        _isInitializedAnim = false;
        AtkCDCheck(_atk3CD);
    }
    public void LimitBreakFly() //Animation Event for limit break
    {
        _lbMovement = 1;
    }
    public void LimitBreakEnd() //Animation Event for limit break
    {
        _halfHPLBLaunched = true;
        _isInitializedAnim = false;
        _isDropLBThunder = true;
        _currentAtkNum = 5;

    }
    public void Attack5TornadoEnd() //Animation Event for atk3Movement 3
    {
        _isInitializedAnim = false;
        _atk5TornadoGOs[0].SetActive(false);
        _atk5TornadoGOs[1].SetActive(false);
        _atk5Movement = 10;
        AtkCDCheck(_atk5CD);
    }
    private IEnumerator LBDropThunder(float thunderDelay, float thunderDuration, float thunderCD)
    {
        _isDropLBThunder = false;

        Vector3 tempThunderPos = new(_ninaGO.transform.position.x, _atk5MidPos.position.y, _atk5MidPos.position.z);
        GameObject thunderGO = Instantiate(_thunderPrefab, tempThunderPos, _atk5MidPos.rotation);
        Vector3 tempWarnSignPos = new(thunderGO.transform.position.x, _atk5MidPos.position.y, _atk5MidPos.position.z);
        thunderGO.SetActive(false);
        GameObject tempWarnSignGO = Instantiate(_lbWarnSignPrefab, tempWarnSignPos, _atk5MidPos.rotation);

        yield return new WaitForSeconds(thunderDelay);
        tempWarnSignGO.SetActive(false);
        thunderGO.SetActive(true);

        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Qinglong's limit break thunder VFX");
        }

        yield return new WaitForSeconds(thunderDuration);
        thunderGO.SetActive(false);

        yield return new WaitForSeconds(thunderCD);
        _isDropLBThunder = true;
    }

    //private void StopAtk4DBCoroutine()
    //{
    //    if (_atk4DBCoroutine != null)
    //    {
    //        StopCoroutine(_atk4DBCoroutine);
    //        _atk4DBCoroutine = null;
    //    }
    //}
    private void InitializeAnim()
    {
        if (!_isInitializedAnim)
        {
            _isResetAnim = true;
            ResetBoolParameters();
            _l2BossAnimtr.SetBool("isIdle", true);
            _isInitializedAnim = true;
        }
    }
    private void ResetBoolParameters()
    {
        if (_isResetAnim)
        {
            _isResetAnim = false;

            if (_l2BossAnimtr == null)
            {
                Debug.LogWarning("Animator component not found.");
                return;
            }

            AnimatorControllerParameter[] parameters = _l2BossAnimtr.parameters;

            foreach (AnimatorControllerParameter parameter in parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    _l2BossAnimtr.SetBool(parameter.name, false);
                }
            }

            foreach (AnimatorControllerParameter parameter in parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Int)
                {
                    _l2BossAnimtr.SetInteger(parameter.name, 0);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _currentL2BossHP -= damage;
    }
}
