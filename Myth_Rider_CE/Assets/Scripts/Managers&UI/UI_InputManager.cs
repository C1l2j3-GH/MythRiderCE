using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_InputManager : MonoBehaviour
{
    public static UI_InputManager instance; //Singleton Pattern
    public bool _pauseMenuInput { get; private set; }
    private PlayerInput _playerInput;
    private InputAction _pauseMenuAction;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _playerInput = GetComponent<PlayerInput>();
        DontDestroyOnLoad(gameObject);
        _pauseMenuAction = _playerInput.actions["PauseMenuOnOff"];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _pauseMenuInput = _pauseMenuAction.WasPressedThisFrame();
    }
}
