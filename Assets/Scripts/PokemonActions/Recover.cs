using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : GAction
{
    public override bool PostPerform()
    {
        Debug.Log(name + " has finished recovering");
        GetComponent<Pokemon>().anim.SetBool("isStunned", false);
        beliefs.RemoveState(WorldState.Label.isStunned);
        GWorld.Instance.PokemonStunned2Free(gameObject);
        return true;
    }

    public override bool PrePerform()
    {
        target= gameObject;
        Debug.Log(name + " is starting recovery.");
        target = gameObject;
        GetComponent<Pokemon>().anim.SetBool("isStunned", true);
        return true;
    }

    public override void Reset()
    {
        actionName= "Recover";
        duration = 20;
        cost = 1;
        preConditions = new WorldState[1] ;
        preConditions[0] = new WorldState(WorldState.Label.isStunned,0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.isRecovered,0);
    }
}
