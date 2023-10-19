using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBossAI : DealDamageCont_E, IFTakeDamage_E
{
    //Week 1 06062023 KS's Note: Random range & Sprite changing & (int currentAction) got prob
    //New
    [Header("Preset")]
    [Tooltip("Enemy2 Attack Status")]
    public GameObject topFireport;
    public GameObject frontFireport;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    [SerializeField] private GameObject _skillPointGO;
    [SerializeField] private Animator _tBossAnimtr;
    [SerializeField] private GameObject _cutSceneGO;

    public Rigidbody2D enemyPjtTop;
    public Rigidbody2D enemyPjtFront;
    public Rigidbody2D spawnedCreeps;
    private Rigidbody2D spawnedCreepsRB2D1;
    private Rigidbody2D spawnedCreepsRB2D2;
    
    public Transform rushTarget;
    public Transform originalPos;
    //private Vector3 originalPos;
    [Header("Attack 4 Preset")]
    [Tooltip("Attack 4 Preset")]
    [SerializeField] private Transform _botLeftPos;
    [SerializeField] private Transform _botRightPos;
    [SerializeField] private Transform _topLeftPos;
    [SerializeField] private Transform _topRightPos;

    [Header("Config")]
    [Tooltip("Enemy2 Statistics")]
    [SerializeField] private bool _isResetAnim; // Originally under Debug, but place here for adjusting it in editor
    public float _maxTBossHP;
    public float _currentTBossHP;
    public float enemy2HPRegenRate;

    //public float topCDCount;
    public float frontFireCD = 30f;
    //public float frontCDCount;
    public float swapSpriteTime = 30f;
    //public float swapTimeCount;
    public float rushTime = 2f;
    public float rushTimeCount = 2f;
    public float rushSpd;
    public float returnSpd;
    public int pjtUpSpeed;
    public int pjtSideSpeed;
    //public bool isFireTop;
    //public bool isFireFront;
    
    private Sprite currentSprite;
    public Sprite[] enemySprite;
    //public bool isSwapSprite;

    [Header("Attack 1 Config")]
    [Tooltip("Attack 1 Config")]
    [SerializeField] private float _atk1CD;
    [SerializeField] private int _atk1PjtNum;
    [SerializeField] private float _atk1TopFireCD = 0.5f;

    [Header("Attack 2 Config")]
    [Tooltip("Attack 2 Config")]
    [SerializeField] private float _atk2CD;
    [SerializeField] private int _atk2PjtNum;
    [SerializeField] private float _atk2FrontFireCD = 0.5f;

    [Header("Attack 3 Config")]
    [Tooltip("Attack 3 Config")]
    [SerializeField] private float _atk3CD;
    
    [Header("Attack 4 Config")]
    [Tooltip("Attack 4 Config")]
    [SerializeField] private float _atk4CD;
    [SerializeField] private float _atk4XForce, _atk4YForce;
     [SerializeField] private float _atk4FlipDelay, _atk4JumpDelay;

    [Header("Attack 5 Config")]
    [Tooltip("Attack 5 Config")]
    [SerializeField] private float _atk5CD;

    [Header("Debug")]
    [Tooltip("TBoss Spawn")]
    [SerializeField] private bool _startAtk;
    [SerializeField] private int _currentAtkNum;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private bool _isDoneAtking;
    [SerializeField] private Rigidbody2D _tBossRb2D;
    [SerializeField] private bool _isInitializedAnim;

    private bool _isSpawning;
    public bool startAction;
    public int currentAction;
    public int lastUsedAction;
    private int pjtDirection;
    [SerializeField] private List<int> _atkArrangement = new() { 1, 2, 3, 4 };

    [Header("Attack 1 Debug")]
    [Tooltip("Attack 1 Debug")]
    [SerializeField] private bool _isFireAtk1TopPjt;
    
    [Header("Attack 2 Debug")]
    [Tooltip("Attack 2 Debug")]
    [SerializeField] private bool _isFireAtk2FrontPjt;

    [Header("Attack 3 Debug")]
    [Tooltip("Attack 3 Debug")]
    [SerializeField] private bool _isSpawnedAtk3Creeps;

    [Header("Attack 4 Debug")]
    [Tooltip("Attack 4 Debug")]
    [SerializeField] private int _atk4Movement;
    [SerializeField] private bool _atk4CanJump;

    // Start is called before the first frame update
    void Start()
    {
        _tBossRb2D = GetComponent<Rigidbody2D>();
        //InitializeVar();
    }

    private void OnEnable()
    {
        _isSpawning = true;
        //InitializeVar();
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
            ////_tBossAnimtr.SetInteger("currentAttack", _currentAtkNum); // Turn back on if have animator
        }

        ////if (_currentTBossHP <= 0)
        ////{
        ////    //Destroy(gameObject);
        ////    ////Instantiate(_skillPointGO, new Vector3(transform.position.x, 3, transform.position.z), _skillPointGO.transform.rotation);
            
        ////    ////Menus.PauseGame(true, 0f);
        ////    ////Menus._otherMenuOn = true;
        ////    Menus.LoadScene(4);
        ////    ////_cutSceneGO.SetActive(true);
        ////    ////gameObject.SetActive(false);
        ////}
    }

    private void Check_SwitchAttack(bool isAttacking)
    {
        if (!isAttacking)
        {
            switch (_currentAtkNum)
            {
                default:
                    
                    if(!_isFireAtk1TopPjt)
                    {
                        StartCoroutine(FireAtk1TopPjt(_atk1PjtNum, _atk1TopFireCD));
                    }

                    _isDoneAtking = true;

                    break;

                case 2:
                    
                    if (!_isFireAtk2FrontPjt)
                    {
                        StartCoroutine(FireAtk2FrontPjt(_atk2PjtNum, _atk2FrontFireCD));
                    }

                    _isDoneAtking = true;

                    break;

                case 3:

                    if (!_isSpawnedAtk3Creeps)
                    {
                        _isSpawnedAtk3Creeps = true;
                        _isDoneAtking = true;

                        spawnedCreepsRB2D1 = Instantiate(spawnedCreeps, spawnPoint1.transform.position, transform.rotation);
                        spawnedCreepsRB2D2 = Instantiate(spawnedCreeps, spawnPoint2.transform.position, transform.rotation);
                        AtkCDCheck(_atk3CD);
                    }


                    break;

                case 4:
                   
                    Atk4Movement();

                    _isDoneAtking = true;

                    break;
                
                case 5:
                    // Go from right to up to left to down to right
                    break;
            }
        }
    }

    private void Atk4Movement()
    {
        if(_atk4Movement == 0)
        {
            if(!_atk4CanJump)
            {
                StartCoroutine(Atk4JumpDelay(-_atk4XForce, _atk4YForce, _atk4FlipDelay, _atk4JumpDelay));
            }
        }
        else if(_atk4Movement == 1)
        {
            if (!_atk4CanJump)
            {
                StartCoroutine(Atk4JumpDelay(_atk4XForce, _atk4YForce, _atk4FlipDelay, _atk4JumpDelay));
            }
        }
        else if (_atk4Movement == 2)
        {
            AtkCDCheck(_atk4CD);
        }
    }

    private void SpawnBoss()
    {
        if (_isSpawning)
        {
            _currentTBossHP += _maxTBossHP / 2 * Time.deltaTime;

            if (_currentTBossHP >= _maxTBossHP)
            {
                _currentTBossHP = _maxTBossHP;
                _currentAtkNum = RandomAttack();
                _startAtk = true;
                _isSpawning = false;
            }
        }
    }

    private int RandomAttack()
    {
        int randomAtkNum = 3;
        int randomIndex;

        //if (_currentL2BossHP > _maxL2BossHP / 2)
        //{
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
        //}
        //else if (_currentL2BossHP <= _maxL2BossHP / 2)
        //{
        //    if (!_halfHPLBLaunched)
        //    {
        //        randomAtkNum = 7;
        //        _atkArrangement = new() { 1, 2, 3, 4, 5 };

        //        return randomAtkNum;
        //    }
        //    else if (_halfHPLBLaunched && _atkArrangement.Count > 0)
        //    {
        //        // Generate a random index to pick a number from the list
        //        randomIndex = Random.Range(0, _atkArrangement.Count);

        //        // Get the random number from the list
        //        randomAtkNum = _atkArrangement[randomIndex];

        //        // Remove the picked number from the list
        //        _atkArrangement.RemoveAt(randomIndex);

        //        ////Debug.Log("Picked number after Limit Break: " + randomAtkNum);

        //        return randomAtkNum;
        //    }
        //    else
        //    {
        //        //Debug.Log("Number list is empty after Limit Break.");
        //        _atkArrangement = new() { 1, 2, 3, 4, 5 };

        //        // Generate a random index to pick a number from the list
        //        randomIndex = Random.Range(0, _atkArrangement.Count);

        //        // Get the random number from the list
        //        randomAtkNum = _atkArrangement[randomIndex];

        //        // Remove the picked number from the list
        //        _atkArrangement.RemoveAt(randomIndex);

        //        ////Debug.Log("Picked number: " + randomAtkNum);

        //        return randomAtkNum;
        //    }
        //}

        //return randomAtkNum;
    }
    private IEnumerator StartAtkCD(float cd)
    {
        ////Debug.Log("Waiting for CD");
        _isAttacking = true;
        _isDoneAtking = false;
        yield return new WaitForSeconds(cd);
        Debug.Log("CD done");

        _currentAtkNum = RandomAttack();
        //Debug.Log("_currentAtkNum = " + _currentAtkNum);
        InitializeVar();
        _isAttacking = false;
    }

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

    private IEnumerator FireAtk1TopPjt(int pjtNum, float cd)
    {
        _isFireAtk1TopPjt = true;
        
        for (int b = 0; b < pjtNum; b++)
        {
            Instantiate(enemyPjtTop, topFireport.transform.position, transform.rotation);
            yield return new WaitForSeconds(cd);
        }
        
        AtkCDCheck(_atk1CD);
        _isFireAtk1TopPjt = false;
    }

    private IEnumerator FireAtk2FrontPjt(int pjtNum, float cd)
    {
        _isFireAtk2FrontPjt = true;

        for (int b = 0; b < pjtNum; b++)
        {
            Instantiate(enemyPjtFront, frontFireport.transform.position, transform.rotation);
            yield return new WaitForSeconds(cd);
        }

        AtkCDCheck(_atk2CD);
        _isFireAtk2FrontPjt = false;
    }

    private IEnumerator Atk4JumpDelay(float xForce, float yForce, float flipDelay, float jumpDelay)
    {
        _tBossRb2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Force);
        _atk4CanJump = true;
        //Debug.Log("Jump");

        yield return new WaitForSeconds(flipDelay);
        FlipBoss();
        //Debug.Log("Flip");

        yield return new WaitForSeconds(jumpDelay);
        _atk4Movement++;
        _atk4CanJump = false;
        //Debug.Log("Can Jump");
    }
    private void InitializeVar()
    {
        _isSpawnedAtk3Creeps = false;
        _atk4Movement = 0;
    }
    private void InitializeAnim()
    {
        if (!_isInitializedAnim)
        {
            _isResetAnim = true;
            ResetBoolParameters();
            ////_tBossAnimtr.SetBool("isIdle", true); // Turn back on if have animator
            _isInitializedAnim = true;
        }
    }
    private void ResetBoolParameters()
    {
        if (_isResetAnim)
        {
            _isResetAnim = false;

            if (_tBossAnimtr == null)
            {
                Debug.LogWarning("Animator component not found.");
                return;
            }

            AnimatorControllerParameter[] parameters = _tBossAnimtr.parameters;

            foreach (AnimatorControllerParameter parameter in parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    _tBossAnimtr.SetBool(parameter.name, false);
                }
            }

            foreach (AnimatorControllerParameter parameter in parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Int)
                {
                    _tBossAnimtr.SetInteger(parameter.name, 0);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _currentTBossHP -= damage;
    }
}
