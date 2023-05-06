using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornRemovalTeacher : Job
{
    public override string Work(int opt)
    {
        if(opt == 0)
        {
            SkillsAndPerks.playerSkill.AddOtherSkill("Horn Removal");
        }
        return "";
    }
}
