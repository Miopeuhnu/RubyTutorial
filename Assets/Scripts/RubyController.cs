using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    void Start()
    {
            //Framerate Adjustment
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
            //Creates Rigidbody components
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            //Horizontal and Vertical movement through input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Debug.Log(vertical);
        //Debug.Log(horizontal);
        
    }
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
