using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToOccupiedFood : GAction
{
    public override bool PostPerform()
    {
        target = GWorld.Instance.GetClosestEatenFood(gameObject).gameObject;
        if (target == null) {
                return false;
        }
        return true;    }

    public override bool PrePerform()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        actionName = "FindOccupiedFood";
        cost = 5;
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState("isHungry", 0);
        preConditions[1] = new WorldState("foodEaten", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("nearFood", 0);
    }

   
}
