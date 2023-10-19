using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    [SerializeField] private Transform[] _atk5CurvePoints;
    [SerializeField] private bool _isFlipped;
    [SerializeField] private float _interpolateSpd;
    [SerializeField] private float _interpolateAmount;
    [SerializeField] private Vector3 _tempPos;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 tempPos = transform.position;
        _tempPos = tempPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFlipped)
        {
            transform.position = _tempPos;
            //_interpolateAmount = (_interpolateAmount + _interpolateSpd * Time.deltaTime) % 1f;
            _interpolateAmount = _interpolateAmount + _interpolateSpd * Time.deltaTime;
            Vector3 lerpA = Vector3.Lerp(_atk5CurvePoints[0].position, _atk5CurvePoints[1].position, _interpolateAmount);
            Vector3 lerpB = Vector3.Lerp(_atk5CurvePoints[1].position, _atk5CurvePoints[2].position, _interpolateAmount);
            Vector3 lerpC = Vector3.Lerp(_atk5CurvePoints[2].position, _atk5CurvePoints[3].position, _interpolateAmount);
            Vector3 lerpAB = Vector3.Lerp(lerpA, lerpB, _interpolateAmount);
            Vector3 lerpBC = Vector3.Lerp(lerpB, lerpC, _interpolateAmount);
            transform.position = Vector3.Lerp(lerpAB, lerpBC, _interpolateAmount);
            Debug.Log("CurveMovement" + transform.position);
        }
        else
        {
            transform.position = _tempPos;
            //_interpolateAmount = (_interpolateAmount + _interpolateSpd * Time.deltaTime) % 1f;
            _interpolateAmount = _interpolateAmount + _interpolateSpd * Time.deltaTime;
            Vector3 lerpA = Vector3.Lerp(_atk5CurvePoints[3].position, _atk5CurvePoints[2].position, _interpolateAmount);
            Vector3 lerpB = Vector3.Lerp(_atk5CurvePoints[2].position, _atk5CurvePoints[1].position, _interpolateAmount);
            Vector3 lerpC = Vector3.Lerp(_atk5CurvePoints[1].position, _atk5CurvePoints[0].position, _interpolateAmount);
            Vector3 lerpAB = Vector3.Lerp(lerpA, lerpB, _interpolateAmount);
            Vector3 lerpBC = Vector3.Lerp(lerpB, lerpC, _interpolateAmount);
            transform.position = Vector3.Lerp(lerpAB, lerpBC, _interpolateAmount);
            Debug.Log("CurveMovement" + transform.position);
        }
    }
}
