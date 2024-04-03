using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
//// Data Persistence Manager

public class DPManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool _disableDP = false;
    [SerializeField] private bool _initializeDataIfNull = false;
    [SerializeField] private bool _overrideSelectedProfileID = false;
    [SerializeField] private string _testSelectedProfileID = "test";

    [Header("File Storage Config")]
    [SerializeField] private string _fileName;

    private GameData _gameData;
    public static DPManager _instance { get; private set; }
    private List<IDataPersistence> _dpObjects;
    private FileDataHandler _dataHandler;

    private string _selectedProfileID = "";
    private void Awake()
    {
        // Singleton instance
        if (_instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(_disableDP)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }

        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);

        this._selectedProfileID = _dataHandler.GetMostRecentlyUpdatedProfileID();

        if(_overrideSelectedProfileID)
        {
            this._selectedProfileID = _testSelectedProfileID;
            Debug.LogWarning("Overrode selected profile ID with test ID: " + _testSelectedProfileID);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded Called");
        //Save to default Path set by Unity
        this._dpObjects = FindAllDPObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        //Debug.Log("OnSceneUnloaded Called");
        SaveGame();
    }
    private void Start()
    {

    }

    public void ChangeSelectedProfileID(string newProfileID)
    {
        this._selectedProfileID = newProfileID;
        LoadGame();
    }
    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        if(_disableDP)
        {
            return;
        }

        this._gameData = _dataHandler.Load(_selectedProfileID);

        if(this._gameData == null && _initializeDataIfNull)
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (this._gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        foreach (IDataPersistence dpObject in _dpObjects)
        {
            dpObject.LoadData(_gameData);
        }

        Debug.Log("Loaded Current Skill Point = " + _gameData._sdCurrentSP);
    }

    public void SaveGame()
    {
        if (_disableDP)
        {
            return;
        }
        // if we don't have any data to save, log a warning here
        if (this._gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        foreach (IDataPersistence dpObject in _dpObjects)
        {
            dpObject.SaveData(_gameData);
        }

        Debug.Log("Saved Current Skill Point = " + _gameData._sdCurrentSP);
        Debug.Log("File saved in = " + Application.persistentDataPath);

        _gameData._lastUpdated = System.DateTime.Now.ToBinary();

        _dataHandler.Save(_gameData, _selectedProfileID);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDPObjects()
    {
        IEnumerable<IDataPersistence> dpObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dpObjects);
    }

    public bool HasGameData()
    {
        return _gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _dataHandler.LoadAllProfiles();
    }
}
