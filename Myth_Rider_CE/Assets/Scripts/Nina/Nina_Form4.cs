using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nina_Form4 : Nina_BaseForm
{
    public override void EnterForm(Nina_Status nina)
    {
        SpriteRenderer ninaMesh = nina.GetComponent<SpriteRenderer>();
        //ninaMesh.material.SetColor("_Color", Color.green);
        ninaMesh.sharedMaterial.SetColor("_Color", Color.green);
        Debug.Log("4th Form Called");
    }
    public override void UpdateForm(Nina_Status nina)
    {

    }
    public override void ExitForm(Nina_Status nina)
    {

    }
    public override void OnCollisionEnter(Nina_Status nina)
    {

    }
    public override void OnTriggerEnter(Nina_Status nina)
    {

    }
}
