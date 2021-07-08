using UnityEngine;

[RequireComponent(typeof(Fight))]
public class PokemonTrained : Pokemon
{

    public void SetViolence()
    {
        beliefs.ModifyState("isViolent", 0);
        Debug.Log(gameObject.name + " is violent");

    }

}