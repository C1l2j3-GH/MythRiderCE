using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [Header("Preset")]
    [Tooltip("Preset")]
    [SerializeField] private GameObject _tBossGO;
    [SerializeField] private GameObject _l1BossGO;
    [SerializeField] private GameObject _l2BossGO;
    //Level2
    [SerializeField] private GameObject _l2LeftBorder;
    [SerializeField] private GameObject _l2RightBorder;
    [SerializeField] private GameObject _l2DropBorder;

    [Header("Config")]
    [Tooltip("Config")]
    [SerializeField] private float _findNinaDelay = 0.5f;
    
    [Header("Debug")]
    [Tooltip("Debug")]
    [SerializeField] private Nina_Status _nina_Status;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(SpawnDelay(_findNinaDelay));
    }

    private IEnumerator SpawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _nina_Status = GameObject.FindGameObjectWithTag("Player").GetComponent<Nina_Status>();


        if(_tBossGO !=  null)
        {
            _nina_Status._tBossGO = _tBossGO;
        }
        if (_l1BossGO != null)
        {
            _nina_Status._l1bossGO = _l1BossGO;
        }
        if (_l2BossGO != null)
        {
            _nina_Status._l2bossGO = _l2BossGO;
            _nina_Status._l2DropBorder = _l2DropBorder;
            _nina_Status._l2LeftBorder = _l2LeftBorder;
            _nina_Status._l2RightBorder = _l2RightBorder;
        }
    }
}
