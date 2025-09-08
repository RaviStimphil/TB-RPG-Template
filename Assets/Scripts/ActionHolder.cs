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
    public List<UnitAction> currentActions;

    public static event Action<UnitAction[]> playerActions;
    
    void Awake(){
        currentActions = new List<UnitAction>();
    }
    void Start()
    {
        tempAction = new UnitAction();
    }

    void OnEnable(){
        Debug.Log("Subscribing " + name);
        BattleChooseButton.chooseSource += AddSource;
        BattleChooseButton.chooseMainTarget += AddMainTarget;
        BattleChooseButton.chooseSkill += AddSkill;
        BattleChooseButton.addCurrentAction += AddCurrentAction;

    }
    void OnDisable(){
        BattleChooseButton.chooseSource -= AddSource;
        BattleChooseButton.chooseMainTarget -= AddMainTarget;
        BattleChooseButton.chooseSkill -= AddSkill;
        BattleChooseButton.addCurrentAction -= AddCurrentAction;
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
    public void AddCurrentAction(){
        currentActions.Add(tempAction);
    } 
    
    public void SendPlayerActions(){
        playerActions?.Invoke(currentActions.ToArray());
        currentActions.Clear();
    }



    // Update is called once per frame
    void Update()
    {
        
    }


}
