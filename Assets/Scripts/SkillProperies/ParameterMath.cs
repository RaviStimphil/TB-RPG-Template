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
}
