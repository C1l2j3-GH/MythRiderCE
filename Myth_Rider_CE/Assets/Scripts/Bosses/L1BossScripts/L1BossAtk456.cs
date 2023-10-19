using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1BossAtk456 : L1BossAtkBase
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
    public float CalculateDistance(Vector3 firstPos, Vector3 secondPos)
    {
        float Distance = (firstPos - secondPos).sqrMagnitude;
        return Distance;
    }
    public Vector3 HomingProjectileDir(Vector3 targetPos, Vector3 projectilePos)
    {
        Vector3 atkDirection = targetPos - projectilePos;
        atkDirection.Normalize();
        return atkDirection;
    }
    public GameObject SpawnProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        GameObject projectileGO = GameObject.Instantiate(projectilePrefab, position, rotation);
        return projectileGO;
    }

    public GameObject SpawnProjectileAsChild(GameObject projectilePrefab, Vector3 position, Quaternion rotation, Transform parentTransform)
    {
        GameObject projectileGO = GameObject.Instantiate(projectilePrefab, position, rotation, parentTransform);
        return projectileGO;
    }
    public float AimTarget(Vector3 rightAxis, float rotateSpeed, Vector3 _atkDirection)
    {
        float angularVel;
        //_atkDirection.Normalize();

        Vector3 rotateAmount = Vector3.Cross(-_atkDirection, rightAxis);
        angularVel = rotateAmount.y * rotateSpeed;

        return angularVel;
    }

    public Vector3 RandomPos(float xMin, float xMax, float yMin, float yMax)
    {
        Vector3 randomPos = new(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        return randomPos;
    }
}
