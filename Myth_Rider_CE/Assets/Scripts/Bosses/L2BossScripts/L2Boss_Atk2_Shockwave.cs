using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Boss_Atk2_Shockwave : DealDamageOnce_E
{
    [SerializeField] private L2BossAI _l2BossAI;

    // Start is called before the first frame update
    void Start()
    {
        _l2BossAI = GameObject.FindGameObjectWithTag("L2Boss").GetComponent<L2BossAI>();
        
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Qinglong's ground pround");
        }
    }

    public void ShockwaveEnd()
    {
        _l2BossAI.Attack2ShockwaveEnd();
    }
}
