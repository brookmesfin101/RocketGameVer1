using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustFactor = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] AudioClip thrustSound;
    AudioSource audioSource;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        LeftRightParticlesControl(horizontal);
    }

    private void LeftRightParticlesControl(float horizontal)
    {
        if (horizontal < 0)
        {
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
            if (leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Stop();
            }
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (horizontal > 0)
        {
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
            if (rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Stop();
            }
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
            audioSource.Stop();
        }
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustSound);
            }
        } 
        else
        {
            thrustParticles.Stop();
            audioSource.Stop();
        }
    }
}
