using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBase : MonoBehaviour
{
    // Start is called before the first frame update
    public ConfigData config;
    public GameController gameController;

    protected void Start() {
        gameController = FindObjectOfType<GameController>();
        config = gameController.config;
    }
}
