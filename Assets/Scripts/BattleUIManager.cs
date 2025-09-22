using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    //public GameObject UnitTargetArea;
    //public GameObject ActionChoiceArea;
    //public GameObject SkillChoiceArea;
    public GameObject allyResourceArea;

    public GameObject allyResourceButton;
    
    void OnEnable(){
        BattleControl.startOfBattleEvent += SpawnAllyButtons;
    }
    void OnDisable(){
        BattleControl.startOfBattleEvent += SpawnAllyButtons;
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
        foreach(GameObject ally in allies){
            GameObject allyButton = Instantiate(allyResourceButton, allyResourceArea.transform);
            allyButton.GetComponent<AllyButtonUI>().assignedUnit = ally.GetComponent<Unit>();
        }
        
    }


}
