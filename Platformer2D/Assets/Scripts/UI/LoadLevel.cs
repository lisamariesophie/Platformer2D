using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadLevel : MonoBehaviour // Transition Loading Screen between Scenes
{
    public Animator transition;
    public TextMeshProUGUI loadText;
    public float transitionTime = 1;

    public void LoadNextLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex + 1) > 2)
        {
            StartCoroutine(Load(0));
        }
        else
        {
            StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    private IEnumerator Load(int i)
    {
        if (i == 0)
        {
            loadText.text = "Loading Main Menu...";
        }
        else
        {
            loadText.text = "Loading Level " + i + "...";
        }
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(i);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
