/******************
 * Lily Lessor
******************/

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovementController : MonoBehaviour
{
    
    [SerializeField] private float playerSpeed;
    private float moveDirection;
    private float centerToEdge;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerToEdge = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        DeterminesPlayerDirection(ctx.ReadValue<Vector2>());
    }


    
    private void DeterminesPlayerDirection(Vector2 value)
    {
        moveDirection = value.x;
    }

    //Process movement logic
    private void PlayerMovement()
    {
        if(transform.position.x < centerToEdge && moveDirection > 0 || transform.position.x > -centerToEdge && moveDirection < 0)


        {
            transform.Translate(Vector3.right * playerSpeed * moveDirection * Time.deltaTime);
        }

    }

}