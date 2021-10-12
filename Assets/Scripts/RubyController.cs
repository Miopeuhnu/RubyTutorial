using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    void Start()
    {
            //Framerate Adjustment
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    void Update()
    {
            //Horizontal and Vertical movement through input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(vertical);
        //Debug.Log(horizontal);
        Vector2 position = transform.position;
        position.x = position.x+3.0f*horizontal*Time.deltaTime;
        position.y = position.y+3.0f*vertical*Time.deltaTime;
        transform.position = position;
    }
}
