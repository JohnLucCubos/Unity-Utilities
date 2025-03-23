using UnityEngine;

public class Dialogue : ScriptableObject
{
    public string characterName;
    [TextArea(1,5)] public string sentences;
    public string audioSFX;
}