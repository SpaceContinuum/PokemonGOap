using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>
    /// debugging class created for showing on screen data about the players.
    /// </summary>
public class DebugText : MonoBehaviour
{

    private GameController gameController;
    private ConfigData gameConfig;
    public float velocity = 0f;
    public float force = 0f;

    private float hideTime;

    public TextAnchor textPosition;
    // Start is called before the first frame update
    Text textBox;
    // Update is called once per frame
    void Start() {
        textBox = GetComponent<Text>();
        gameController = FindObjectOfType<GameController>();
        gameConfig = gameController.config;

    }
    
    public void Say(string message) {
        hideTime = Time.time+gameConfig.DebugTextDuration;
        textBox.text = message;
    }

    void FixedUpdate()
    {
        textBox.alignment = textPosition;
        if (Time.time > hideTime) {
            
        }
    }
}
