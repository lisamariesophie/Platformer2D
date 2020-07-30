using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found.");
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Respawn()
    {
        PlaySound("GameOver");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void KillPlayer(PlayerController player)
    {
        Destroy(player.gameObject);
        instance.StartCoroutine(instance.Respawn());
    }

    public void PlaySound(string soundName){
        audioManager.PlaySound(soundName);
    }

    
}
