﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDG;
using IDG.FSClient;

public class SkillBase:ComponentBase
{
    public KeyNum key;
    public FixedNumber timer;
    public FixedNumber time;
    public virtual void UseOver()
    {
        timer = time;
     //   UnityEngine.Debug.LogError("UseOver");
    }
    public virtual void StayUse(){
      //  timer = time;
      //  UnityEngine.Debug.LogError("StayUse");
    }
    public override void Update()
    {
     
        if (timer > 0)
        {
            timer -= data.deltaTime;
        }
        else
        {
            if(data.Input.GetKey(key)){
                StayUse();
            }
            if (data.Input.GetKeyUp(key))
            {
                
                UseOver();
            }
        }
    }
}
public class SkillList:ComponentBase
{
    public Dictionary<KeyNum, List<SkillBase>> skillTable;
    
    public SkillList()
    {
        skillTable = new Dictionary<KeyNum, List<SkillBase>>();
        skillTable.Add(KeyNum.Skill1, new List<SkillBase>());
        skillTable.Add(KeyNum.Skill2, new List<SkillBase>());
        skillTable.Add(KeyNum.Skill3, new List<SkillBase>());
    }
    public void AddSkill(SkillBase skill)
    {
        skill.InitNetData(this.data);
        if (skillTable.ContainsKey(skill.key))
        {
            skillTable[skill.key].Add( skill);
        }
    }
    public override void Update()
    {
        foreach (var item in skillTable)
        {
            if (item.Value.Count>0)
            {
                item.Value[item.Value.Count-1].Update();
            }
        } 
    }
}

