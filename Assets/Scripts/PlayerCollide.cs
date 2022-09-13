using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour
{
    [SerializeField] ParticleSystem explodeParticles;
    [SerializeField] ParticleSystem finishParticles;

    [SerializeField] AudioClip powerUpSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip explodeSound;

    PlayerMovement playerMovement;
    AudioSource audioSource;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {       
        switch (other.gameObject.tag)
        {
            case "Finish": 
                finishParticles.Play();
                audioSource.clip = successSound;
                audioSource.Play();
                break;            
            case "Respawn":
                audioSource.clip = explodeSound;
                audioSource.Play();
                explodeParticles.Play();
                playerMovement.enabled = false;
                Invoke("ReloadLevel", 2f);
                break;
        }
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
