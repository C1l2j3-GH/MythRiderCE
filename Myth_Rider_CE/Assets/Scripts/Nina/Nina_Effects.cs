using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Nina_Effects
{
    public static bool _isKnocked;
    public static bool _isKnocking;
    public static bool _isIdle;
    public static IEnumerator KnockbackCD(Rigidbody2D rb2D, float kbForce, Vector2 direction, float kbCD, float inviTime)
    {
        _isKnocked = true;
        
        if (AudioManager.amInstance != null)
        {
            AudioManager.amInstance.PlaySF("Nina's take damage");
        }

        rb2D.velocity = new Vector2(0, 0);
        rb2D.AddForce(5 * -kbForce * direction, ForceMode2D.Force);

        //float remainingTime = kbCD;
        //while (remainingTime > 0)
        //{
        //    Debug.Log("Countdown: " + remainingTime.ToString("F2") + " seconds");
        //    yield return null; // Wait for the next frame
        //    remainingTime -= Time.deltaTime;
        //}

        yield return new WaitForSeconds(kbCD);
        _isKnocked = false;
        _isIdle = true;

        yield return new WaitForSeconds(0.01f);
        _isIdle = false;

        yield return new WaitForSeconds(inviTime);
        _isKnocking = false;
    }

}
