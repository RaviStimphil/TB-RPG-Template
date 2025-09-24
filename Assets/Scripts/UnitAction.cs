using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction
{
    public Unit source;
    public Unit mainTarget;
    //public List<Unit> targets;
    public BaseSkill skill;
    public bool checkedThrough;
    public int turnCount;
    public int turnOrder;

    public void CountTurnDown(){
        turnCount--;
    }

    public void CalulateTurnOrder(){
        turnOrder = ParameterMath.ActionPriority(source, skill);
    }
    public UnitAction(){

    }
    public UnitAction(Unit source, Unit mainTarget, BaseSkill skill){
        if(source == null){
            Debug.Log("source is null");
        }
        if(mainTarget == null){
            Debug.Log("Target is null");
        }
        if(skill == null){
            Debug.Log("skill is null");
        }
        //Debug.Log("Action was made");
        this.source = source;
        this.mainTarget = mainTarget;
        this.skill = skill;
    }

    public bool ValidAction(){
        bool valid = true;
        if(source == null){
            Debug.Log("Source is null");
            valid = false;
        }
        if(skill == null){
            Debug.Log("Skill is null");
            valid = false;
        }
        if(mainTarget == null){
            Debug.Log("MainTarget is null");
            valid = false;
        }
        return valid;
    }
    public void ClearAction(){
        source = null;
        mainTarget = null;
        skill = null;
    }

    public UnitAction Clone(){
        return new UnitAction{
            source = this.source,
            mainTarget = this.mainTarget,
            skill = this.skill,
            checkedThrough = this.checkedThrough,
            turnCount = this.turnCount,
            turnOrder = this.turnOrder
        };
    }

    /*public void AddTargets(Unit[] targets){
        //this.targets.AddRange(targets);
    }*/


}
