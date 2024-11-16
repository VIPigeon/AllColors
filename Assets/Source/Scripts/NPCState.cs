using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : Singleton<NPCState>
{
    public Dictionary<CharacterID, CharacterState> States;

    private void OnEnable()
    {
        if (States == null)
            States = new Dictionary<CharacterID, CharacterState>();
    }
}


public enum CharacterID
{
    testNPC1,
    testNPC2,
    testNPC3,
}

public enum CharacterState
{
    Normal,
    Defeated
}
