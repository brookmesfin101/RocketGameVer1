using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustFactor = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip thrustSound2;
    [SerializeField] ButtonLogic boost;
    [SerializeField] ButtonLogic leftButton;
    [SerializeField] ButtonLogic rightButton;

    AudioSource _audioSource;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {        
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
        float horizontal = leftButton.buttonPressed ? -1 : rightButton.buttonPressed ? 1 : 0;
        var rotation = new Vector3(horizontal, 0, 0);
        transform.Rotate(rotation * Time.deltaTime * rotateSpeed);
        LeftRightParticlesControl(horizontal);
    }

    private void LeftRightParticlesControl(float horizontal)
    {                
        if (leftButton.buttonPressed)
        {
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
            if (leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Stop();
            }
            PlayThrustSound();
        }
        else if (rightButton.buttonPressed)
        {
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
            if (rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Stop();
            }
            PlayThrustSound();
        }
        else
        {
            if (!boost.buttonPressed && _audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            rightThrustParticles.Stop();
            leftThrustParticles.Stop();            
        }
    }

    private void PlayThrustSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(thrustSound);
        }
    }

    private void ProcessThrust()
    {
        if (boost.buttonPressed)
        {            
            if (!thrustParticles.isPlaying)
            {
                thrustParticles.Play();                
            }

            PlayThrustSound();

            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustFactor);
        } 
        else
        {
            thrustParticles.Stop();
        }
    }
}
