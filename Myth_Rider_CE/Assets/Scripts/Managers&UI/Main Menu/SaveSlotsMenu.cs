using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SaveSlotsMenu : MenuSelect
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenuNav _mainMenuNav;

    [Header("Menu Buttons")]
    [SerializeField] private Button _backButton;

    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmPUMenu _confirmPUMenu;

    private SaveSlot[] _saveSlots;

    private bool _isLoadingGame = false;
    // Start is called before the first frame update
    private void Awake()
    {
        _saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();
        
        //DPManager._instance.ChangeSelectedProfileID(saveSlot.GetProfileID()); // Save for study
        
        //if(!_isLoadingGame)
        //{
        //    DPManager._instance.NewGame();
        //}

        if(_isLoadingGame)
        {
            DPManager._instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
            SaveGameAndLoadScene();
        }
        else if(saveSlot._hasData)
        {
            _confirmPUMenu.ActivateMenu(
                "Starting a New Game with this slot will override the currently saved data. Are you sure?",
                //When Confirm Button is clicked
                () =>
                {
                    DPManager._instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
                    DPManager._instance.NewGame();
                    SaveGameAndLoadScene();
                },

                //When Cancel Button is clicked
                () => {
                    ActivateMenu(_isLoadingGame);
                }
                );
        }
        else
        {
            DPManager._instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
            DPManager._instance.NewGame();
            SaveGameAndLoadScene();
        }
    }

    private void SaveGameAndLoadScene()
    {
        DPManager._instance.SaveGame();

        ////SceneManager.LoadSceneAsync("SampleScene");
        SceneManager.LoadSceneAsync(2);
    }
    public void OnBackClicked()
    {
        _mainMenuNav.ActivateMenu();
        this.DeactivateMenu();
    }
    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);

        _isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profilesGameData = DPManager._instance.GetAllProfilesGameData();

        _backButton.interactable = true;

        GameObject firstSelected = _backButton.gameObject;

        foreach (SaveSlot saveSlot in _saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);

            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if(firstSelected.Equals(_backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }
        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        //StartCoroutine(this.SetFirstSelected(firstSelected)); // Save for study
        this.SetFirstSelected(firstSelectedButton);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach(SaveSlot saveSlot in _saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        _backButton.interactable = false;
    }
}
