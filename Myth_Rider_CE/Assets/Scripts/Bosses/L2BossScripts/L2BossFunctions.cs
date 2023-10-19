using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2BossFunctions : MonoBehaviour
{
    public static float CalculateDistance(Vector3 firstPos, Vector3 secondPos)
    {
        float Distance = (firstPos - secondPos).sqrMagnitude;
        return Distance;
    }
    public static Vector3 HomingDir(Vector3 ninaPos, Vector3 projectilePos)
    {
        Vector3 atkDirection = ninaPos - projectilePos;
        return atkDirection;
    }
    public static Vector3 NormalizedDir(Vector3 ninaPos, Vector3 projectilePos)
    {
        Vector3 atkDirection = ninaPos - projectilePos;
        atkDirection.Normalize();
        return atkDirection;
    }

    public static float AimTarget(Vector3 rightAxis, float rotateSpeed, Vector3 _atkDirection)
    {
        float angularVel;

        Vector3 rotateAmount = Vector3.Cross(-_atkDirection, rightAxis);
        angularVel = rotateAmount.y * rotateSpeed;

        return angularVel;
    }

    public static float NormalizedAimTarget(Vector3 rightAxis, float rotateSpeed, Vector3 _atkDirection)
    {
        float angularVel;

        _atkDirection.Normalize();

        Vector3 rotateAmount = Vector3.Cross(-_atkDirection, rightAxis);
        angularVel = rotateAmount.y * rotateSpeed;

        return angularVel;
    }

    public static Vector3 RandomPos(float xMin, float xMax, float yMin, float yMax)
    {
        Vector3 randomPos = new(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        return randomPos;
    }
}
