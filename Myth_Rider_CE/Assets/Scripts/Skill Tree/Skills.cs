using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skills : MonoBehaviour
{
    [Header("Preset")]
    public GameObject _skillCover;
    [SerializeField] private SkillsSO _skillsSO;
    //public TextMeshProUGUI _currentSPText;
    public TextMeshProUGUI _spToUnlockText;

    [Header("Config")]
    [Space(10f)]
    public string _skillName;
    public string _skillDesc;
    public int _spToUnlock;
    public int _skillID;

    [Header("Debug")]
    public bool _isUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        _skillName = _skillsSO._skillName;
        _skillDesc = _skillsSO._skillDesc;
        _spToUnlock = _skillsSO._spToUnlock;
        _skillID = _skillsSO._skillID;
        //_spToUnlockText.text = Nina_Status._currentSP.ToString() + " / " + _spToUnlock.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //_currentSPText.text = Nina_Status._currentSP.ToString();
        _spToUnlockText.text = Nina_Status._currentSP.ToString() + "/" + _spToUnlock.ToString();

    }

}
