using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ActionBattleButton : MonoBehaviour
{
    public BaseSkill assignedSkill;
    public static event Action<BaseSkill> actionButtonPress;

    void Awake(){
        assignedSkill ??= null;
    }
    public void ChooseSkill(){
        Debug.Log("Skill goes through");
        actionButtonPress?.Invoke(assignedSkill);
    }
}
