using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTypingWord : TypingWordBase
{
    [SerializeField] TMPro.TMP_Text text;

    string defaultText;

    private void Start()
    {
        if(text == null)
        {
            Destroy(gameObject);
        }

        defaultText = text.text;    
    }
    public override int GetAllCharacterCount()
    {
        return text.maxVisibleCharacters;
    }

    public override char GetNextCharacter()
    {
        if (text == null || text.text == null || text.text.Length == 0)
        {
            return default;
        }
        return text.text[text.firstVisibleCharacter];
    }

    public override int GetRemainingCharacterCount()
    {
        throw new System.NotImplementedException();
    }

    public override string GetWord()
    {
        return defaultText;
    }

    public override void SendTypingCharacter(char character, out bool isMatched)
    {
        if (text == null || text.text == null || text.text.Length == 0)
        {
            isMatched = false;
            return;
        }

        isMatched = character == text.text.ToLower()[text.firstVisibleCharacter];

        if (isMatched)
        {
            text.text = text.text.Remove(text.firstVisibleCharacter, 1);
        }
    }
}
