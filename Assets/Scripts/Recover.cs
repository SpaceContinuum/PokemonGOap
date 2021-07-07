using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : GAction
{
    public override bool PostPerform()
    {
        Debug.Log(name + " has finished recovering");
        gameObject.GetComponent<Pokemon>().SetStun(false);
        GetComponent<Pokemon>().anim.SetBool("isStunned", false);
        return true;
    }

    public override bool PrePerform()
    {
        target= gameObject;
        Debug.Log(name + " is starting recovery.");
        return true;
    }

    public override void Reset()
    {
        actionName= "Recover";
        duration = 20;
        cost = 1;
        preConditions = new WorldState[1] ;
        preConditions[0] = new WorldState(WorldState.Label.stunned,0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.recovered,0);
    }
}
