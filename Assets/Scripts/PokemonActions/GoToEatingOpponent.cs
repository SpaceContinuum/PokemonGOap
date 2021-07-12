using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GoToEatingOpponent : GAction
{
    protected Pokemon p;
    protected Pokemon.PokemonType myType;
    protected Pokemon.PokemonType myWeaknessType;
    protected Pokemon.PokemonType myStrengthType;
    protected Pokemon.PokemonType targetType;

    public override float cost
    {
        get
        {
            float coef;
            if (targetType == myType)
            {
                coef = 2;
            }
            else if (targetType == myStrengthType)
            {
                coef = 0.8f;
            }
            else
            {
                coef = 100;
            }

            Pokemon p = GWorld.Instance.GetClosestEatingPokemon(gameObject, targetType);
            NavMeshPath path = new NavMeshPath();

            if (p != null && path != null) {
                agent.CalculatePath(p.transform.position, path);

                //Debug.Log(gameObject.name + " cost to attack " + p.name +"-" + p.GetMyPokemonType()+ ": "+coef+"\nTargetType = " +targetType.ToString() );
                return coef*(PathLength(path));
            }
            return Mathf.Infinity;
        }
    }

    public override bool PostPerform()
    {
        Pokemon p = gameObject.GetComponent<Pokemon>();
        Pokemon p_target= target.GetComponent<Pokemon>();
        GameObject obj_food = p_target.inventory.FindItemWithTag("Food");

        if (obj_food == null || target == null)
        {
            return false;
        }

        Food f = obj_food.GetComponent<Food>();
        Debug.Log(gameObject.name + " has reached pokemon to fight");
        
        if (f == null || f.owner == null)
        {
            return false;
        }

        //init fight with an available pokemon and remove both from availability
        Debug.Log(gameObject.name + " initiated a fight with " + target.name);
        GWorld.Instance.PokemonFree2Fighting(gameObject);

        
        if (p_target == null || p_target.GetOpponent() != null || p.GetOpponent() != null)
        {
            Debug.Log(name + " trying to attack a non-pokemon " + target.name);
            target = null;
            return false;
        }
        //Declare attack on opponent
        p_target.SetOpponent(p);
        Debug.Log(gameObject.name + " attacking " + p_target.name + " for " + target.name);
        
        // TODO: consider removing this interrupt
        p_target.Interrupt();
        p_target.SetDefence();
        p_target.beliefs.ModifyState("isDefensive", 1);
        p_target.inventory.AddItem(gameObject);
        GWorld.Instance.PokemonEating2Fighting(target);

        p.SetOpponent(p_target);
        beliefs.ModifyState("attacking", 1);
        beliefs.ModifyState("attackingForFood", 1);
        inventory.AddItem(p_target.gameObject);

        return true;

    }


    public override bool PrePerform()
    {
        if (gameObject != null && targetType != Pokemon.PokemonType.NULL)
        {
            target = GWorld.Instance.GetClosestEatingPokemon(gameObject, targetType).gameObject;
            if (target != null)
            {
                GAction gAction = target.GetComponent<GAgent>().currentAction;
                if (gAction && gAction.actionName == "ConsumeFood")
                {
                    Debug.Log(gameObject.name + " is going towards " + target.name);
                    return true;
                }
            }
        }

        Debug.Log(gameObject.name + " couldn't find opponent");
        return false;
    }

    public override void Reset()
    {
        actionName = "GoToEatingOpponent";
        Debug.Log("Reset in GoToEatingOpponent");
    }

    public Pokemon.PokemonType GetTargetType()
    {
        return targetType;
    }
}
