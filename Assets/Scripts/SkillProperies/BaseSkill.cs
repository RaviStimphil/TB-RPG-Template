using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseSkill", menuName ="ScriptableObjects/SkillParts/Skill")]
public class BaseSkill : ScriptableObject
{
    
    //Targetting can be general.
    // Start is called before the first frame update
    public enum TargettingType{
        SingleEnemy,
        SingleAlly,
        SingleUnit,
        AllEnemy,
        AllAlly,
        AllUnit,
        RandomEnemy,
        RandomAlly,
        RandomUnit,

    }
    public bool targetDead;
    public int numberOfRepeats;
    public List<string> skillCatagories;

    public DamageSkillAddon[] damagePart;
    

    public int damageCal(UnitStats stats){
        return damagePart[0].CalulateTotalDamage(stats);
    }
}
