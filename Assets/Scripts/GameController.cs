using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ConfigData config;

    [SerializeField] Text freeFoodCounter;

    public void ResetLevel() {
        SceneManager.LoadScene("PokemonGOAP");
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        freeFoodCounter.text = GWorld.Instance.freeFoodCount().ToString();
    }
}
