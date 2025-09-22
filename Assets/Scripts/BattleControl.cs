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
    public UnityEvent BattleComplete;
    public bool endBattle; 
    public int turnCount;
    public UnitAction actOne;
    public UnitAction actTwo;
    public GameBattleData currentBattleData = new GameBattleData();

    public static event Action<UnitAction> AfterActionEvent;
    public static event Action<GameObject[], GameObject[]> startOfBattleEvent;
    public static event Action<GameBattleData> startOfTurnEvent;


    void OnEnable(){
        ActionHolder.playerActions += BattleLooper;
    }
    void OnDisable(){
        ActionHolder.playerActions += BattleLooper;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentBattleData.UpdateData(allyUnits, enemyUnits, turnCount);
        startOfBattleEvent?.Invoke(allyUnits.ToArray(), enemyUnits.ToArray());
        startOfTurnEvent?.Invoke(currentBattleData);
        actionQueue = new List<UnitAction>();
        if(actOne != null){
            Debug.Log("Not null baby");
        }else{
            Debug.Log("Is null");
        }
        actOne = new UnitAction(allyUnits[0].GetComponent<Unit>(), enemyUnits[0].GetComponent<Unit>(), allyUnits[0].GetComponent<Unit>().skills[0]);
        actTwo = new UnitAction(enemyUnits[0].GetComponent<Unit>(), allyUnits[0].GetComponent<Unit>(), enemyUnits[0].GetComponent<Unit>().skills[0]);
        
        //AddToActionQueue(actOne);
        //AddToActionQueue(actTwo);

        UnitAction newAction = null;
        newAction = new UnitAction(allyUnits[0].GetComponent<Unit>(), enemyUnits[0].GetComponent<Unit>(), allyUnits[0].GetComponent<Unit>().skills[0]);
        
    }


    // Update is called once per frame
    void Update()
    {   
        /*if(!endBattle){
            BattleLoop();
        }*/
    }

    public void BattleLooper(UnitAction[] actions){
        actionQueue.AddRange(actions);
        BattleLoop();
    }
    public void BattleLoop(){
        
  
        //MakeAddNewAction();
        UnitAction[] tempActions = SortActionQueue(actionQueue.ToArray());
        actionQueue.Clear();
        actionQueue.AddRange(tempActions);
        ThroughActionQueue();
        if(AddAliveMembers(allyUnits.ToArray()).Count == 0 || AddAliveMembers(enemyUnits.ToArray()).Count == 0){
            endBattle = true;
            Debug.Log("It has ended");
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

    public GameObject TopAliveUnit(List<GameObject> army){
        if (army == null || army.Count == 0) return null;

        for (int i = 0; i < army.Count; i++){
            if (army[i] != null && !army[i].gameObject.GetComponent<Unit>().isDead){
                return army[i]; 
            }
        }

        return null; 
    }

    public void MakeAddNewAction(){
        UnitAction allyAction = null;
        allyAction = new UnitAction(allyUnits[0].GetComponent<Unit>(), enemyUnits[0].GetComponent<Unit>(), allyUnits[0].GetComponent<Unit>().skills[0]);
        AddToActionQueue(allyAction);

        UnitAction enemyAction = null;
        enemyAction = new UnitAction(enemyUnits[0].GetComponent<Unit>(), allyUnits[0].GetComponent<Unit>(), enemyUnits[0].GetComponent<Unit>().skills[0]);
        AddToActionQueue(enemyAction);
    }

    public void TurnStart(){
        Debug.Log("Turn Count: " + turnCount);
        activeUnits.AddRange(AddAliveMembers(allyUnits.ToArray()));
        if(activeUnits.Count == 0){
            winner = "Enemy Won";
            endBattle = true;
        }
        activeUnits.AddRange(AddAliveMembers(enemyUnits.ToArray()));
        if(AddAliveMembers(allyUnits.ToArray()).Count == activeUnits.Count){
            winner = "Ally Won";
            endBattle = true;
        }

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

    public void TurnAction(){
        while(activeUnits.Count > 0){
            if(activeUnits[0].gameObject.GetComponent<Unit>().isGood){
                if(TopAliveUnit(enemyUnits) != null){
                    activeUnits[0].gameObject.GetComponent<Unit>().DealDamage(TopAliveUnit(enemyUnits));
                }
                
            }
            else{
                if(TopAliveUnit(allyUnits) != null){
                    activeUnits[0].gameObject.GetComponent<Unit>().DealDamage(TopAliveUnit(allyUnits));
                }
                
            }
            activeUnits.RemoveAt(0);
        }
    }
    public void AddToActionQueue(UnitAction action){
        if(action == null){
            Debug.Log("action is null");
        }
        Debug.Log(actionQueue.Count);
        actionQueue.Add(action);
    }
    public void RemoveFromActionQueue(UnitAction action){
        actionQueue.Remove(action);
    }
    public void BeforeActionExecute(UnitAction action){
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
