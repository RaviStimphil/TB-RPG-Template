using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ActionHolder : MonoBehaviour
{
    public UnitAction tempAction;
    public GameObject testUnit;
    

    void Start()
    {
        tempAction = new UnitAction();
    }

    void OnEnable(){
        Debug.Log("Subscribing " + name);
        BattleChooseButton.chooseSource += AddSource;
        BattleChooseButton.chooseMainTarget += AddMainTarget;
        BattleChooseButton.chooseSkill += AddSkill;
    }
    void OnDisable(){
        BattleChooseButton.chooseSource -= AddSource;
        BattleChooseButton.chooseMainTarget -= AddMainTarget;
        BattleChooseButton.chooseSkill -= AddSkill;
    }
    public void AddSource(Unit unit){
        Debug.Log("Added Source");
        tempAction.source = unit.gameObject.GetComponent<Unit>();
        Debug.Log(tempAction.source.name);
       
    } 

    public void AddSkill(BaseSkill skill){
        Debug.Log("Added Skill");
        tempAction.skill = skill;
    } 

    public void AddMainTarget(Unit unit){
        Debug.Log("Added Target");
        tempAction.mainTarget = unit.gameObject.GetComponent<Unit>();
        Debug.Log(tempAction.mainTarget.name);
       
    } 
    


    // Update is called once per frame
    void Update()
    {
        
    }


}
