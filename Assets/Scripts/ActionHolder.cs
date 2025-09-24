using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ActionHolder : MonoBehaviour
{
    public UnitAction tempAction;
    public List<UnitAction> currentActions;

    public static event Action<UnitAction[]> playerActions;
    
    public static event Action<UnitAction> addPlayerAction;
    
    void Awake(){
        currentActions = new List<UnitAction>();
    }
    void Start()
    {
        tempAction = new UnitAction();
    }

    void OnEnable(){
        Debug.Log("Subscribing " + name);
        AllyButtonUI.allyButtonPress += AddSource;
        TargetButtonSelect.targetEnemyButtonPress += AddMainTarget;
        ActionBattleButton.actionButtonPress += AddSkill;
        BattleChooseButton.addCurrentAction += AddCurrentAction;
        BattleControl.startOfTurnEvent += AddFirstSource;

    }
    void OnDisable(){
        AllyButtonUI.allyButtonPress -= AddSource;
        BattleChooseButton.chooseMainTarget -= AddMainTarget;
        ActionBattleButton.actionButtonPress -= AddSkill;
        BattleChooseButton.addCurrentAction -= AddCurrentAction;
        BattleControl.startOfTurnEvent -= AddFirstSource;
    }

    public void AddFirstSource(GameBattleData data){
        if(NextValidSource(data) != null){
            tempAction.source = NextValidSource(data);
        }
        
        //chooseSource?.Invoke(tempAction.source);
    }
    public Unit NextValidSource(GameBattleData data){
        Unit chosen = new Unit();
        for(int i = 0; i < data.allyUnits.Count; i++){
            if(data.allyUnits[i].gameObject.GetComponent<Unit>().actionPoints.currentValue > 0 && !data.allyUnits[i].gameObject.GetComponent<Unit>().isDead){
                chosen = data.allyUnits[i].gameObject.GetComponent<Unit>();
                return chosen;
            }
        }
        return null;
    }
    public void AddSource(Unit unit){
        Debug.Log("Added Source");
        tempAction.source = unit.gameObject.GetComponent<Unit>();
        //chooseSource?.Invoke(unit);
        Debug.Log(tempAction.source.name);
       
    } 

    public void AddSkill(BaseSkill skill){
        Debug.Log("Added Skill");
        tempAction.skill = skill;
        //chooseSkill?.Invoke(skill);
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
