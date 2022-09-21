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
    bool isTransitioning = false;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Finish":
                    isTransitioning = true;
                    finishParticles.Play();
                    audioSource.Stop();
                    audioSource.PlayOneShot(successSound);
                    Debug.Log("Play finish");
                    break;
                case "Respawn":
                    isTransitioning = true;
                    explodeParticles.Play();
                    audioSource.Stop();
                    audioSource.PlayOneShot(explodeSound);
                    playerMovement.enabled = false;
                    Invoke("ReloadLevel", 2f);
                    break;
            }
        }        
    }    

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
