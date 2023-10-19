using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2BossAnimations : MonoBehaviour
{
    [Header("Preset")]
    [Tooltip("Preset")]
    [SerializeField] private GameObject _deathShine;
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

    public void DeathLightMid()
    {
        _deathShine.SetActive(true);
        Nina_Status._currentSP += 5;
    }

    public void DeathShineEnd()
    {
        _menus.LoadScene(6);
    }
}
