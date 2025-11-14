/*******************************************************************************
 * Class: FeedAnimals
 * Purpose: To feed the animals though player action 
 * Component Of: Player
 * Author: Lily Lesser
 * Version 1.0
 ******************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;
public class FeedTheAnimals : MonoBehaviour
{
    [SerializeField] private GameObject[] foods;
    private float maxForce;
    private AudioSource audioSource;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxForce = 20;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnFeedInput(InputAction.CallbackContext ctx)
    {
        
        //Only feeds animals on start press.  Ignores ctx.proceed and ctx.cancel.
        if (ctx.started)
        {
            
            //Send name of button pressed to FeedAnimal
            SelectFood(ctx.control.name);
        }
    }
   
    private void FeedAnimal(int index, int foodCount, bool allFood)
    {
        Debug.Log($"Food Selected: {foods[index]}");

        Vector3 position = transform.position + new Vector3(0, 2, 0); //Sets position 2 meters above center of Player
        
        
        audioSource.Play(); // Plays the sound when food is thrown

        if (allFood)
        {
            //loop thru all food prefabs
            foreach (GameObject foodPrefab in foods)
            {
                for (int i = 0; i < foodCount; i++)
                {
                    GameObject foodInstance = Instantiate(foodPrefab, position, Quaternion.identity);
                    Rigidbody foodRB = foodInstance.GetComponent<Rigidbody>();
                    foodRB.AddForce(Vector3.forward * maxForce, ForceMode.Impulse);
                }
            }
        }

        for (int i = 0; i < foodCount;i++)
        {
            GameObject foodInstance = Instantiate(foods[index], position, Quaternion.identity);  //Adds food prefab to world
            Rigidbody foodRB = foodInstance.GetComponent<Rigidbody>();    //Set rigidbody of food
            foodRB.AddForce(Vector3.forward * maxForce, ForceMode.Impulse);  //Adds a forward impulse force to the                      
                                                                             //food's rigidbodyx
        }


    }

    private void SelectFood(string keyName)
    {
        
        switch (keyName)
        {
            case "z":
                FeedAnimal(0, 1, false);
                
                break;

            case "x":
                FeedAnimal(1, 1, false);
                break;
                
            case "c":
                FeedAnimal(2, 1, false);
                break;

            case "v":
                FeedAnimal(3, 1, false);
                break;

            case "b":
                FeedAnimal(4, 100, false);
                break;

            case "space":
                FeedAnimal(0, 1, true);
                break;

        }
    }

    

}
        
