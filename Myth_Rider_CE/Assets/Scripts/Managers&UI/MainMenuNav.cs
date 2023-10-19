using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenuNav : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _optionsMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlayBGM("Main Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTutorial1() //by khai wen to put main menu call tutorial level
    {
        SceneManager.LoadScene(2);
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
}
