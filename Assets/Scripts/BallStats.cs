using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallStats : MonoBehaviour
{
    Vector3 velocity;   
    Rigidbody _rigidbody;
    
    [SerializeField] private float speed = 25;
    [SerializeField] Transform _basket;
    [SerializeField] private float initialAngle;
    [SerializeField] private float shootForce;

    private bool canShoot = true;
    private bool absScore = false;
    public Vector3 startPos;
    private int score = 0;
    [SerializeField] private TMP_Text _score;
    public float forcePower = 500;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        startPos = transform.position;
        _score.text = score.ToString();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            _rigidbody.AddForce(velocity * Time.deltaTime * speed, ForceMode.Force);

        }
    }

    void OnMove(InputValue movementInput)
    {
        Vector2 input = movementInput.Get<Vector2>();
        velocity.x = input.x;
        velocity.z = input.y;
    }
    void OnShoot(InputValue shootInput)
    {
        if (canShoot == true && absScore == false)
        {
            transform.LookAt(_basket);
            _rigidbody.AddForce(Vector3.forward * shootForce, ForceMode.VelocityChange);
            _rigidbody.AddForce(Vector3.up * shootForce, ForceMode.VelocityChange);
            canShoot = false;
        }

        if (absScore == true)
        {
            hesapla();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rigidbody.velocity = _rigidbody.velocity/2.79f;
            _rigidbody.AddForce(Vector3.up * forcePower, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AbsScore"))
        {
            absScore = true;
        }
        
        if (other.gameObject.CompareTag("Ground"))
        {
            
            canShoot = true;
            
        }
        if (other.gameObject.CompareTag("Reset"))
        {
            transform.position = startPos;
        }
        if (other.gameObject.CompareTag("Score"))
        {
            score++;
            _score.text = score.ToString();
            transform.position = startPos;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AbsScore"))
        {
            absScore = false;
        }
    }
    void hesapla()
    {
        if (canShoot == true)
        {
            Vector3 p = _basket.position;

            float gravity = Physics.gravity.magnitude;
            float angle = initialAngle * Mathf.Deg2Rad;

            Vector3 planarBall = new Vector3(p.x, 0, p.z);
            Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);

            float distance = Vector3.Distance(planarBall, planarPosition);
            float yOffSet = transform.position.y - p.y;

            float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2))
                / (distance * Mathf.Tan(angle) + yOffSet));

            Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

            float m;
            if (p.x > transform.position.x)
            {
                m = 1f;
            }
            else
            {
                m = -1f;
            }

            float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarBall - planarPosition) * m;
            Mathf.Abs(angleBetweenObjects);
            Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

            _rigidbody.velocity = finalVelocity;
            canShoot = false;
        }
    }
}
