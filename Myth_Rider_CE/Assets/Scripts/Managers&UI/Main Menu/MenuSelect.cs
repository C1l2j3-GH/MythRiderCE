using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuSelect : MonoBehaviour
{
    [Header("First Selected Button")]
    [SerializeField] private GameObject _firstSelected;

    protected virtual void OnEnable()
    {
        StartCoroutine(SetFirstSelected(_firstSelected));
    }
    
    public IEnumerator SetFirstSelected(GameObject firstSelectedObject)
    {
        EventSystem.current.SetSelectedGameObject(null);

        yield return new WaitForEndOfFrame();

        EventSystem.current.SetSelectedGameObject(firstSelectedObject);
    }
}
