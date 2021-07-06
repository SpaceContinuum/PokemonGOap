using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightForFood : GAction
{
    GameObject attackTarget;
    public override bool PostPerform()
    {
        //make sure we got the food from our target when they were defeated.
        GameObject f = inventory.FindItemWithTag("Food");
        if (f != null) {
            beliefs.RemoveState(WorldState.Label.attacking);
            return true;
        }
        return false;

    }

    public override bool PrePerform()
    {
        attackTarget = inventory.FindItemWithTag("Pokemon");
        if (attackTarget == null) {
            return false;
        }
        inventory.RemoveItem(attackTarget);
        return true;
    }

    public override void Reset()
    {
        cost = 3;
        duration = 5;
        preConditions = new WorldState[3];
        preConditions[0] = new WorldState(WorldState.Label.attacking,0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.hasFood, 0);

    }
}
