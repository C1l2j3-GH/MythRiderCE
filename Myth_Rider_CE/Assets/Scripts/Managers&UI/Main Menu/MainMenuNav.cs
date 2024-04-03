using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuNav : MenuSelect
{
    [Header("Main Menu")]
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueGameButton;
    [SerializeField] private Button _loadGameButton;

    [Header("Menu Navigation")]
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _optionsMenuUI;

    [SerializeField] private SaveSlotsMenu _saveSlotsMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlayBGM("Main Menu");
        }

        if (!DPManager._instance.HasGameData())
        {
            _continueGameButton.interactable = false;
            _loadGameButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame() //by khai wen to put main menu call tutorial level
    {
        _saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void ContinueGame()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync(2);
    }

    public void OnLoadGameClicked()
    {
        _saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }
    public void EnableOptions() //by khai wen to put main menu call tutorial level
    {
        _mainMenuUI.SetActive(false);
        _optionsMenuUI.SetActive(true);
    }

    public void DisableOptions()
    {
        _optionsMenuUI.SetActive(false);
        _mainMenuUI.SetActive(true);
    }


    private void DisableMenuButtons()
    {
        _newGameButton.interactable = false;
        _continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        ////this.gameObject.SetActive(true);
        _mainMenuUI.SetActive(true);
    }
    public void DeactivateMenu()
    {
        ////this.gameObject.SetActive(false);
        _mainMenuUI.SetActive(false);
    }

    public void QuitGame() //by khai wen to put main menu call tutorial level
    {
        Application.Quit();
    }
}
