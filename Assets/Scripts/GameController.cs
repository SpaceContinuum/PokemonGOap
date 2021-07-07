using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public ConfigData config;

   

    public void ResetLevel() {
        SceneManager.LoadScene("PokemonGOAP");
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }
 
}
