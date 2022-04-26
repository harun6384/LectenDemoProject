using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;
    public Vector3 distance;
    [SerializeField] private float followSpeed;

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, ball.transform.position + distance, followSpeed);
    }
}
