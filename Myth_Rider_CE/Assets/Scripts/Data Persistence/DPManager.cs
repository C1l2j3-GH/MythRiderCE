using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//// Data Persistence Manager

public class DPManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;

    private GameData _gameData;
    public static DPManager _instance { get; private set; }
    private List<IDataPersistence> _dpObjects;
    private FileDataHandler _dataHandler;

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
    }

    private void Start()
    {
        //Save to default Path set by Unity
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        this._dpObjects = FindAllDPObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        this._gameData = _dataHandler.Load();
        // if no data can be loaded, don't continue
        if (this._gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            NewGame();
        }

        foreach (IDataPersistence dpObject in _dpObjects)
        {
            dpObject.LoadData(_gameData);
        }

        Debug.Log("Loaded Current Skill Point = " + _gameData._sdCurrentSP);
    }

    public void SaveGame()
    {
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

        _dataHandler.Save(_gameData);
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
}
