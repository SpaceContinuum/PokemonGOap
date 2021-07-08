using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightForFood : Fight
{
    
    public override void Reset()
    {
        base.Reset();
        
        actionName = "FightForFood";

        WorldState[] preConsNew = new WorldState[preConditions.Length+1];
        preConditions.CopyTo(preConsNew, 0);
        preConditions = preConsNew;
        preConditions[preConditions.Length-1] = new WorldState(WorldState.Label.attackingForFood, 0);

        WorldState[] afterNew = new WorldState[afterEffects.Length+1];
        afterEffects.CopyTo(afterNew, 0);
        afterEffects = afterNew;
        afterEffects[afterEffects.Length-1] = new WorldState(WorldState.Label.hasFood,0);
    }
}
