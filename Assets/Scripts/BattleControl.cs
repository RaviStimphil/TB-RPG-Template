using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class BattleControl : MonoBehaviour
{
    public List<GameObject> allyUnits;
    public List<GameObject> enemyUnits;

    public List<GameObject> activeUnits;

    public List<UnitAction> actionQueue;

    public string winner = "";
    public bool endBattle; 
    public int turnCount;
    public UnitAction tempAction;
    public GameBattleData currentBattleData = new GameBattleData();

    public static event Action<UnitAction> AfterActionEvent;
    public static event Action<GameObject[], GameObject[]> startOfBattleEvent;
    public static event Action<GameBattleData> startOfTurnEvent;
    public static event Action<GameBattleData> endOfTurnEvent;
    public static event Action<Unit> chooseSource;
    public static event Action<BaseSkill> chooseSkill;



    void OnEnable(){
        //ActionHolder.addPlayerAction +=
        AllyButtonUI.allyButtonPress += AddSource;
        TargetButtonSelect.targetEnemyButtonPress += AddMainTarget;
        ActionBattleButton.actionButtonPress += AddSkill;
    }
    void OnDisable(){
        AllyButtonUI.allyButtonPress -= AddSource;
        TargetButtonSelect.targetEnemyButtonPress -= AddMainTarget;
        ActionBattleButton.actionButtonPress -= AddSkill;
    }
    void Awake(){
        tempAction = new UnitAction();
        actionQueue = new List<UnitAction>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentBattleData.UpdateData(allyUnits, enemyUnits, turnCount);
        startOfBattleEvent?.Invoke(allyUnits.ToArray(), enemyUnits.ToArray());
        
        StartOfTurn();
    
    }


    // Update is called once per frame
    void Update()
    {   
        /*if(!endBattle){
            BattleLoop();
        }*/
    }

    public void StartOfTurn(){
        startOfTurnEvent?.Invoke(currentBattleData);
        foreach(GameObject unit in allyUnits){
            unit.gameObject.GetComponent<Unit>().ResetActionPoint();
        }
        foreach(GameObject unit in enemyUnits){
            unit.gameObject.GetComponent<Unit>().ResetActionPoint();
        }
        AddFirstSource();
    }
    public void EndOfTurn(){
        turnCount++;
        endOfTurnEvent?.Invoke(currentBattleData);
        StartOfTurn();
    }
    public void BattleLoop(){
        
  
        //MakeAddNewAction();
        EnemiesAddAction();
        UnitAction[] tempActions = SortActionQueue(actionQueue.ToArray());
        actionQueue.Clear();
        actionQueue.AddRange(tempActions);
        ThroughActionQueue();
        if(AddAliveMembers(allyUnits.ToArray()).Count == 0 || AddAliveMembers(enemyUnits.ToArray()).Count == 0){
            endBattle = true;
            Debug.Log("It has ended");
        }
        EndOfTurn();
        
    }
    public void EnemiesAddAction(){
        foreach(GameObject unit in enemyUnits){
            //for loop for going through action points.
            tempAction.source = unit.GetComponent<Unit>();
            tempAction.skill = unit.GetComponent<Unit>().skills[0];
            tempAction.mainTarget = unit.GetComponent<Unit>().unitStat.ChooseTarget(allyUnits);
            AddToActionQueue(tempAction.Clone());
            tempAction.ClearAction();
       }
    }
    public void AddFirstSource(){
        Debug.Log("First source");
        Unit validSource = NextValidSource();
        if(validSource != null){
            tempAction.source = validSource;
            chooseSource?.Invoke(tempAction.source);
        }
        else{
            BattleLoop();
        }
        
        
    }
    public Unit NextValidSource(){
        if(allyUnits.Count < 1){
            Debug.Log("Nothing in allyUnits yet");    
        }
        for(int i = 0; i < allyUnits.Count; i++){
            if(allyUnits[i].gameObject.GetComponent<Unit>().actionPoints.currentValue > 0 && !allyUnits[i].gameObject.GetComponent<Unit>().isDead){
                Unit chosen = allyUnits[i].gameObject.GetComponent<Unit>();
                return chosen;
            }
        }
        return null;
    }
    public void AddSource(Unit unit){
        if(UnitAvailable(unit)){
            Debug.Log("Added Source");
            tempAction.source = unit.gameObject.GetComponent<Unit>();
            chooseSource?.Invoke(unit);
            Debug.Log(tempAction.source.name);
        }
        
       
    } 
    public bool UnitAvailable(Unit unit){
        if(unit.isDead || unit.actionPoints.currentValue < 1){
            return false;
        }
        else{
            return true;
        }
    }
    public void AddSkill(BaseSkill skill){
        Debug.Log("Added Skill");
        tempAction.skill = skill;
        chooseSkill?.Invoke(skill);
    } 

    public void AddMainTarget(Unit unit){
        Debug.Log("Added Target");
        tempAction.mainTarget = unit.gameObject.GetComponent<Unit>();
        Debug.Log(tempAction.mainTarget.name);
        if(tempAction.ValidAction()){
            AddToActionQueue(tempAction.Clone());
            tempAction.ClearAction();
            AddFirstSource();
        }else{
            Debug.Log("Action not finished");
        }
    }

    

    public void FullActionExecute(UnitAction action){
        BeforeActionExecute(action);
        DuringActionExecute(action);
        AfterActionExecute(action);
        AfterActionEvent?.Invoke(action);
    }
    
    public void ThroughActionQueue(){
        //Please note that this is likely to fail.
        for(int i = 0; i < actionQueue.Count; i++){
            if(actionQueue[i].checkedThrough == false){
                if(actionQueue[i].turnCount <= 0){
                    FullActionExecute(actionQueue[i]);
                    actionQueue.Remove(actionQueue[i]);
                    i--;
                }else{
                    actionQueue[i].checkedThrough = true;
                    actionQueue[i].turnCount -= 1;
                }
            }
        }
        
    }

    public UnitAction[] SortActionQueue(UnitAction[] actions){
        UnitAction[] newOrder = new UnitAction[actions.Length];
        for(int i = 0; i < newOrder.Length; i++){
            for(int j = i; j < newOrder.Length; j++){
                if(j == i){
                    newOrder[i] = actions[j];
                }
                else{
                    if(newOrder[i].turnCount > actions[j].turnCount){
                        newOrder[i] = actions[j];
                    }
                    else if(newOrder[i].turnOrder < actions[j].turnOrder){
                        newOrder[i] = actions[j];
                    }
                }
            }
        }
        return newOrder;
    }

    public void RevealVictor(){
        Debug.Log("The winner has been decided");
    }

    public void EndBattleLoop(){

    }



    public List<GameObject> AddAliveMembers(GameObject[] army){
        List<GameObject> finalArmy = new List<GameObject>();
        foreach (GameObject unit in army){
            if(!unit.gameObject.GetComponent<Unit>().isDead){
                finalArmy.Add(unit);
            }
        }
        return finalArmy;
    }

    
    //The 1's will be changed to like the action value of the skill. 
    public void AddToActionQueue(UnitAction action){
        action.source.actionPoints.currentValue -= 1;
        actionQueue.Add(action);
    }
    public void RemoveFromActionQueue(UnitAction action){
        action.source.actionPoints.currentValue += 1;
        actionQueue.Remove(action);
    }
    public void BeforeActionExecute(UnitAction action){
        Debug.Log(action.mainTarget.name);
        if(action.mainTarget.isDead){
            Debug.Log("Target is dead");
        }else{
            action.skill.BeforeSkillExecute();
        }
    }
    public void DuringActionExecute(UnitAction action){
        action.skill.SkillExecute(action.source, action.mainTarget);
    }

    public void AfterActionExecute(UnitAction action){
        action.skill.AfterSkillExecute();
        //RemoveFromActionQueue(action);
    }



}
