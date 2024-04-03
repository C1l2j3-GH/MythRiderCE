using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string _profileID = "";

    [Header("Content")]
    [SerializeField] private GameObject _noDataCont;
    [SerializeField] private GameObject _hasDataCont;

    ////[SerializeField] private TextMeshProUGUI _percentCompleteText;
    [SerializeField] private TextMeshProUGUI _mapNameText;
    [SerializeField] private TextMeshProUGUI _skillPointText;

    private Button _saveSlotButton;

    private void Awake()
    {
        _saveSlotButton = this.GetComponent<Button>();
    }
    public void SetData(GameData data)
    {
        if (data == null)
        {
            _noDataCont.SetActive(true);
            _hasDataCont.SetActive(false);
        }
        else
        {
            _noDataCont.SetActive(false);
            _hasDataCont.SetActive(true);

            _mapNameText.text = data._mapName;
            _skillPointText.text = "Skill Point: " + data._sdCurrentSP;
        }
    }

    public string GetProfileID()
    {
        return this._profileID;
    }

    public void SetInteractable(bool interactable)
    {
        _saveSlotButton.interactable = interactable;
    }
}
