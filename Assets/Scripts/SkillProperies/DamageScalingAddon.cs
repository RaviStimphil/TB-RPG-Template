using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageScaling", menuName ="ScriptableObjects/SkillParts/SkillScaling")]
public class DamageScalingAddon : ScriptableObject
{
    // Start is called before the first frame update
    public enum StatToScale{
        attack,
        defenese
    }
    public StatToScale parameter;
    public bool fromSource = true;
    public float damageScalingMultipler;
    
}
