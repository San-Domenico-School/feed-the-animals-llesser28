using System.Collections;
using UnityEngine;

public class DestroyFood : MonoBehaviour
{
    private float secondsInScene = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(secondsInScene);

        Destroy(gameObject);
    }

   

}
