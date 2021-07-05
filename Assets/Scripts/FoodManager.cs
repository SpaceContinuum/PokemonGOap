using UnityEngine;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour {

    // Grab our prefab
    public GameObject foodPrefab;
    // Number of patients to spawn
    public int numPokemon = 5;

    //private int foodCounter = 1;

    //private int foodCounter = 1;
    //[SerializeField]
    //private List<Food> foodList; //where all the instances of food in the game are stored.


    public static FoodManager Instance { get; private set; } //singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start() {

        //Invoke("SpawnFood", 5.0f);
    }

   public void SpawnFood() {
       //int xSize = Screen.width;
       //int ySize = Screen.height;

        Vector2 spawnPosition = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f));
        /*
        if (numPokemon - 1 > GWorld.Instance.FoodCounter())
        {
            // Instantiate new food
            GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            GWorld.Instance.AddNewFood(newFood);

        }
        */
        // Invoke this method at random intervals
        //Invoke("SpawnFood", Random.Range(2.0f, 10.0f));
        //Instantiate new food
        GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        GWorld.Instance.AddNewFood(newFood);
    }

    // Update is called once per frame
    void Update()
    {

    }

   
}
