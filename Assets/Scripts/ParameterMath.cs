using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParameterMath
{
    public static int ActionPriority(Unit source, BaseSkill skill){
        int speed = 0;
        speed = source.unitStat.speed + skill.skillPriority;
            
        return speed;
    }
    public static int BasicDamageCalculation(UnitAction thing){
        return 0;
    }
    public static int FinalParameter(int baseAmount, float ratioAmount, int addedAmount, int equipAmount){
        int finalAmount = 0;
        finalAmount = (int) Mathf.Round((baseAmount + equipAmount) * ratioAmount + addedAmount);

        return finalAmount; 
    }
}
