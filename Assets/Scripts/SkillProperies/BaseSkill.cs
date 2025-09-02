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
    public int skillPriority;
    public int turnDelay; 
    public List<string> skillCatagories;

    public DamageSkillAddon[] damagePart;
    

    public int damageCal(UnitStats stats){
        return damagePart[0].CalulateTotalDamage(stats);
    }

    public void BeforeSkillExecute(){
        
    }

    public void AfterSkillExecute(){

    }
    public void SkillExecute(Unit source, Unit target){
        if(damagePart.Length > 0){
            DealDamage(source, target, damageCal(source.unitStat));
        }

    }

    public void DealDamage(Unit source, Unit target, int amount){
        target.unitStat.currentHealth -= amount;
        Debug.Log(target.name + " has " + target.unitStat.currentHealth + " remaining.");
        if(target.unitStat.currentHealth <= 0){
            target.isDead = true;

        }
    }
}
