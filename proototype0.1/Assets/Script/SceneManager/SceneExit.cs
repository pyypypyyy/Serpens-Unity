using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionToNextScene();
        }
    }

    public void TransitionToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int nextSceneIndex = currentSceneIndex + 1; 
      
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); 
        }
        else
        {
            Debug.Log("Already the last scene, there are no more scenes to be loaded£¡");
        }
    }
}