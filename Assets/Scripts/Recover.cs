﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : GAction
{
    public override bool PostPerform()
    {
        Debug.Log(name + " has finished recovering");
        beliefs.RemoveState(WorldState.Label.stunned);
        transform.Rotate(new Vector3(0,0,-90));
        return true;
    }

    public override bool PrePerform()
    {
        Debug.Log(name + " is starting recovery.");
        return true;
    }

    public override void Reset()
    {
        actionName= "Recover";
        duration = 20;
        cost = 0;
        preConditions = new WorldState[1] ;
        preConditions[0] = new WorldState(WorldState.Label.stunned,0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.recovered,0);
    }
}