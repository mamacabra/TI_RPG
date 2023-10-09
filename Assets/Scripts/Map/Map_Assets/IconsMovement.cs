using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsMovement : MonoBehaviour
{
    float initialPos;
    bool up;
    float speed = 2;
    void Start()
    {
        initialPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 5 * Time.deltaTime, 0);

        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);      
        

        if(transform.position.y >= initialPos + 0.2f)
        {
            speed = -0.1f;
        }
        if(transform.position.y <= initialPos - 0.2f)
        {
            speed = 0.1f;
        }
        
    }
}
