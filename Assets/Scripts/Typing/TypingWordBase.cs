using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TypingWordBase : MonoBehaviour
{
    public System.Action OnDefeated;
    public abstract int GetRemainingCharacterCount();
    public abstract int GetAllCharacterCount();
    public abstract string GetWord();
    public abstract char GetNextCharacter();
    public abstract void SendTypingCharacter(char character, out bool isMatched);
}
