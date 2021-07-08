using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeFood : GAction
{
    public override bool PostPerform()
    {
        Debug.Log(name + " finished eating, will now destroy food object");
        //release and destroy the food.
        GWorld.Instance.ConsumeFood(target); 
        beliefs.RemoveState(WorldState.Label.isHungry);
        //beliefs.RemoveState(WorldState.Label.isEating);
        GetComponent<Pokemon>().anim.SetBool("isEating", false);
        inventory.RemoveItem(target);

        return true;

    }

    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Food");
        if (target == null) {
            Debug.Log("Food not found in inventory when trying to eat");
            return false;
        }
        Debug.Log(name + " is eating");
        //beliefs.ModifyState(WorldState.Label.isEating,1);
        GetComponent<Pokemon>().anim.SetBool("isEating", true);
        return true;
    }

    public override void Reset()
    {
        actionName = "ConsumeFood";
        duration = 20;
        cost = 1;
        preConditions = new WorldState[2];
       
        preConditions[0] = new WorldState(WorldState.Label.hasFood, 0);
        
        preConditions[1] = new WorldState(WorldState.Label.isHungry, 0);
        afterEffects = new WorldState[1];
        
        afterEffects[0] = new WorldState(WorldState.Label.isFull, 0);

    }

}
