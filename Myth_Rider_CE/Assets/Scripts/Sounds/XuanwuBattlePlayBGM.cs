using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuanwuBattlePlayBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlayBGM("Xuanwu boss fight");
        }
    }
}
