using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to move the player
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player to the right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        }
        // Move the player to the left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-3, 0, 0) * Time.deltaTime;
        }
    }
}