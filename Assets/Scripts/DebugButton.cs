using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
    Button button;
    Text buttonText;
    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();
        
        buttonText.text = button.onClick.GetPersistentTarget(0).name; //set the button to the name of the target
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
