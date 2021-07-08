using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GoToEatingOpponent : GAction
{
    protected Pokemon p;
    protected Pokemon.PokemonType myType;
    protected Pokemon.PokemonType weaknessType;
    protected Pokemon.PokemonType strengthType;
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
            else if (targetType == strengthType)
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

                //Debug.Log(gameObject.name + " cost to attack " + p.name +"-" + p.GetPokemonType()+ ": "+coef+"\nTargetType = " +targetType.ToString() );
                return coef*(PathLength(path));
            }
            return Mathf.Infinity;
        }
    }

    public override bool PostPerform()
    {
        Pokemon p = gameObject.GetComponent<Pokemon>();
        Food f = target.GetComponent<Food>();
        Debug.Log(gameObject.name + " has reached pokemon to fight");
        
        if (target == null || f == null || f.owner == null)
        {
            return false;
        }

        //init fight with an available pokemon and remove both from availability
        GameObject owner = f.owner;
        Debug.Log(gameObject.name + " initiated a fight with " + target.name);
        GWorld.Instance.PokemonFree2Fighting(gameObject);

        Pokemon other = owner.GetComponent<Pokemon>();
        if (other == null || other.GetOpponent() != null || p.GetOpponent() != null)
        {
            Debug.Log(name + " trying to attack a non-pokemon " + owner.name);
            target = null;
            return false;
        }
        //Declare attack on opponent
        other.SetOpponent(p);
        Debug.Log(gameObject.name + " attacking " + other.name + " for " + target.name);
        other.beliefs.ModifyState("isDefensive", 1);
        other.Interrupt();
        other.inventory.AddItem(gameObject);
        GWorld.Instance.PokemonEating2Fighting(owner);

        p.SetOpponent(other);
        beliefs.ModifyState("attacking", 1);
        beliefs.ModifyState("attackingForFood", 1);
        inventory.AddItem(other.gameObject);

        return true;

    }


    public override bool PrePerform()
    {
        target = GWorld.Instance.GetClosestEatingPokemon(gameObject, targetType).gameObject;
        if (target == null)
        {
            Debug.Log(gameObject.name + " couldn't find opponent");
            return false;
        }
        Debug.Log(gameObject.name + " is going towards " + target.name);
        
        return true;
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
