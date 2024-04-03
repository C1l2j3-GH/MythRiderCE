using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath = "";

    private string _dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this._dataDirPath = dataDirPath;
        this._dataFileName = dataFileName;
    }

    public GameData Load(string profileID)
    {
        if(profileID == null)
        {
            return null;
        }
        //string fullPath = _dataDirPath + "/" + _dataFileName;
        string fullPath = Path.Combine(_dataDirPath, profileID, _dataFileName);
        
        GameData loadedData = null;
        
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data to file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data, string profileID)
    {
        if(profileID == null)
        {
            return;
        }
        //string fullPath = _dataDirPath + "/" + _dataFileName;
        string fullPath = Path.Combine(_dataDirPath, profileID, _dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDict = new Dictionary<string, GameData>();

        //Loop over all directory names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(_dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileID = dirInfo.Name;

            //Defensive Programming - Check if the data file exists
            //If it doesn't, then this folder isn't a profile and should be skipped
            string fullPath = Path.Combine(_dataDirPath, profileID, _dataFileName);
            if(!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + profileID);
                continue;
            }

            GameData profileData = Load(profileID);

            //Defensive programming - Ensure the profile data isn't null,
            //because if it is then something went wrong and we should let ourselves know
            if(profileData != null)
            {
                profileDict.Add(profileID, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. Profile ID: " + profileID);
            }
        }

        return profileDict;
    }

    public string GetMostRecentlyUpdatedProfileID()
    {
        string mostRecentProfileID = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileID = pair.Key;
            GameData gameData = pair.Value;

            if(gameData == null)
            {
                continue;
            }

            if(mostRecentProfileID == null)
            {
                mostRecentProfileID = profileID;
            }
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileID]._lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData._lastUpdated);

                if(newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileID = profileID;
                }
            }
        }
        return mostRecentProfileID;
    }
}
