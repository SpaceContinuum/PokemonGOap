using System;
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
        string label = stateDropdown.GetComponentInChildren<Text>().text;
        WorldState.Label requestedState;

        if (!Enum.TryParse<WorldState.Label>(label, out requestedState)) {
            Debug.Log(gameObject.name+ ": Cannot parse requested state "+label);
            return;
        }
        
        string msg = pokemon.name;
        pokemon.beliefs.ModifyState(requestedState,0);
        Debug.Log(pokemon.name+": applying state "+requestedState);
    }
}
