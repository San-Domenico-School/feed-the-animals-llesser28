/*******************************************************************************
 * Class: FeedAnimals
 * Purpose: To feed the animals though player action 
 * Component Of: Player
 * Author: Lily Lesser
 * Version 1.0
 ******************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FeedTheAnimals : MonoBehaviour
{
    [SerializeField] private GameObject[] foods;
    private float maxForce;
    private AudioSource audioSource;
    [SerializeField] private PlayerMovementController playerMovement;
    private float normalSpeed;
    private float boostedSpeed;
    [SerializeField] private float playerSpeed;


    // Z-hold state
    private bool zPressed = false;
    private bool zHeld = false;
    private Coroutine zHoldCoroutine = null;
    [SerializeField] private float holdThreshold = 0.20f; // seconds to consider a hold (adjustable)

    void Start()
    {
        maxForce = 20;
        audioSource = GetComponent<AudioSource>();

        // assumes PlayerMovementController exposes GetPlayerSpeed() and SetPlayerSpeed(...)
        normalSpeed = playerMovement.GetPlayerSpeed();
        boostedSpeed = normalSpeed * 2f;
    }

    public void OnFeedInput(InputAction.CallbackContext ctx)
    {
        string key = ctx.control.name;

        // --- Special handling for Z (we want tap = throw, hold = speed boost) ---
        if (key == "z")
        {
            if (ctx.started)
            {
                
            }

            if (ctx.canceled)
            {
                if (!zHeld)
                {
                    SelectFood("z");
                }

                if (!zHeld)
                {
                    // It was a short tap -> throw food (preserves your original behavior)
                    SelectFood("z");
                }

                playerMovement.playerSpeed = normalSpeed;
                zHeld = false;

            }

            // Return here so we don't fall-through and double-handle a started/performed elsewhere
            return;
        }

        // --- All other keys behave as before: throw on started only ---
        if (ctx.started)
        {
            SelectFood(key);
        }
    }

    private IEnumerator ZHoldCheck()
    {
        // Wait the threshold; if still pressed after this, treat as hold
        yield return new WaitForSeconds(holdThreshold);

        if (zPressed)
        {
            zHeld = true;
            playerMovement.SetPlayerSpeed(boostedSpeed);
            // do NOT call SelectFood — holding should NOT throw
        }

        zHoldCoroutine = null;
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

        for (int i = 0; i < foodCount; i++)
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


