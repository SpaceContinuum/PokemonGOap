using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToEatingSameTypeOpponent : GoToEatingOpponent
{

    public new void Start() {
        p = gameObject.GetComponent<Pokemon>();
        myType = p.GetMyPokemonType();
        myWeaknessType = p.GetMyWeaknessType();
        myStrengthType = p.GetMyStrengthType();

        targetType = myType;

    }

 

    public override void Reset()
    {
        
        duration = 5;
        actionName = "FindEatingSameTypeOpponent";
       
        preConditions = new WorldState[1];
        //preConditions[0] = new WorldState("isViolent", 0);
        preConditions[0] = new WorldState("eatingPokemon", 0);
        afterEffects = new WorldState[2];
        afterEffects[0] = new WorldState("attacking", 1);
        afterEffects[1] = new WorldState("attackingForFood", 1);

    }
}
