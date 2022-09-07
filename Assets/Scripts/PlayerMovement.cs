using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustFactor = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] ParticleSystem thrustParticles;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        var rotation = new Vector3(horizontal, 0, 0);
        transform.Rotate(rotation * Time.deltaTime * rotateSpeed);
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustFactor);

            if (!thrustParticles.isPlaying)
            {
                thrustParticles.Play();
            }
        } 
        else
        {
            thrustParticles.Stop();
        }
    }
}
