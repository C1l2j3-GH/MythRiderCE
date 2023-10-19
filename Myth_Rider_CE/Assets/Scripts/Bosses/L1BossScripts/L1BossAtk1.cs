using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1BossAtk1 : L1BossAtkBase
{
    public override void EnterForm(L1BossAI l1Boss)
    {

    }
    public override void UpdateForm(L1BossAI l1Boss)
    {

    }
    public override void ExitForm(L1BossAI l1Boss)
    {

    }
    public override void OnCollisionEnter(L1BossAI l1Boss)
    {

    }
    public override void OnTriggerEnter(L1BossAI l1Boss)
    {

    }

    public GameObject ShootLaser(GameObject laser, Transform transform)
    {
        GameObject laserGO = Rigidbody2D.Instantiate(laser, transform.position, transform.rotation);
        return laserGO;
    }

}
