using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlayBGM("Tutorial 1");
        }
    }
}
