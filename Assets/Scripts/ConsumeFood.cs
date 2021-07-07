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
        beliefs.RemoveState("isHungry");
        beliefs.RemoveState("isEating");
        return true;

    }

    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Food");
        if (target == null) {
            Debug.Log("Food not found in inventory when trying to eat");
            return false;
        }
        inventory.RemoveItem(target);
        Debug.Log(name + " is eating");
        beliefs.ModifyState("isEating",1);
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
