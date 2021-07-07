using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text freeFoodCounter;
    [SerializeField] Text eatenFoodCounter;
    [SerializeField] Text UITextWorldStates;

    // Update is called once per frame
    void Update()
    {
     /*   freeFoodCounter.text = GWorld.Instance.freeFoodCount().ToString();
        eatenFoodCounter.text = GWorld.Instance.eatenFoodCount().ToString();*/
        UITextWorldStates.text = GWorld.Instance.GetWorld().ToString();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
}
