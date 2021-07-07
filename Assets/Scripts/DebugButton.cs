using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
    Button button;
    Dropdown stateDropdown;
    Text buttonText;

    public Pokemon pokemon;
    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();
        
        stateDropdown = FindObjectOfType<Dropdown>();

        buttonText.text = pokemon.name; //set the button to the name of the target

        GetComponent<Button>().onClick.AddListener(TriggerState);

    }

    // Update is called once per frame
    void TriggerState() {
        int requestedState = stateDropdown.value;
        string msg = pokemon.name;
        switch(requestedState) {
            case 0:
                pokemon.GetHungry();
                msg+=": triggering hunger";
                break;
            case 1:
                if (pokemon.GetComponent<PokemonTrained>() != null) {
                    pokemon.GetComponent<PokemonTrained>().SetViolence();
                    msg+=": triggering violence";
                }
                else msg+=": not a trained pokemon";
                break;
            case 2:
                pokemon.SetStun(true);
                msg+=": setting stun";
                break;
            default:
                msg+=": Unknown state requested";
                break;
        }
        Debug.Log(msg+", button value: "+requestedState);
    }
}
