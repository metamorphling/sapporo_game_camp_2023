using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTypingWord : TypingWordBase
{
    [SerializeField] TMPro.TMP_Text text;

    string defaultText;
    int textCount;

    private void Start()
    {
        if(text == null)
        {
            Destroy(gameObject);
        }

        defaultText = text.text;    
    }

    public event System.Action<float> OnTimerChangedCallBack;
    public override int GetAllCharacterCount()
    {
        return defaultText.Length;
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
        if (text == null || text.text == null || text.text.Length == 0)
        {
            return -1;
        }
        return text.maxVisibleCharacters;
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
            textCount--;

            if (textCount == 0)
            {
                OnDefeated?.Invoke();
            }
        }
    }

    public override void SetWord(string word)
    {
        text.text = word;
        textCount = word.Length;
    }

    public void WordSpawn()
    {
        text.maxVisibleCharacters = 0;

        StartCoroutine(WordSpawnAnimation());

        IEnumerator WordSpawnAnimation()
        {
            for(int i=0;i<text.text.Length;i++)
            {
                text.maxVisibleCharacters++;

                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
