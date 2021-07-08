using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToFreeWeakTypeOpponent : GoToFreeOpponent
{

    public new void Awake() {
        base.Awake();

        myType = p.GetPokemonType();
        weaknessType = p.GetWeaknessType();
        strengthType = p.GetStrengthType();

        targetType = strengthType;

    }
    public override void Reset()
    {
        p = gameObject.GetComponent<Pokemon>();
        duration = 5;
        actionName = "FindFreeWeakTypeOpponent";
        
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState(WorldState.Label.availablePokemon, 0);
        preConditions[1] = new WorldState("isViolent", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("attacking", 0);

    }

}