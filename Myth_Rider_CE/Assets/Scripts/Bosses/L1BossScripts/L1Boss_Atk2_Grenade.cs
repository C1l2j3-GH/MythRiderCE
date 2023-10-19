using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss_Atk2_Grenade : DealDamageCont_E
{
    [Header("Preset")]
    [SerializeField] private Animator _animator;

    [Header("Config")]
    [SerializeField] private float _duration;
    
    [Header("Debug")]
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private bool _isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Xuanwu's fire grenade");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")
            ||collision.gameObject.CompareTag("Platform")
            || collision.gameObject.CompareTag("LeftWall") 
            || collision.gameObject.CompareTag("RightWall"))
        {
            if (!_isTriggered)
            {
                _rb2D.velocity = Vector2.zero;
                _animator.SetBool("isExploding", true);
                _isTriggered = true;
                Destroy(gameObject, _duration);
            }
        }

    }
}
