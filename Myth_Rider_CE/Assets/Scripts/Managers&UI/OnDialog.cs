using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDialog : MonoBehaviour
{
    public static bool _onDialog;

    public void OnDialogFunction()
    {
        _onDialog = true;
        Nina_Status._isShowingTBossDlg = true;
        transform.parent.gameObject.SetActive(false);
    }

    public void PlayWarningSFX()
    {
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Warning sound effect");
        }
    }
}
