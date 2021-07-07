using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: current implemnentation means defending pokemon will always lose.

public class DefendFood : GAction
{
    
    GameObject attacker;
    public override bool PostPerform()
    {
        beliefs.RemoveState(WorldState.Label.underAttack);
        gameObject.GetComponent<Pokemon>().SetStun(true);

        return true;
    }

    public override bool PrePerform()
    {
        attacker = inventory.FindItemWithTag("Pokemon");
        if (attacker == null) {
            //where's our attacker? that's odd.
            Debug.Log(name + " can't find attacker object");
            beliefs.RemoveState(WorldState.Label.underAttack);
            return false;
        }
        
        inventory.RemoveItem(attacker);
        
        GameObject f = inventory.FindItemWithTag("Food");
        if (f == null) {
            //where's the food we were supposed to have?
            Debug.Log(name + " can't find food in inventory");
            beliefs.RemoveState(WorldState.Label.underAttack);
            return false;
        }

        //give up our food. 
        //Theoretically, this should happen in "PostPerform" but we haven't solved the synchronization of attacks.
        attacker.GetComponent<GAgent>().inventory.AddItem(f);
        
        return true;
        
    }

    public override void Reset()
    {
        actionName = "DefendFood";
        duration = 3;
        cost = 0; //this should always be a priority since there's no alternative.
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState(WorldState.Label.underAttack,0);
        preConditions[1] = new WorldState(WorldState.Label.hasFood, 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.safeFromAttack, 0);

    }

}
