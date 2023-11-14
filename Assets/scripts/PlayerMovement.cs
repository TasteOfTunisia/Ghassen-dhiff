using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float gravity = -20;
    public float jumpForce;
    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between tow lanes

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        direction.z = forwardSpeed;
        direction.y += gravity * Time.deltaTime;
        if (controller.isGrounded) { 
        if (SwipeManager.swipeUp)
        {
            Jump();
        }
        }

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        transform.position = Vector3.Lerp(transform.position , targetPosition , 300 * Time.deltaTime);
        controller.center=controller.center;


    }
    private void FixedUpdate()
    {
        if(!PlayerManager.isGameStarted)
        return;
        controller.Move(direction * Time.fixedDeltaTime); 
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
            if(hit.transform.tag=="Obstacle")
            {

                PlayerManager.gameOver = true;

            }



    }
}
