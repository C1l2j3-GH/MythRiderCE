using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAnimations : MonoBehaviour
{
    [SerializeField] private Menus _menus;

    // Start is called before the first frame update
    void Start()
    {
        _menus = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Menus>();
    }

    private void OnEnable()
    {
        _menus = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Menus>();
    }
    public void EndingEnd()
    {
        _menus.LoadScene(1);
    }
}
