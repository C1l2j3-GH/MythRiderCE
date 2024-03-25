using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    ////sd = saved data
    public int _sdCurrentSP;
    public Vector3 _ninaPos;

    // The values defined in this constructor will be the default values
    // the game starts with when there's no data to load 
    // (Initialize all game data when new GameData class is created)
    public GameData()
    {
        this._sdCurrentSP = 0;
    }
}
