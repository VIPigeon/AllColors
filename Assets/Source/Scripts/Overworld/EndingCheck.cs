using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCheck : MonoBehaviour
{
    [SerializeField] private CharacterID[] _ids; 
    [SerializeField] private string _endingSceneName; 


    void Update()
    {
        foreach (CharacterID id in _ids)
            if (NPCStates.Instance.States[id] != CharacterState.Defeated)
                return;
        FindObjectOfType<SceneLoader>().GoToScene(_endingSceneName);
    }
}
