using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageSkillAddon", menuName ="ScriptableObjects/SkillParts/SkillDamage")]
public class DamageSkillAddon : ScriptableObject
{
    public int baseAccuracy;
    public int baseDamageAmount;
    public string[] elementDamageType;
    public int enemyDefenseMultiplier;
    public DamageScalingAddon[] damageScalingAddons;

    public int CalulateTotalDamage(UnitStats stats){
        int totalDamage = 0;
        foreach(DamageScalingAddon DSA in damageScalingAddons){
            switch(DSA.parameter){
                case 0:
                    totalDamage += (int) Math.Round(stats.attack * DSA.damageScalingMultipler);
                    break;
                default:
                    totalDamage += (int) Math.Round(stats.attack * DSA.damageScalingMultipler);
                    break;
            }
        }
        return totalDamage;
    } 

    
}
