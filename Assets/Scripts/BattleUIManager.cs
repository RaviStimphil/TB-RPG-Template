using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public GameObject unitTargetArea;
    public GameObject actionChoiceArea;
    //public GameObject SkillChoiceArea;
    public GameObject allyResourceArea;

    public GameObject allyResourceButton;
    public GameObject actionChoiceButton;
    public GameObject unitTargetButton;
    
    void OnEnable(){
        BattleControl.startOfBattleEvent += SpawnAllyButtons;
        BattleControl.chooseSource += SpawnActionButtons;
        BattleControl.startOfBattleEvent += SpawnTargetButtons;
    }
    void OnDisable(){
        BattleControl.startOfBattleEvent -= SpawnAllyButtons;
        BattleControl.chooseSource -= SpawnActionButtons;
        BattleControl.startOfBattleEvent -= SpawnTargetButtons;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAllyButtons(GameObject[] allies, GameObject[] enemies){
        while (allyResourceArea.transform.childCount > 0) {
            DestroyImmediate(allyResourceArea.transform.GetChild(0).gameObject);
        }
        foreach(GameObject ally in allies){
            GameObject allyButton = Instantiate(allyResourceButton, allyResourceArea.transform);
            allyButton.GetComponent<AllyButtonUI>().assignedUnit = ally.GetComponent<Unit>();
        }
        
    }
    public void SpawnActionButtons(Unit unit){
        //BattleControl.startOfTurnEvent += AddFirstSource;
        while (actionChoiceArea.transform.childCount > 0) {
            DestroyImmediate(actionChoiceArea.transform.GetChild(0).gameObject);
        }
        if (unit == null){
            Debug.LogError("SpawnActionButtons received a NULL unit!");
            return;
        }
        if (unit.skills == null){
            Debug.LogError("Unit " + unit.name + " has NULL skills list!");
            return;
        }
        if (actionChoiceArea == null){
            Debug.LogError("actionChoiceArea is not assigned in inspector!");
            return;
        }
        if (actionChoiceButton == null){
            Debug.LogError("actionChoiceButton prefab is not assigned in inspector!");
            return;
        }
        foreach(BaseSkill skill in unit.skills){
            GameObject actionButton = Instantiate(actionChoiceButton, actionChoiceArea.transform);
            actionButton.GetComponent<ActionBattleButton>().assignedSkill = skill;
        }
    }
    public void SpawnTargetButtons(GameObject[] allies, GameObject[] enemies){
        while (unitTargetArea.transform.childCount > 0) {
            DestroyImmediate(unitTargetArea.transform.GetChild(0).gameObject);
        }
        foreach(GameObject enemy in enemies){
            GameObject enemyButton = Instantiate(unitTargetButton, unitTargetArea.transform);
            enemyButton.GetComponent<TargetButtonSelect>().assignedUnit = enemy.GetComponent<Unit>();
        }
        
    }

}
