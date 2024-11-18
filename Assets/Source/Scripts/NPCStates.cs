using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStates : Singleton<NPCStates>
{
    public Dictionary<CharacterID, CharacterState> States;

    private void OnEnable()
    {
        if (States == null)
            States = new Dictionary<CharacterID, CharacterState>();
        foreach (CharacterID character in Enum.GetValues(typeof(CharacterID))) {
            States[character] = CharacterState.Normal;
        }
    }
}


public enum CharacterID
{
    cave_OldMan,
    Big_Red,
    Blue_Small1,
}

public enum CharacterState
{
    Normal,
    Defeated
}
