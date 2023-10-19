using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Boss_Animations : MonoBehaviour
{
    [Header("Preset")]
    [Header("Level 2 Boss Preset")]
    [SerializeField] L2BossAI _l2BossAI;
    [SerializeField] GameObject _lbLight;

    private void Start()
    {
        _l2BossAI = GameObject.FindGameObjectWithTag("L2Boss").GetComponent<L2BossAI>();
    }
    public void Attack3MeleeUpStart() //Animation Event for atk3Movement 0
    {
        _l2BossAI.Attack3MeleeUpStart();
    }
    public void Attack3MeleeUpEnd() //Animation Event for atk3Movement 0
    {
        _l2BossAI.Attack3MeleeUpEnd();
    }
    public void Attack3MeleeDownStart() //Animation Event for atk3Movement 1
    {
        _l2BossAI.Attack3MeleeDownStart();
    }
    public void Attack3MeleeDownEnd() //Animation Event for atk3Movement 1
    {
        _l2BossAI.Attack3MeleeDownEnd();
    }

    public void Attack3TornadoEnd()
    {
       _l2BossAI.Attack3TornadoEnd();
    }

    public void LimitBreakFly()
    {
        _l2BossAI.LimitBreakFly();
    }
    public void LimitBreakEnd() //Animation Event for limit break
    {
        _l2BossAI.LimitBreakEnd();
    }

    public void EnableLBLight()
    {
        _lbLight.SetActive(true);
    }
    public void Attack5TornadoEnd()
    {
        _l2BossAI.Attack5TornadoEnd();
    }
}
