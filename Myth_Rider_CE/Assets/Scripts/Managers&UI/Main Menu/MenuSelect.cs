using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSelect : MonoBehaviour
{
    [Header("First Selected Button")]
    [SerializeField] private Button _firstSelected;

    protected virtual void OnEnable()
    {
        //StartCoroutine(SetFirstSelected(_firstSelected)); // Save for study
        SetFirstSelected(_firstSelected);
    }
    
    //public IEnumerator SetFirstSelected(GameObject firstSelectedObject) // Save for study
    //{
    //    EventSystem.current.SetSelectedGameObject(null);

    //    yield return new WaitForEndOfFrame();

    //    EventSystem.current.SetSelectedGameObject(firstSelectedObject);
    //}

    public void SetFirstSelected(Button firstSelectedButton)
    {
        firstSelectedButton.Select();
    }
}
