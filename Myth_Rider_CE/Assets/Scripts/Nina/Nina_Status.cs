using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Nina_Status : MonoBehaviour, IDataPersistence
{
    [Header("Status")]
    [Header("Preset")]
    [Tooltip("Passing GameObjects as reference")]
    //Nina Change Form UI
    //[SerializeField] private Animator _nina_HenshinAnimator;
    //[Tooltip("Nina UI")]
    //[SerializeField] private GameObject _ninaCanvas;
    //[SerializeField] private GameObject _formUI;
    //[SerializeField] private GameObject _ninaHenshinGO;

    //Nina Change Form Buttons
    //[Space(10f)]
    //[SerializeField] private Button[] _formButtons;

    [Space(10f)]
    [SerializeField] private GameObject _tBossUI; //Turn Back On
    public GameObject _tBossGO; //Turn Back On
    [SerializeField] private GameObject _l1bossUI;
    public GameObject _l1bossGO;
    [SerializeField] private GameObject _l2bossUI;
    public GameObject _l2bossGO;
    [SerializeField] private GameObject _hpSliderGO;
    ////[SerializeField] private GameObject _chargedAtkUIGO; // Charge Attack UI
    [SerializeField] private Menus _menus;

    //Level2
    public GameObject _l2LeftBorder;
    public GameObject _l2RightBorder;
    public GameObject _l2DropBorder;

    //Nina Change Form Animators
    //[Space(20f)]
    //[Tooltip("RuntimeAnimatorControllers")]
    //[SerializeField] private RuntimeAnimatorController _ninaBF_AniCtrlr;
    //[SerializeField] private RuntimeAnimatorController _ninaF1_AniCtrlr;
    //[SerializeField] private RuntimeAnimatorController _ninaF2_AniCtrlr;
    //[SerializeField] private RuntimeAnimatorController _ninaF3_AniCtrlr;



    [Header("Config")]
    public float maxPlayerHP;
    public float currentPlayerHP;
    public float hpRegenRate;
    public float maxPlayerMP;
    public float currentPlayerMP;
    public float mpRegenRate;
    [SerializeField] private Color _spriteColor;
    [SerializeField] private List<Vector3> _ninaSpawnPoss;
    [SerializeField] private Transform _ninaSpawnPosTf;

    [Header("Debug")]
    [Space(10f)]
    public bool playerDied;
    public bool enemyDied;
    public static bool isAttacked;
    public static bool isChangingForm = false;
    public static bool _isShowingTBossDlg = false;
    public static bool _isFinishedBossDlg = false;
    public static bool _isTriggeredTBossDlg = false;
    public static int _currentSP; //Skill Point
    public static int _currentFormNum;
    [SerializeField] private Animator _nina_Animator;
    [SerializeField] private SpriteRenderer _ninaSR;
    //public float enemyCurrentHP;

    //Skill Tree
    public bool _canHPRegen;


    //Crack
    [SerializeField] private bool _cracked;
    [SerializeField] private float _tempMaxHP;
    [SerializeField] private float _tempCurrentHP;
    [SerializeField] private float _tempMaxMP;
    [SerializeField] private float _tempCurrentMP;
    
    public static bool _isSpawnedInT1;
    public static bool _isSpawnedInL1;
    public static bool _isSpawnedInL2;
    public static bool _isSpawnedInL3;

    public Nina_BaseForm _currentForm;
    public Nina_Form1 _form1 = new();
    public Nina_Form2 _form2 = new();
    public Nina_Form3 _form3 = new();
    public Nina_Form4 _form4 = new();
    
    public static Nina_Status _n_S_Instance;

    void Awake() // Singeton
    {
        if (_n_S_Instance == null)
        {
            _n_S_Instance = this;
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
        _ninaSR = GetComponent<SpriteRenderer>();
        _nina_Animator = GetComponent<Animator>();
        //Debug.Log(_nina_Animator.runtimeAnimatorController);

        currentPlayerHP = maxPlayerHP;
        currentPlayerMP = maxPlayerMP;

        _currentForm = _form1;
        _currentForm.EnterForm(this);
        _currentFormNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("_isShowingTBossDlg = " + _isShowingTBossDlg);
        //Debug.Log("SkillPoint = " + _currentSP);
        _currentForm.UpdateForm(this);
        SpawnBoss();
        SpawnNina();

        MPRegen();
        if(_canHPRegen)
        { 
        
        HPRegen();
        }

        //Crack
        BossHalfHP();
        Boss1HP();
        HPMP99999();
        SPIncrease();

        if (currentPlayerHP > maxPlayerHP)
        {
            currentPlayerHP = maxPlayerHP;
        }

        #region Change Form Button
        //if (Menus.gameIsPaused == false && isChangingForm == false)
        //{
        //    if (Input.GetKeyDown(KeyCode.L))
        //    {
        //        if(transform.localScale.x < 0)
        //        {
        //            if (_ninaCanvas.transform.localScale.x > 0)
        //            {
        //                _ninaCanvas.transform.localScale = new Vector3(_ninaCanvas.transform.localScale.x * -1, _ninaCanvas.transform.localScale.y, _ninaCanvas.transform.localScale.z);
        //            }
        //        }
        //        else
        //        {
        //            _ninaCanvas.transform.localScale = new Vector3(Mathf.Abs(_ninaCanvas.transform.localScale.x), _ninaCanvas.transform.localScale.y, _ninaCanvas.transform.localScale.z);
        //        }

        //        _spriteColor = new(1, 1, 1, 0);
        //        _ninaSR.color = _spriteColor;
        //        _formUI.SetActive(true);
        //        _ninaHenshinGO.SetActive(true);
        //        Menus.PauseGame(true, 0f);
        //        Menus._otherMenuOn = true;

        //        //Physics.autoSimulation = false; //Test
        //        //StopAllCoroutines();
        //        isChangingForm = true;

        //        switch (_currentFormNum)
        //        {
        //            default:
        //                _nina_HenshinAnimator.Play("Nina Base Form Henshin");
        //                break;
        //            case 2:
        //                _nina_HenshinAnimator.Play("Nina H_A Henshin");
        //                break;
        //            case 3:

        //                break;
        //            case 4:

        //                break;
        //        }
        //    }
        //}

        //////else if (Menus.gameIsPaused == false && isChangingForm == true)
        //else if (isChangingForm == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        _formButtons[0].onClick.Invoke();
        //        _spriteColor = new(1, 1, 1, 1);
        //        _ninaSR.color = _spriteColor;
        //        ////GetComponent<Animator>().runtimeAnimatorController = _ninaBF_AniCtrlr;
        //        ////_nina_Animator = GetComponent<Animator>();
        //        //_nina_Animator.Play("Nina Base Form Henshin");
        //        IFNina_Animation.SetAnimatorInt(_nina_Animator, IFNina_Animation._currentFormNum, _currentFormNum);
        //        _nina_Animator.Play("Nina Base Form Idle");
        //    }

        //    else if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        _formButtons[1].onClick.Invoke();
        //        _spriteColor = new(1, 1, 1, 1);
        //        _ninaSR.color = _spriteColor;
        //        ////GetComponent<Animator>().runtimeAnimatorController = _ninaF1_AniCtrlr;
        //        ////_nina_Animator = GetComponent<Animator>();
        //        //_nina_Animator.Play("Nina H_A Henshin");
        //        IFNina_Animation.SetAnimatorInt(_nina_Animator, IFNina_Animation._currentFormNum, _currentFormNum);
        //        _nina_Animator.Play("H_A Idle");

        //    }

        //    else if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        //Nina_Animation.AnimIsHenshining(false);
        //        _formButtons[2].onClick.Invoke();
        //        _spriteColor.a = 1;
        //        _ninaSR.color = _spriteColor;
        //    }

        //    else if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        //Nina_Animation.AnimIsHenshining(false);
        //        _formButtons[3].onClick.Invoke();
        //        _spriteColor.a = 1;
        //        _ninaSR.color = _spriteColor;
        //    }


        //else if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Escape))
        //{
        //    _formUI.SetActive(false);
        //    _ninaHenshinGO.SetActive(false);
        //    _spriteColor.a = 1;
        //    _ninaSR.color = _spriteColor;

        //    switch (_currentFormNum)
        //    {
        //        default:
        //            _nina_Animator.Play("Nina Base Form Idle");
        //            //Debug.Log("Idle 1 is playing");
        //            break;
        //        case 2:
        //            _nina_Animator.Play("H_A Idle");
        //            //Debug.Log("Idle 2 is playing");
        //            break;
        //        case 3:

        //            break;
        //        case 4:

        //            break;
        //    }

        //    Menus.PauseGame(false, 1f);
        //    Menus._otherMenuOn = false;
        //    ////Time.timeScale = 1f;
        //    isChangingForm = false;


        //}

        //}
        #endregion

        //if(playerDied)
        //{
        //    loseMenuPanel.SetActive(true);
        //    timescale
        //}

        //else if (enemyDied)
        //{
        //    winMenuPanel.SetActive(true);
        //}

        //Debug.Log(currentPlayerHP);
    }

    private void MPRegen()
    {
        if (currentPlayerMP < maxPlayerMP)
        { 
            if (!Nina_Attack.isShooting)
                {
                    currentPlayerMP += mpRegenRate * Time.deltaTime;
                }
        }
    }

    private void HPRegen()
    {
        if (currentPlayerHP < maxPlayerHP)
        {
            if (!isAttacked)
            {
                currentPlayerHP += hpRegenRate * Time.deltaTime;
            }
        }
    }

    private void SpawnNina()
    {
        if(Menus._currentScene.buildIndex == 2 && !_isSpawnedInT1)
        {
            //gameObject.transform.position = _ninaSpawnPoss[0];


            //if (_tBossGO != null && _tBossUI != null) //Turn Back On
            //{
            //    Debug.Log("Disabled Tboss Real:" + _isSpawnedInL1);

            //    EnableTBoss(false);
            //}
            InitializeBossUIs();
            InitializeBosses();
            Nina_Movement.InitializeMovement();
            Nina_Attack.InitializeAttack();

            _ninaSpawnPosTf = GameObject.FindGameObjectWithTag("NinaSpawn").transform;

            gameObject.transform.position = _ninaSpawnPosTf.position;
            //Time.timeScale = 1f;

            _isSpawnedInT1 = true;
        }
        if(Menus._currentScene.buildIndex == 3 && !_isSpawnedInL1)
        {
            //gameObject.transform.position = _ninaSpawnPoss[1];



            //if (_tBossGO != null && _tBossUI != null) //Turn Back On
            //{
            //    Debug.Log("Disabled Tboss Real:" + _isSpawnedInL1);

            //    EnableTBoss(false);
            //}
            InitializeBossUIs();
            _hpSliderGO.SetActive(false);
            InitializeBosses();
            Nina_Movement.InitializeMovement();
            Nina_Attack.InitializeAttack();
            //Time.timeScale = 1f;
            _ninaSpawnPosTf = GameObject.FindGameObjectWithTag("NinaSpawn").transform;

            gameObject.transform.position = _ninaSpawnPosTf.position;

            _isSpawnedInL1 = true;
        }
        if (Menus._currentScene.buildIndex == 4 && !_isSpawnedInL2)
        {
            //gameObject.transform.position = _ninaSpawnPoss[2];
            InitializeBossUIs();
            _hpSliderGO.SetActive(false);
            InitializeBosses();
            Nina_Movement.InitializeMovement();
            Nina_Attack.InitializeAttack();

            _ninaSpawnPosTf = GameObject.FindGameObjectWithTag("NinaSpawn").transform;

            gameObject.transform.position = _ninaSpawnPosTf.position;

            _isSpawnedInL2 = true;
        }
        if (Menus._currentScene.buildIndex == 5 && !_isSpawnedInL3)
        {
            InitializeBosses();
            InitializeBossUIs();
            _hpSliderGO.SetActive(false);
            Nina_Movement.InitializeMovement();
            Nina_Attack.InitializeAttack();

            //gameObject.transform.position = _ninaSpawnPoss[3];

            _ninaSpawnPosTf = GameObject.FindGameObjectWithTag("NinaSpawn").transform;

            gameObject.transform.position = _ninaSpawnPosTf.position;

            _isSpawnedInL3 = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DealDamageOnce_E>() != null)
        {
            if (!Nina_Effects._isKnocking)
            {
                currentPlayerHP -= collision.GetComponent<DealDamageOnce_E>()._onceDamage;
                isAttacked = true;
            }
        }
        #region Old Compare Tags
        //if (collision.gameObject.CompareTag("EnemyPjt"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("EnemyPjt").GetComponent<EnemyProjectile>().pjtDamage;
        //    //Nina_Movement._animIsTakeDamage = true;
        //    //Nina_Animation.AnimIsTakeDamage(true);
        //}

        //if (collision.gameObject.CompareTag("EnemyPjtEFX"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("EnemyPjtEFX").GetComponent<EnemyPjtTopEFX>().pjtEFXDamage;
        //    isAttacked = true;

        //}

        //if (collision.gameObject.CompareTag("EnemyPjtFront"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("EnemyPjtFront").GetComponent<EnemyPjtFront>().pjtDamage;
        //    isAttacked = true;

        //}

        //if (collision.gameObject.CompareTag("EnemyDroppedPjt"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("EnemyDroppedPjt").GetComponent<EnemyDroppedPjt>().pjtDamage;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossDrone"))
        //{
        //    currentPlayerHP -= collision.gameObject.GetComponent<L1Boss_Atk4_Drone>()._droneDamage;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossDL"))
        //{
        //    currentPlayerHP -= collision.GetComponent<L1Boss_Atk4_Drone_Laser>().pjtDamage;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossAtk6Grenade"))
        //{
        //    currentPlayerHP -= collision.gameObject.GetComponent<L1Boss_Atk6_Grenade>()._pjtDamage;
        //    isAttacked = true;
        //}
        #endregion

        if (collision.gameObject.CompareTag("SkillPoint"))
        {
            _currentSP += collision.gameObject.GetComponent<SkillPoint>()._skillPoint;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Level2Trigger"))
        {
            //SceneManager.LoadScene(2);
            _menus.LoadScene(3);
            //Time.timeScale = 10f;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        #region Old Compare Tags
        //if (collision.gameObject.CompareTag("Enemy1"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("Enemy1").GetComponent<Enemy1_AI>().enemy1Damage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("Enemy2"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("Enemy2").GetComponent<TBossAI>().enemy2Damage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("SpawnedCreep"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("SpawnedCreep").GetComponent<SpawnedCreeps>().spawnedCreepDamage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossLaser"))
        //{
        //    currentPlayerHP -= collision.gameObject.GetComponent<L1Boss_Atk1_Laser>().pjtDamage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossGrenade"))
        //{
        //    currentPlayerHP -= collision.gameObject.GetComponent<L1Boss_Atk2_Grenade>()._pjtDamage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossEB"))
        //{
        //    currentPlayerHP -= collision.gameObject.GetComponent<L1Boss_Atk3_EnergyBall>()._pjtDamage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossFW")|| collision.gameObject.CompareTag("L1BossFWX"))
        //{
        //    currentPlayerHP -= GameObject.FindGameObjectWithTag("L1BossFW").GetComponent<L1Boss_Atk3_FireWall>()._pjtDamage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossAtk5Laser"))
        //{
        //    currentPlayerHP -= collision.gameObject.GetComponent<L1Boss_Atk5_Laser>()._pjtDamage * Time.deltaTime;
        //    isAttacked = true;
        //}

        //if (collision.gameObject.CompareTag("L1BossAtk6Laser"))
        //{
        //    currentPlayerHP -= collision.gameObject.GetComponent<L1Boss_Atk6_Laser>()._pjtDamage * Time.deltaTime;
        //    isAttacked = true;
        //}
        #endregion
        if(collision.GetComponent<DealDamageCont_E>() != null)
        {
            if (!Nina_Effects._isKnocking)
            {
                //currentPlayerHP -= collision.GetComponent<DealDamageCont_E>()._contDamage * Time.deltaTime;
                currentPlayerHP -= collision.GetComponent<DealDamageCont_E>()._contDamage;
                isAttacked = true;
            }
        }

        if (collision.gameObject.CompareTag("TBossTr"))
        {
            if (Nina_Movement.isGrounded)
            {
                _isShowingTBossDlg = true;
                _isTriggeredTBossDlg = true;
                collision.gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        #region Old Compare Tags

        //if (collision.gameObject.CompareTag("Enemy1") 
        //    || collision.gameObject.CompareTag("Enemy2") 
        //    || collision.gameObject.CompareTag("EnemyPjt") 
        //    || collision.gameObject.CompareTag("EnemyPjtEFX")
        //    || collision.gameObject.CompareTag("SpawnedCreep")
        //    || collision.gameObject.CompareTag("L1BossLaser"))
        //{
        //    isAttacked = false;
        //}

        //if (collision.gameObject.CompareTag("TBossTr"))
        //{
        //    _isShowingTBossDlg = false;
        //    Debug.Log("_isShowingTBossDlg = " + _isShowingTBossDlg);
        //}
        #endregion

        if(collision.GetComponent<DealDamageOnce_E>() != null || collision.GetComponent<DealDamageCont_E>() != null)
        {
            isAttacked = false;
        }
    }

    public void SwitchForm(Nina_BaseForm form)
    {
        _currentForm = form;
        form.EnterForm(this);
    }

    //private void LoseGame()
    //{
    //    if (currentplayerHP <= 0)
    //    {
    //        playerDied = true;
    //    }
    //}

    //private void WinGame()
    //{
    //    if (enemyCurrentHP <= 0)
    //    {
    //        enemyDied = true;
    //    }
    //}

    #region Change Form Button Calls
    //public void Form1Button()
    //{
    //    SwitchForm(_form1);
    //    _currentFormNum = 1;
    //    Menus.PauseGame(false, 1f);
    //    Menus._otherMenuOn = false;
    //    ////Time.timeScale = 1f;
    //    isChangingForm = false;
    //}

    //public void Form2Button()
    //{
    //    SwitchForm(_form2);
    //    _currentFormNum = 2;
    //    Menus.PauseGame(false, 1f);
    //    Menus._otherMenuOn = false;
    //    ////Time.timeScale = 1f;
    //    isChangingForm = false;
    //}

    //public void Form3Button()
    //{
    //    SwitchForm(_form3);
    //    _currentFormNum = 3;
    //    Menus.PauseGame(false, 1f);
    //    Menus._otherMenuOn = false;
    //    ////Time.timeScale = 1f;
    //    isChangingForm = false;
    //}

    //public void Form4Button()
    //{
    //    SwitchForm(_form4);
    //    _currentFormNum = 4;
    //    Menus.PauseGame(false, 1f);
    //    Menus._otherMenuOn = false;
    //    ////Time.timeScale = 1f;
    //    isChangingForm = false;
    //}
    #endregion

    private void SpawnBoss()
    {
        if (_isFinishedBossDlg)
        {
            _isFinishedBossDlg = false;
            EnableTBoss(true); //Turn Back On
            EnableL1Boss(true);
            EnableL2Boss(true);
            //_tBossGO = null;
        }
    }

    public void EnableTBoss(bool bossEnabled) //Turn Back On
    {
        if (_tBossGO != null)
        {
            _tBossUI.SetActive(bossEnabled);
            _tBossGO.SetActive(bossEnabled);
            _hpSliderGO.SetActive(bossEnabled);
        }
    }

    private void EnableL1Boss(bool bossEnabled)
    {
        if (_l1bossGO != null)
        {
            _l1bossUI.SetActive(bossEnabled);
            _l1bossGO.SetActive(bossEnabled);
            _hpSliderGO.SetActive(bossEnabled);
            ////_chargedAtkUIGO.SetActive(bossEnabled); // Charge Attack UI
        }
    }

    private void EnableL2Boss(bool bossEnabled)
    {
        if (_l2bossGO != null)
        {
            _l2bossUI.SetActive(bossEnabled);
            _l2bossGO.SetActive(bossEnabled);
            _hpSliderGO.SetActive(bossEnabled);
            _l2DropBorder.SetActive(bossEnabled);
            _l2LeftBorder.SetActive(bossEnabled);
            _l2RightBorder.SetActive(bossEnabled);
            ////_chargedAtkUIGO.SetActive(bossEnabled); // Charge Attack UI
        }
    }

    private void InitializeBosses()
    {
        EnableTBoss(false);
        EnableL1Boss(false);
        EnableL2Boss(false);
    }
    private void InitializeBossUIs()
    {
        _tBossUI.SetActive(false);
        _l1bossUI.SetActive(false);
        _l2bossUI.SetActive(false);
    }

    #region Save & Load
    // Save & Load Data
    public void LoadData(GameData data)
    {
        _currentSP = data._sdCurrentSP;
    }

    public void SaveData(GameData data)
    {
        data._sdCurrentSP = _currentSP;
    }
    #endregion

    #region Crack
    private void BossHalfHP()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_tBossGO != null)
            {
                float tBossHP = _tBossGO.GetComponent<TBossAI>()._maxTBossHP;
                _tBossGO.GetComponent<TBossAI>()._currentTBossHP = tBossHP / 2 - 1;
            }
            if (_l1bossGO != null)
            {
                float l1bossHP = _l1bossGO.GetComponent<L1BossAI>()._maxL1BossHP;
                _l1bossGO.GetComponent<L1BossAI>()._currentL1BossHP = l1bossHP / 2 - 1;
            }
            if (_l2bossGO != null)
            {
                float l2bossHP = _l2bossGO.GetComponent<L2BossAI>()._maxL2BossHP;
                _l2bossGO.GetComponent<L2BossAI>()._currentL2BossHP = l2bossHP / 2 - 1;
            }
        }
    }

    private void Boss1HP()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (_tBossGO != null)
            {
                _tBossGO.GetComponent<TBossAI>()._currentTBossHP = 1f;
            }
            if (_l1bossGO != null)
            {
                _l1bossGO.GetComponent<L1BossAI>()._currentL1BossHP = 1f;
            }
            if (_l2bossGO != null)
            {
                _l2bossGO.GetComponent<L2BossAI>()._currentL2BossHP = 1f;
            }
        }
    }

    private void HPMP99999()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            if (!_cracked)
            {
                _cracked = true;

                _tempMaxHP = maxPlayerHP;
                _tempCurrentHP = currentPlayerHP;
                _tempMaxMP = maxPlayerMP;
                _tempCurrentMP = currentPlayerMP;

                maxPlayerMP = 99999f;
                currentPlayerMP = 99999f;
                maxPlayerHP = 99999f;
                currentPlayerHP = 99999f;
            }
            else
            {
                _cracked = false;
                maxPlayerHP = _tempMaxHP;
                currentPlayerHP = _tempCurrentHP;
                maxPlayerMP = _tempMaxMP;
                currentPlayerMP = _tempCurrentMP;
            }
        }
    }

    private void SPIncrease()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            _currentSP += 5;
        }
    }
    #endregion
}
