using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuanWuEnd : MonoBehaviour
{
    [SerializeField] private Menus _menus;
    public void XuanWuEndLoad()
    {
        _menus.LoadScene(5);
        transform.parent.gameObject.SetActive(false);
    }
}
