using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GoToFreeOpponent : GAction
{
    protected Pokemon p;
    protected Pokemon.PokemonType myType;
    protected Pokemon.PokemonType weaknessType;
    protected Pokemon.PokemonType strengthType;
    protected Pokemon.PokemonType targetType;

    public new int cost
    {
        get
        {
            int coef;
            if (targetType == myType)
            {
                coef = 2;
            }
            else if (targetType == strengthType)
            {
                coef = 1;
            }
            else 
            {
                coef = 100;
            }

            Pokemon p = GWorld.Instance.GetClosestFreePokemon(gameObject, targetType);
            NavMeshPath path = new NavMeshPath();

            agent.CalculatePath(p.transform.position, path);
            return coef*(int)(PathLength(path));
        }
    }

    public override bool PostPerform()
    {
        Debug.Log(gameObject.name + " has reached pokemon to fight");
        //init fight with an available pokemon and remove both from availability
        bool fighting = GWorld.Instance.InitFight(target.GetComponent<Pokemon>());
        if (fighting)
        {
            Debug.Log(gameObject.name + " initiated a fight with " + target.name);
            GWorld.Instance.PokemonFree2Fighting(gameObject);
            return true;
        }
        else
        {
            Debug.Log(gameObject + " did not initiate a fight with " + target.name);
            target = null;
            return false; //could not secure fight
        }
    }


    public override bool PrePerform()
    {
        target = GWorld.Instance.GetClosestFreePokemon(gameObject, targetType).gameObject;
        if (target == null)
        {
            Debug.Log(gameObject.name + " is going towards " + target.name);
            return false;
        }
        Debug.Log(gameObject.name + " couldn't find food");
        return true;
    }

    public Pokemon.PokemonType GetTargetType()
    {
        return targetType;
    }

}
