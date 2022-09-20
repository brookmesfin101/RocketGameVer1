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
    bool playerEnabled = true;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (playerEnabled)
        {
            switch (other.gameObject.tag)
            {
                case "Finish":
                    finishParticles.Play();
                    audioSource.Stop();
                    Debug.Log("FINISH");
                    Debug.Log(audioSource.isPlaying);
                    audioSource.PlayOneShot(successSound);
                    playerEnabled = false;
                    break;
                case "Respawn":
                    explodeParticles.Play();
                    audioSource.Stop();
                    audioSource.PlayOneShot(explodeSound);
                    playerMovement.enabled = false;
                    Invoke("ReloadLevel", 2f);
                    playerEnabled = false;
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
