using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Notes:
 * Unity doesn't allow you to change the value of a property being animated.
 * If you change the localscale value of an object in playmode it will be overwritten back to the animated value.
 * To work around this I created a Vector 3 variable to adjust the scale.
 * 
 * */

public class BaseObject : MonoBehaviour
{
    public float currentHeight;
    public float currentWidth;

    public int moveSpeed = 10;
    public int maxHeight = 5;

    Vector3 scale;

    public bool isWalking = false;
    public bool isHopping = false;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        scale = gameObject.transform.localScale;

        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        scale.Set(currentWidth, currentHeight, transform.localScale.z);

        gameObject.transform.localScale = scale;

        if (isWalking)
        {
            rb.velocity = (transform.forward * moveSpeed);
        }

        if (isHopping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = new Vector3(0, maxHeight, 0);
            }
        }
    }
}
