using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class BattleChooseButton : MonoBehaviour
{
    public GameObject source;
    public BaseSkill skill;
    public GameObject target;

    public static event Action<Unit> chooseSource;
    public static event Action<BaseSkill> chooseSkill;
    public static event Action<Unit> chooseMainTarget;
    public static event Action<Unit> clearUnitAction;
    // Start is called before the first frame update
    void Start()
    {
        source ??= new GameObject("Source");
        target ??= new GameObject("Target");
        skill ??= ScriptableObject.CreateInstance<BaseSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoosingSource(){
        Debug.Log("Source goes through");
        chooseSource?.Invoke(source.gameObject.GetComponent<Unit>());
    }
    public void ChoosingTarget(){
        Debug.Log("Target goes through");
        chooseMainTarget?.Invoke(target.gameObject.GetComponent<Unit>());
    }
    public void ChooseSkill(){
        Debug.Log("Skill goes through");
        chooseSkill?.Invoke(skill);
    }

}
