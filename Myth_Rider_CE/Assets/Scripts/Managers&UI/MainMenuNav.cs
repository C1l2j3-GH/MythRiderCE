using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenuNav : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueGameButton;

    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _optionsMenuUI;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame() //by khai wen to put main menu call tutorial level
    {
        ////SceneManager.LoadScene(2);
        DisableMenuButtons();
        DPManager._instance.NewGame();
        SceneManager.LoadSceneAsync(2);
    }

    public void ContinueGame()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync(2);
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

    public void QuitGame() //by khai wen to put main menu call tutorial level
    {
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        _newGameButton.interactable = false;
        _continueGameButton.interactable = false;
    }
}
