using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss_Animations : MonoBehaviour
{
    [Header("Preset")]
    [Tooltip("Preset")]
    [SerializeField] private L1BossAI _l1BossAI;
    [SerializeField] private GameObject _deathShine;
    [SerializeField] private GameObject _cutSceneGO;
    [SerializeField] private Menus _menus;

    // Start is called before the first frame update
    void Start()
    {
        _menus = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Menus>();
        _cutSceneGO = _menus._cutSceneGO;
    }

    private void OnEnable()
    {
        _menus = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Menus>();
        _cutSceneGO = _menus._cutSceneGO;
    }

    public void DeathLightMid()
    {
        _deathShine.SetActive(true);
        Nina_Status._currentSP += 5;
    }

    public void DeathShineEnd()
    {
        _cutSceneGO.SetActive(true);
    }
}
