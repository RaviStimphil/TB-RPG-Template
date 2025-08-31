using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction
{
    public Unit source;
    public Unit mainTarget;
    //public List<Unit> targets;
    public BaseSkill skill;
    public int turnCount;
    public int turnOrder;

    public void CountTurnDown(){
        turnCount--;
    }

    public void CalulateTurnOrder(){
        turnOrder = ParameterMath.ActionPriority(source, skill);
    }

    public UnitAction(Unit source, Unit mainTarget, BaseSkill skill){
        this.source = source;
        this.mainTarget = mainTarget;
        this.skill = skill;
    }

    public void AddTargets(Unit[] targets){
        //this.targets.AddRange(targets);
    }


}
