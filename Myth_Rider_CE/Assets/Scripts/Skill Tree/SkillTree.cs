using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillTree : MonoBehaviour
{
    [Header("Preset")]
    public List<GameObject> _skillGOList;
    public TextMeshProUGUI _skillNameText;
    public TextMeshProUGUI _skillDescText;
    public TextMeshProUGUI _spToUnlockText;
    
    [SerializeField] private Nina_Status _nina_Status;
    [SerializeField] private Nina_Attack _nina_Attack;
    //[SerializeField] private Slider _mpSlider;
    [SerializeField] private Rigidbody2D _currentRangedAtk;

    [Header("Config")]

    //public int _f1SkillID;
    //public int _f2SkillID;
    //public int _f3SkillID;

    [Header("Debug")]
    [SerializeField] private int _spToUnlock;
    public int _skillID;
    public Skills _skills;
    [SerializeField] private GameObject _selectedButton;

    // Start is called before the first frame update
    void Start()
    {
        _skillID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Menus._stMenuOn)
        {
            NavSkillButton();
        }
    }


    public void NavSkillButton()
    {
        //GetComponent when Navigated
        _selectedButton = EventSystem.current.currentSelectedGameObject;
        if (_selectedButton.GetComponent<Skills>() != null)
        {
            _skills = _selectedButton.GetComponent<Skills>();
            if (_skills != null)
            {
                _skillNameText.text = _skills._skillName;
                _skillDescText.text = _skills._skillDesc;
                _skillID = _skills._skillID;
                _spToUnlock = _skills._spToUnlock;
            }
        }
        //PressSkillButton();
    }
    public void PressSkillButton()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            //if (!_skills._isUnlocked)
            //{
            //    _skills._isUnlocked = true;
            //    _skills._skillCover.SetActive(false);
            //}
            _selectedButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void OnPressSkillButton()
    {
        //_skillID = GetComponent<Skills>()._skillID;
        
        if (_skillGOList[_skillID] != null)
        {
            switch (_skillID)
            {
                case 0:
                    if (Nina_Status._currentSP >= _spToUnlock)
                    {
                        //Debug.Log("0 =" + Nina_Status._currentSP);
                        //Debug.Log("0 =" + _spToUnlock);
                        OnSkillBF_01();
                        Nina_Status._currentSP -= _spToUnlock;
                    }
                    break;

                case 1:
                    if (Nina_Status._currentSP >= _spToUnlock)
                    {
                        //Debug.Log("1 =" + Nina_Status._currentSP);
                        //Debug.Log("1 =" + _spToUnlock);
                        OnSkillBF_02();
                        Nina_Status._currentSP -= _spToUnlock;
                    }
                    break;

                case 2:
                    if (Nina_Status._currentSP >= _spToUnlock)
                    {
                        //Debug.Log("2 =" + Nina_Status._currentSP);
                        //Debug.Log("2 =" + _spToUnlock);
                        OnSkillBF_03();
                        Nina_Status._currentSP -= _spToUnlock;
                    }
                    break;
            }
        }
    }

    private void OnSkillBF_01()
    {
        //_mpSlider.maxValue += 10000f;
        //_mpSlider.value += 10000f;
        _skills._skillCover.SetActive(false);
        //_nina_Status.maxPlayerMP += 10000f;
        //_nina_Status.currentPlayerMP += 10000f;
        _nina_Attack._chargeSpeed += 30f;
        _skillGOList[_skillID] = null;
    }

    private void OnSkillBF_02()
    {
        _skills._skillCover.SetActive(false);
        //_nina_Status.currentPlayerHP -= 10000f;
        _nina_Status.mpRegenRate += 15f;
        _skillGOList[_skillID] = null;
    }
    private void OnSkillBF_03()
    {
        _skills._skillCover.SetActive(false);
        //_nina_Attack.rangedAtk[0] = _currentRangedAtk;
        _nina_Status._canHPRegen = true;
        _skillGOList[_skillID] = null;
    }
}
