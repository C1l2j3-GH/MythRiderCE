using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Status")]
    [Header("Preset")]
    [Tooltip("Passing GameObjects as reference")]
    [SerializeField] private Nina_Status _ninaStatus;
    [SerializeField] private Nina_Movement _ninaMovement;
    [SerializeField] private Nina_Attack _ninaAttack;
    [SerializeField] private TBossAI _tBossAI;
    [SerializeField] private L1BossAI _l1BossAI;
    [SerializeField] private L2BossAI _l2BossAI;

    [Space(10f)]
    [SerializeField] private Image _ninaHPBarFill;
    public Slider playerHPBar;
    public Slider playerMPBar;
    public Slider enemy2HPBar;
    public Slider _l1BossHPBar;
    public Slider _l2BossHPBar;



    //[Header("Config")]


    //[Header("Debug")]
    //[Space(10f)]

    //public TextMeshProUGUI playerHPText;

    //public TextMeshProUGUI playerMPText;

    //public Slider enemy1HPBar;

    ////public Image[] playerHPBarFill;
    public static HUD _hud_Instance;

    void Awake() // Singeton
    {
        if (_hud_Instance == null)
        {
            _hud_Instance = this;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        SceneCheck();
        //Week 1 06062023 KS's Note: Need to fix the max value of the slider following Nina's value?

        //playerHPText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().currentPlayerHP.ToString();
        //playerHPBar.value = GameObject.FindGameObjectWithTag("Player").GetComponent<Nina_Status>().currentPlayerHP;
        if (playerHPBar != null && playerMPBar != null && _ninaStatus != null)
        {
            playerHPBar.maxValue = _ninaStatus.maxPlayerHP;
            playerHPBar.value = _ninaStatus.currentPlayerHP;
            //playerMPText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>().currentPlayerMP.ToString();
            playerMPBar.maxValue = _ninaStatus.maxPlayerMP;
            playerMPBar.value = _ninaStatus.currentPlayerMP;
        }
        //enemy1HPBar.value = GameObject.FindGameObjectWithTag("Enemy1").GetComponent<Enemy1_AI>().currentEnemy1HP;
        
        if (_tBossAI != null && enemy2HPBar != null)
        {
            enemy2HPBar.value = _tBossAI._currentTBossHP;
        }

        if (_l1BossAI != null && _l1BossHPBar != null)
        {
            _l1BossHPBar.value = _l1BossAI._currentL1BossHP;
        }

        if (_l2BossAI != null && _l2BossHPBar != null)
        {
            _l2BossHPBar.value = _l2BossAI._currentL2BossHP;
        }

        SwitchHPSliderFill();
    }

    public void SwitchHPSliderFill()
    {
        if (playerHPBar.value <= _ninaStatus.maxPlayerHP && playerHPBar.value >= _ninaStatus.maxPlayerHP * 65/100)
        {
            ////playerHPBarFill[0].enabled = true;
            _ninaHPBarFill.color = Color.green;
        }

        else if (playerHPBar.value < _ninaStatus.maxPlayerHP * 65/100 && playerHPBar.value >= _ninaStatus.maxPlayerHP * 30 / 100)
        {
            ////playerHPBarFill[1].enabled = true;
            _ninaHPBarFill.color = Color.yellow;

        }

        else if (playerHPBar.value < _ninaStatus.maxPlayerHP * 30 / 100 && playerHPBar.value >= 0)
        {
            ////playerHPBarFill[2].enabled = true;
            _ninaHPBarFill.color = Color.red;

        }
    }

    private void SceneCheck()
    {
        if (Menus._currentScene.buildIndex == 3 && GameObject.FindGameObjectWithTag("Enemy2").GetComponent<TBossAI>() != null)
        {
            _tBossAI = GameObject.FindGameObjectWithTag("Enemy2").GetComponent<TBossAI>();
        }
        if(Menus._currentScene.buildIndex == 4 && GameObject.FindGameObjectWithTag("L1Boss").GetComponent<L1BossAI>() != null)
        {
            _l1BossAI = GameObject.FindGameObjectWithTag("L1Boss").GetComponent<L1BossAI>();
        }
        if (Menus._currentScene.buildIndex == 5 && GameObject.FindGameObjectWithTag("L2Boss").GetComponent<L2BossAI>() != null)
        {
            _l2BossAI = GameObject.FindGameObjectWithTag("L2Boss").GetComponent<L2BossAI>();
        }
    }
}
