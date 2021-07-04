using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeFood : GAction
{
    public override bool PostPerform()
    {
        //release and destroy the food.
        GameObject f = inventory.FindItemWithTag("Food");
        if (f == null) {
            Debug.Log("Food not found in inventory when trying to eat");
            return false;
        }
        GWorld.Instance.ConsumeFood(f);
        return true;

    }

    public override bool PrePerform()
    {
        beliefs.RemoveState("isHungry");
        return true;
    }

    public override void Reset()
    {
        duration = 10;
        cost = 0;
        preConditions = new WorldState[1];
        preConditions[0] = new WorldState("hasFood", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("isEating", 0);

    }

}
