using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMemberMovement : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float speed;

    private float targetX;
    private float distanceToPlayer;

    private GameObject player;
    private bool isStopped;
    private bool isMovingRight;

    void Start()
    {
        targetX = Random.Range(minX, maxX);
        player = GameObject.FindGameObjectWithTag("Player");
        isMovingRight = Random.value > 0.5f;
    }

    void Update()
    {
        // If the object is not stopped, move it towards the target position
        if (!isStopped)
        {
            // Choose the direction of movement based on the current target position and the wall collisions
            float direction = isMovingRight ? 1f : -1f;
            if (direction == 1f && transform.position.x + 0.5f >= maxX || direction == -1f && transform.position.x - 0.5f <= minX)
            {
                // If the object would move beyond the given range, reverse the direction
                isMovingRight = !isMovingRight;
                direction = isMovingRight ? 1f : -1f;
            }

            // Move the object in the chosen direction
            transform.position += new Vector3(speed * direction * Time.deltaTime, 0f, 0f);
        }

        // If the object has reached the target position, generate a new random target position within the given range
        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            targetX = Random.Range(minX, maxX);
        }

        // Check if the object is too close to the player
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 1.0f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Stop the object if the "E" key is pressed while the player is colliding with it
                isStopped = true;
            }
            else
            {
                // Generate a new random target position within the given range if the object is too close to the player
                float newTargetX = Random.Range(minX, maxX);

                // Check if the new target position would result in a collision with the player
                float playerWidth = player.GetComponent<Renderer>().bounds.size.x;
                if (newTargetX > player.transform.position.x - playerWidth && newTargetX < player.transform.position.x + playerWidth)
                {
                    // If a collision would occur, choose a new target position on the opposite side of the player
                    if (targetX < player.transform.position.x)
                    {
                        newTargetX = player.transform.position.x + playerWidth + Random.Range(0.1f, 1.0f);
                    }
                    else
                    {
                        newTargetX = player.transform.position.x - playerWidth - Random.Range(0.1f, 1.0f);
                    }
                }

                // Set the new target position
                targetX = newTargetX;
            }
        }
        else
        {
            // Resume movement if the player is not colliding with the object
            isStopped = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the object collides with a wall, reverse the direction of movement
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
        }
    }
}
