using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedCreeps : DealDamageCont_E, IFTakeDamage_E
{
    [Header("Config")]
    [Tooltip("Config")]
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private float _moveSpd = 5f;
    [SerializeField] private float _moveSpd2 = 5f;
    [SerializeField] private float _incSpdCD = 2f;
    [SerializeField] private float _maxHP = 70f;

    [Header("Debug")]
    [Tooltip("Debug")]
    [SerializeField] private float _incSpdCDCount;
    [SerializeField] private float _currentHP = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _currentHP = _maxHP;     
        _rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb2D.velocity = new Vector2(-_moveSpd, 0);

        if(_incSpdCDCount < _incSpdCD)
        {
            _incSpdCDCount += Time.deltaTime;
        }
        else
        {
            //Debug.Log("I am SPEEEEED");
            _moveSpd = _moveSpd2;
        }

        Destroy(gameObject, 5f);

        if(_currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
    }
}
