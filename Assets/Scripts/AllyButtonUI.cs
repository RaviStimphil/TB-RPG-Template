using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

public class AllyButtonUI : MonoBehaviour
{
    public GameObject HPBar;
    public GameObject SPBar;
    public GameObject UltBar;
    public Unit assignedUnit;
    public static event Action<Unit> allyButtonPress;

    void Awake(){
        assignedUnit ??= null;
    }
    void OnEnable(){
        Unit.currentStatusAlert += UpdateStatusBars;
    }
    void OnDisable(){
        Unit.currentStatusAlert -= UpdateStatusBars;
    }

    public void UpdateStatusBars(Unit unit){
        if(unit == assignedUnit){
            Slider HPSlider = HPBar.GetComponent<Slider>();
            HPSlider.maxValue = unit.unitStat.maxHealth;
            HPSlider.value = unit.unitStat.currentHealth;
        }
        
    }

    public void ChoosingSource(){
        Debug.Log("Source goes through");
        allyButtonPress?.Invoke(assignedUnit);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
