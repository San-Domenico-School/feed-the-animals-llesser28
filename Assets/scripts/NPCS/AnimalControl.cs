using UnityEngine;

public class AnimalControl : MonoBehaviour
{
    private float lowerBound;
    [SerializeField] private float playerspeed;
    [SerializeField] GameObject fooditeats;
    private bool notHungry;
    public float GetPlayerSpeed() => playerspeed;
    public void SetPlayerSpeed(float value) => playerspeed = value;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lowerBound = 35.6f;
    }

    // Update is called once per frame
    void Update()
    {
        DeleteOutOfScene();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * playerspeed * Time.deltaTime);
    }

    private void DeleteOutOfScene()
    {
        if (transform.position.z < 35.6f)
        {
            MoveForward();
        }

        else
        {
            Destroy(gameObject);
        }

    }

    private bool IsFoodItEats(string foodTriggered)
    {
        string foodItEatsName = fooditeats.name;

        // Remove "(Clone)" if it exists
        int cloneIndex = foodTriggered.IndexOf("(Clone)");
        if (cloneIndex != -1)
        {
            foodTriggered = foodTriggered.Substring(0, cloneIndex).Trim();
        }

        //compare the cleaned names
        return foodTriggered.Equals(foodItEatsName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food") && !notHungry)
        {
            if (IsFoodItEats(other.name))
            {
                ScoreKeeper.Instance.UpdateScore(10);
            }

            else
            {
                ScoreKeeper.Instance.UpdateScore(-10);
            }

        }

            notHungry = true;
            Destroy(gameObject);


        }
    }
