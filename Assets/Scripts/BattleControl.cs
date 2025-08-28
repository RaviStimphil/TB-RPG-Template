using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleControl : MonoBehaviour
{
    public List<GameObject> allyUnits;
    public List<GameObject> enemyUnits;

    public List<GameObject> activeUnits;

    public string winner = "";
    public UnityEvent BattleComplete;
    public bool endBattle; 
    public int turnCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {   
        if(!endBattle){
            BattleLoop();
        }
    }

    public void BattleLoop(){
        
        TurnStart();
        TurnAction();
        
        
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



}
