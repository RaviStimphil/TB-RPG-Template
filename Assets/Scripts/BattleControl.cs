using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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


    // Start is called before the first frame update
    void Start()
    {
        actOne = new UnitAction(allyUnits[0].GetComponent<Unit>(), enemyUnits[0].GetComponent<Unit>(), allyUnits[0].GetComponent<Unit>().skills[0]);
        actTwo = new UnitAction(enemyUnits[0].GetComponent<Unit>(), allyUnits[0].GetComponent<Unit>(), enemyUnits[0].GetComponent<Unit>().skills[0]);
        AddToActionQueue(actOne);
        AddToActionQueue(actTwo);

    }


    // Update is called once per frame
    void Update()
    {   
        if(!endBattle){
            BattleLoop();
        }
    }

    public void BattleLoop(){
        
        //TurnStart();
        //TurnAction();
        Debug.Log("Something happened");
        BeforeActionExecute(actOne);
        DuringActionExecute(actOne);
        AfterActionExecute(actOne);

        BeforeActionExecute(actTwo);
        DuringActionExecute(actTwo);
        AfterActionExecute(actTwo);

        if(AddAliveMembers(allyUnits.ToArray()).Count == 0 || AddAliveMembers(enemyUnits.ToArray()).Count == 0){
            endBattle = true;
            Debug.Log("It has ended");
        }
        
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
            return army[i]; // first alive one
        }
    }

    return null; // none alive
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
    public void te(){
        
    }
    public void AddToActionQueue(UnitAction action){
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
