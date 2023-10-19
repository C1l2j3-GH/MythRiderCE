using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss_Atk3_EnergyBall2 : MonoBehaviour
{
    [Header("Preset")]
    [SerializeField] private GameObject _fireWallPrefab;
    [SerializeField] private GameObject _wallPos;
    [SerializeField] private GameObject _groundPos;

    [Header("Config")]
    [SerializeField] private Quaternion _rotationOffSet;
    //[Space(30f)]
    //[Header("Debug")]
    //[SerializeField] private SpriteRenderer _EnergyBallSR;
    //[SerializeField] private GameObject _EnergyBallGO;

    // Start is called before the first frame update
    void Start()
    {
        //_EnergyBallSR = GetComponent<SpriteRenderer>();
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Xuanwu's fire grenade");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            //_EnergyBallSR.color = new(1, 1, 1, 0);
            //gameObject.SetActive(false);
            Debug.Log("Collided");
            SpawnFirewall(_groundPos.transform.position, transform.rotation);
            transform.parent.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            //_EnergyBallSR.color = new(1, 1, 1, 0);
            //gameObject.SetActive(false);
            Debug.Log("Collided");
            SpawnFirewall(_wallPos.transform.position, _rotationOffSet);
            transform.parent.gameObject.SetActive(false);
        }

    }

    private void SpawnFirewall(Vector3 position, Quaternion rotation)
    {
        Instantiate(_fireWallPrefab, position, rotation);
    }
    //private GameObject SpawnFirewall(Vector3 position, Quaternion rotation)
    //{
    //    GameObject fireWallGO = Instantiate(_fireWallPrefab, position, rotation);
    //    return fireWallGO;
    //}
}
