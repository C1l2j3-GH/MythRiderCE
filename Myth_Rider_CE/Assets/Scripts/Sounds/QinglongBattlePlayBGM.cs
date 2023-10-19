using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QinglongBattlePlayBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlayBGM("Qinglong boss fight");
        }
    }
}
