﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToFreeWeakTypeOpponent : GoToFreeOpponent
{

    public override void Reset()
    {
        p = gameObject.GetComponent<Pokemon>();
        duration = 5;
        actionName = "FindFreeWeakTypeOpponent";
        myType = p.GetPokemonType();
        weaknessType = p.GetWeaknessType();
        strengthType = p.GetStrengthType();

        targetType = weaknessType;

        preConditions = new WorldState[1];
        //preConditions[0] = new WorldState("availablePokemon", 0);
        preConditions[0] = new WorldState("isViolent", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("attacking", 0);

    }

}