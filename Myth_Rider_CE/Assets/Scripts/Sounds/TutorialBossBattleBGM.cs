using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossBattleBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlayBGM("Tutorial boss fight");
        }
    }

}
