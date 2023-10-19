using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menus : MonoBehaviour
{
    [Header("Preset")]
    [Header("Menus GOs")]
    public GameObject loseMenuPanel;
    public GameObject winMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject _warningUI;
    public GameObject _cutSceneGO;

    public static bool gameIsPaused = false;
    public static bool _otherMenuOn;
    public static bool _stMenuOn;
    [SerializeField] private bool onPauseMenu;

    public AudioManager audiomanager; //Audiomanager
    public AudioSource currentAudioSource; //Audio manager, by khai wen

    [SerializeField] private Nina_Status _nina_Status;


    [Header("First Selected Options")]
    [SerializeField] private GameObject _pauseMenuFSO;
    [SerializeField] private GameObject _optionsMenuFSO;
    [SerializeField] private GameObject _winMenuFSO;
    [SerializeField] private GameObject _loseMenuFSO;
    [SerializeField] private GameObject _sT1MenuFSO;
    [SerializeField] private GameObject _sT2MenuFSO;
    [SerializeField] private GameObject _sT3MenuFSO;

    [SerializeField] private bool _isDefeatTBoss;

    public static bool _isOnTBossEndDialog;
    public static bool _isShowingTBossEndDialog;

    public static Menus _menusInstance;

    public static bool _isStartL1;
    public static bool _isStartL2;

    public int _menuID;

    [Header("Debug")]
    [Tooltip("Debug")]
    public static Scene _currentScene;
    void Awake() // Singeton
    {
        if(_menusInstance == null)
        {
            _menusInstance = this;
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
        EventSystem.current.SetSelectedGameObject(null);
        
        if (AudioManager.amInstance != null)
        {
            currentAudioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        _currentScene = SceneManager.GetActiveScene();

        LoseGame(); //Off when testing, need to turn back on
        
        if (_currentScene.buildIndex == 4 && !_isStartL1)
        {
            _isStartL1 = true;
            _warningUI.SetActive(true);

        }
        if (_currentScene.buildIndex == 5 && !_isStartL2)
        {
            _isStartL2 = true;
            _warningUI.SetActive(true);
            //WinGame(); //Off when testing, need to turn back on
        }

        PauseByEscape();

        OnTBossDefeat();
        //Pause();
        ////if(gameIsPaused)
        ////{
        ////    Time.timeScale = 0f;
        ////}
        ////else if(!gameIsPaused)
        ////{
        ////    Time.timeScale = 1f;
        ////}
        #region Crack
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadScene(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadScene(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            LoadScene(6);
        }
        #endregion
    }

    #region Load Scenes Functions
    public void LoadMainMenu()
    {
        _nina_Status.currentPlayerHP = _nina_Status.maxPlayerHP;
        _nina_Status.currentPlayerMP = _nina_Status.maxPlayerMP;
        Nina_Status._isSpawnedInT1 = false;
        Nina_Status._isSpawnedInL1 = false;
        Nina_Status._isSpawnedInL2 = false;
        Nina_Status._isSpawnedInL3 = false;
        _isDefeatTBoss = false;
        _isStartL1 = false;
        _isStartL2 = false;

        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.StopAllBGM();
            AudioManager.amInstance.StopAllSF();
        }

        PauseGame(false, 1f);
        _otherMenuOn = false;
        pauseMenuPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void LoadTutorial1() //by khai wen to put main menu call tutorial level
    {
        PauseGame(false, 1f);
        _otherMenuOn = false;
        SceneManager.LoadScene(2);
    }

    public void LoadLevel1()
    {
        PauseGame(false, 1f);
        _otherMenuOn = false;
        SceneManager.LoadScene(4);
    }

    public void LoadLevel2()
    {
        PauseGame(false, 1f);
        _otherMenuOn = false;
        SceneManager.LoadScene(5);
    }
    public void QuitGame()
    {
        PauseGame(false, 1f);
        Application.Quit();
        //Debug.Log("Quit");
    }

    public void LoadScene(int index)
    {
        _nina_Status.currentPlayerHP = _nina_Status.maxPlayerHP;
        _nina_Status.currentPlayerMP = _nina_Status.maxPlayerMP;
        Nina_Status._isSpawnedInT1 = false;
        Nina_Status._isSpawnedInL1 = false;
        Nina_Status._isSpawnedInL2 = false;
        Nina_Status._isSpawnedInL3 = false;
        _isDefeatTBoss = false;
        _isStartL1 = false;
        _isStartL2 = false;

        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.StopAllBGM();
            AudioManager.amInstance.StopAllSF();
        }

        PauseGame(false, 1f);
        _otherMenuOn = false;
        SceneManager.LoadScene(index);
    }

    public void RestartLevel()
    {
        _nina_Status.currentPlayerHP = _nina_Status.maxPlayerHP;
        _nina_Status.currentPlayerMP = _nina_Status.maxPlayerMP;
        Nina_Status._isSpawnedInT1 = false;
        Nina_Status._isSpawnedInL1 = false;
        Nina_Status._isSpawnedInL2 = false;
        Nina_Status._isSpawnedInL3 = false;
        _isDefeatTBoss = false;
        _isStartL1 = false;
        _isStartL2 = false;

        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.StopAllBGM();
            AudioManager.amInstance.StopAllSF();
        }

        PauseGame(false, 1f);
        _otherMenuOn = false; 
        loseMenuPanel.SetActive(false);
        SceneManager.LoadScene(_currentScene.buildIndex);
    }
    #endregion
    public static void PauseGame(bool isPaused, float timeScale)
    {
        gameIsPaused = isPaused;
        Time.timeScale = timeScale;
    }
    public void PauseByEscape()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Pause_N_Resume();
        //}

        if(UI_InputManager.instance._pauseMenuInput)
        {
            Pause_N_Resume();
        }
    }

    public void Pause_N_Resume()
    {
            if (onPauseMenu)
            {
                PauseGame(false, 1f);
                ////gameIsPaused = false;
                pauseMenuPanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                onPauseMenu = false;
            if (currentAudioSource != null)
            {
                currentAudioSource.UnPause(); //audio manager, by khai wen
            }
                ////Time.timeScale = 1f;
            }

            ////else if (!gameIsPaused)
            else if (!onPauseMenu && !_otherMenuOn)
            {
                PauseGame(true, 0f);
                ////gameIsPaused = true;
                pauseMenuPanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_pauseMenuFSO);
                onPauseMenu = true;
            if (currentAudioSource != null)
            {
                currentAudioSource.Pause(); //audio manager, by khai wen
            }
                ////Time.timeScale = 0f;
            }
    }

    public void OnSkillTree()
    {
        _stMenuOn = true;
        EventSystem.current.SetSelectedGameObject(_sT1MenuFSO);
    }

    public void OffSkillTree()
    {
        _stMenuOn = false;
        EventSystem.current.SetSelectedGameObject(_pauseMenuFSO);
    }

    public void OnOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(_optionsMenuFSO);
    }

    public void OffOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(_pauseMenuFSO);
    }
    public void LoseGame()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Nina_Status>().currentPlayerHP <= 0)
        {
            PauseGame(true, 0f);
            _otherMenuOn = true;
            ////gameIsPaused = true;
            ////Time.timeScale = 0f;
            loseMenuPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_loseMenuFSO);
        }
    }

    public void OnTBossDefeat()
    {
        //GameObject tBoss = GameObject.FindGameObjectWithTag("Enemy2");
        GameObject tBoss = _nina_Status._tBossGO;

        if (tBoss != null && tBoss.activeSelf == true)
        {
            if (tBoss.GetComponent<TBossAI>()._currentTBossHP <= 0)
            {
                if (!_isDefeatTBoss)
                {
                    _isDefeatTBoss = true;
                    Nina_Status._currentSP += 5;
                    _isOnTBossEndDialog = true;
                    _isShowingTBossEndDialog = true;
                }
            }
        }
    }
    public void WinGame()
    {
        //GameObject.FindGameObjectWithTag("Enemy1").GetComponent<Enemy1_AI>().currentEnemy1HP <= 0
        //    ||
        GameObject level2Boss = GameObject.FindGameObjectWithTag("L2Boss");

        if (level2Boss != null)
        {
            if (level2Boss.GetComponent<L2BossAI>()._currentL2BossHP <= 0)
            {
                PauseGame(true, 0f);
                _otherMenuOn = true;
                ////gameIsPaused = true;
                ////Time.timeScale = 0f;
                winMenuPanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_winMenuFSO);
            }
        }
    }

    //public void TimeUp()
    //{
    //    if (GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUI>().remainingTime <= 0)
    //    {
    //        Time.timeScale = 0f;
    //        gameOverMenuUI.SetActive(true);
    //    }
    //}



}
