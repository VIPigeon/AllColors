using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "Main")
            QuestStates.Instance.PlayerPositionSave = FindObjectOfType<PlayerMovement>().transform.position;
        SceneManager.LoadScene(sceneName);
    }
}
