using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;
    public Vector3 distance;
    
    

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 ballPosition = new Vector3(ball.transform.position.x, 8f, ball.transform.position.z - 7f);
        this.transform.position = ballPosition;
    }
}
