using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightForFood : Fight
{
    
    public override void Reset()
    {
        base.Reset();
        
        actionName = "FightForFood";

        preConditions = new WorldState[2];
        preConditions[0] = new WorldState(WorldState.Label.attackingForFood, 0);
        preConditions[1] = new WorldState(WorldState.Label.isHungry,0);

        WorldState[] afterNew = new WorldState[afterEffects.Length+1];
        afterEffects.CopyTo(afterNew, 0);
        afterEffects = afterNew;
        afterEffects[afterEffects.Length-1] = new WorldState(WorldState.Label.hasFood,0);
    }
}
