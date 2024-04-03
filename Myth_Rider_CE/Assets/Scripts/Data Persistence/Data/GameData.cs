using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long _lastUpdated;
    ////sd = saved data
    public int _sdCurrentSP;
    public Vector3 _ninaPos;
    public string _mapName;
    ////public Dictionary<string, Vector3> _ninaPos;

    // The values defined in this constructor will be the default values
    // the game starts with when there's no data to load 
    // (Initialize all game data when new GameData class is created)
    public GameData()
    {
        this._sdCurrentSP = 0;
        _ninaPos = new Vector3(-14.75f, -3.16f, 0f);
        _mapName = "Tutorial 1";
        ////_ninaPos = new Dictionary<string, Vector3>();
    }


}
