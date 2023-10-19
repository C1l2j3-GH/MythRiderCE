using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [Header("Preset")]
    [SerializeField] private GameObject _dialogCanvas;
    [SerializeField] private Text _dialogText;
    [SerializeField] private string[] _dialog;
    [Space(30f)]
    [SerializeField] private string[] _dialog2;
    [SerializeField] private int _dialogIndex = 0;
    [SerializeField] private float _wordSpeed;
    [SerializeField] private float _nextLineDelay = 5f;
    [SerializeField] private Menus _menus;

    private Coroutine _dialogCoroutine;

    //[Header("Debug")]

    // Start is called before the first frame update
    void Start()
    {
        _menus = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Menus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Nina_Status._isTriggeredTBossDlg) // Turn back on for tutorial boss
        {
            TBossStartDlg();
        }

        if(Menus._isOnTBossEndDialog)
        {
            TBossEndDlg();
        }

        if (OnDialog._onDialog)
        {
            L1BossDlg();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Nina_Status._isShowingTBossDlg)
            {
                CloseDialog();
            }
            if (Menus._isShowingTBossEndDialog)
            {
                CloseDialog2();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Nina_Status._isShowingTBossDlg)
            {
                NextLine();
            }
            if (Menus._isShowingTBossEndDialog)
            {
                NextLine2();
            }
        }
        //else if (Input.GetKeyDown(KeyCode.Space) && Nina_Status._isShowingTBossDlg)
        //{
        //    NextLine();
        //}
    }

    public void TBossStartDlg()
    {
        Menus.PauseGame(true, 0f);
        Menus._otherMenuOn = true;
        ////Time.timeScale = 0f;
        _dialogCanvas.SetActive(true);
        _dialogCoroutine = StartCoroutine(TypingDialog());
        Nina_Status._isTriggeredTBossDlg = false;
    }

    public void TBossEndDlg()
    {
        Menus.PauseGame(true, 0f);
        Menus._otherMenuOn = true;
        ////Time.timeScale = 0f;
        _dialogCanvas.SetActive(true);
        _dialogCoroutine = StartCoroutine(TypingDialog2());
        Menus._isOnTBossEndDialog = false;
    }
    public void L1BossDlg()
    {
        Menus.PauseGame(true, 0f);
        Menus._otherMenuOn = true;
        ////Time.timeScale = 0f;
        _dialogCanvas.SetActive(true);
        _dialogCoroutine = StartCoroutine(TypingDialog());
        OnDialog._onDialog = false;
    }
    IEnumerator TypingDialog()
    {
        int a = 0;
        foreach(char letter in _dialog[_dialogIndex].ToCharArray())
        {
            _dialogText.text += letter;
            a++;
            ////yield return new WaitForSeconds(_wordSpeed);
            yield return new WaitForSecondsRealtime(_wordSpeed);

            if (a == _dialog[_dialogIndex].ToCharArray().Length)
            {
                yield return new WaitForSecondsRealtime(_nextLineDelay);

                NextLine();
                break;
            }
        }
    }

    IEnumerator TypingDialog2()
    {
        int a = 0;
        foreach (char letter in _dialog2[_dialogIndex].ToCharArray())
        {
            _dialogText.text += letter;
            a++;
            ////yield return new WaitForSeconds(_wordSpeed);
            yield return new WaitForSecondsRealtime(_wordSpeed);

            if (a == _dialog2[_dialogIndex].ToCharArray().Length)
            {
                yield return new WaitForSecondsRealtime(_nextLineDelay);

                NextLine2();
                break;
            }
        }
    }
    public void CloseDialog()
    {
        StopDialogCoroutine();
        Menus.PauseGame(false, 1f);
        Menus._otherMenuOn = false;
        ////Time.timeScale = 1f;
        _dialogCanvas.SetActive(false);
        _dialogText.text = "";
        _dialogIndex = 0;

        if (Nina_Status._isShowingTBossDlg == true)
        {
            Nina_Status._isShowingTBossDlg = false;
            Nina_Status._isFinishedBossDlg = true;
        }
    }
    public void CloseDialog2()
    {
        StopDialogCoroutine();
        Menus.PauseGame(false, 1f);
        Menus._otherMenuOn = false;
        ////Time.timeScale = 1f;
        _dialogCanvas.SetActive(false);
        _dialogText.text = "";
        _dialogIndex = 0;

        if (Menus._isShowingTBossEndDialog == true)
        {
            Menus._isShowingTBossEndDialog = false;
            _menus.LoadScene(Menus._currentScene.buildIndex + 1);
        }
    }
    public void NextLine()
    {
        if (_dialogIndex < _dialog.Length - 1)
        {
            StopDialogCoroutine();
            _dialogText.text = "";
            _dialogIndex++;
            //_dialogText.text = _dialog[_dialogIndex];
            _dialogCoroutine = StartCoroutine(TypingDialog());
        }

        else
        {
            CloseDialog();
        }
    }
    public void NextLine2()
    {
        if (_dialogIndex < _dialog2.Length - 1)
        {
            StopDialogCoroutine();
            _dialogText.text = "";
            _dialogIndex++;
            //_dialogText.text = _dialog[_dialogIndex];
            _dialogCoroutine = StartCoroutine(TypingDialog2());
        }

        else
        {
            CloseDialog2();
        }
    }
    public void StopDialogCoroutine()
    {
        if (_dialogCoroutine != null)
        {
            StopCoroutine(_dialogCoroutine);
            _dialogCoroutine = null;
        }
    }
}
