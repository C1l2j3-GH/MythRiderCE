using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamBahaviour : MonoBehaviour
{
    [Header("Config")]
    [Tooltip("Config")]
    [SerializeField] private float _cameraDelay = 0f;

    [Header("Debug")]
    [Tooltip("Debug")]
    [SerializeField] private GameObject _ninaGO;
    [SerializeField] private CinemachineVirtualCamera _virtualCam;

    // Start is called before the first frame update
    void Start()
    {
        _ninaGO = GameObject.FindGameObjectWithTag("Player");
        _virtualCam = GetComponent<CinemachineVirtualCamera>();
        //_virtualCam.Follow = _ninaGO.transform;
        StartCoroutine(CameraDelay(_cameraDelay));
    }

    private IEnumerator CameraDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _virtualCam.Follow = _ninaGO.transform;
    }
}
