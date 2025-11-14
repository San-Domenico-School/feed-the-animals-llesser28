using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] animalPrefab;
    public int animalsToFeed;
    private float zStart;
    private float xSpawnRange;
    private float startDelay = 2f;
    private float repeatRate = 1.5f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zStart = 35f;
        xSpawnRange = 10f;

       ScoreKeeper.Instance.UpdateRemaining(animalsToFeed);



        InvokeRepeating("SpawnAnimal", startDelay, repeatRate);

    }

    private void SpawnAnimal()
    {
        int choice = Random.Range(0, animalPrefab.Length);

        float xPosition = Random.Range(-xSpawnRange, xSpawnRange);

        Instantiate(animalPrefab[choice], new Vector3(xPosition, 0, zStart), Quaternion.Euler(0, 180, 0));
        animalsToFeed--;

        if (animalsToFeed >= 0)
        {
            ScoreKeeper.Instance.UpdateRemaining(animalsToFeed);
        }
        else
        {
            GameOver();
        }


    } 

    private void GameOver()
    {

        CancelInvoke();
    }
   
}
