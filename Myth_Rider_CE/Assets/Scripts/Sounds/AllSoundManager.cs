using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSoundManager : MonoBehaviour
{
    public static AllSoundManager _ASM_instance;

    private void Awake()
    {
        if (_ASM_instance == null)
        {
            _ASM_instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
