using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Form01 Skill01", menuName = "Skills SO", order = 1)]
public class SkillsSO : ScriptableObject
{
    public string _skillName;
    public string _skillDesc;
    public int _spToUnlock;
    public int _skillID;
}
