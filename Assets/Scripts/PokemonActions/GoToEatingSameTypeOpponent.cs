using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToEatingSameTypeOpponent : GoToEatingOpponent
{

    public new void Start() {
        p = gameObject.GetComponent<Pokemon>();
        myType = p.GetPokemonType();
        weaknessType = p.GetWeaknessType();
        strengthType = p.GetStrengthType();

        targetType = myType;

    }
    public override void Reset()
    {
        
        duration = 5;
        actionName = "FindEatingeSameTypeOpponent";
       
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState("isViolent", 0);
        preConditions[1] = new WorldState("eatingPokemon", 0);
        afterEffects = new WorldState[2];
        afterEffects[0] = new WorldState("attacking", 1);
        afterEffects[1] = new WorldState("attackingForFood", 1);

    }
}
