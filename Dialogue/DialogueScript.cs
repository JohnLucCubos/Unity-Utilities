using UnityEngine;
// fix to acomodate for new Dialogue system
[CreateAssetMenu(fileName = "DialogueScript", menuName = "DialogueScript", order = 0)]
public class DialogueScript : ScriptableObject 
{
    public Dialogue[] dialogue;
}