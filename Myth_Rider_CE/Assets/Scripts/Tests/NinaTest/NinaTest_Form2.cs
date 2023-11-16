using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinaTest_Form2 : NinaTest_BaseForm
{
    public override void EnterForm(NinaTest_Status ninaTest)
    {
        SpriteRenderer ninaMesh = ninaTest.GetComponent<SpriteRenderer>();
        //ninaMesh.material.SetColor("_Color", Color.white);
        ninaMesh.sharedMaterial.SetColor("_Color", Color.red);

        //Debug.Log("1st Form Called");
    }
    public override void UpdateForm(NinaTest_Status ninaTest)
    {

    }
    public override void ExitForm(NinaTest_Status ninaTest)
    {

    }
    public override void OnCollisionEnter(NinaTest_Status ninaTest)
    {

    }
    public override void OnTriggerEnter(NinaTest_Status ninaTest)
    {

    }

}
