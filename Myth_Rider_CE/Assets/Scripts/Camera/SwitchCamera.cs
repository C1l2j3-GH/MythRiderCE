using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Preset")]
    [Tooltip("Preset")]
    [SerializeField] private GameObject _walkZoneVCam;
    [SerializeField] private GameObject _bossZoneVCam;
    [SerializeField] private GameObject _tutorial2Door; //Turn Back On

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_walkZoneVCam != null)
            {
                _bossZoneVCam.SetActive(true);
                _walkZoneVCam.SetActive(false);
                _tutorial2Door.SetActive(true);
            }
        }
    }
}
