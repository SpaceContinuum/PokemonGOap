using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private FoodManager spawner;
    public float destroyTimer = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<FoodManager>();
        //Invoke("DestroyFood", destroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyFood()
    {
        spawner.RemoveFood();
        Destroy(gameObject);
        
    }

}
