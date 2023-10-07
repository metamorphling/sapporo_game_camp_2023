using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingWordFactory : MonoBehaviour
{
    [SerializeField] Transform TypingWordPool;

    public DefaultTypingWord CreateDefaultTypingWord(string word,Vector3 position)
    {
        if (string.IsNullOrEmpty(word)) { return null; }

        var typingWordPref = Resources.Load<DefaultTypingWord>("TypingWord\\DefaultTypingWord");

        var typingWord = Instantiate(typingWordPref, position, Quaternion.identity);

        typingWord.SetWord(word);

        return typingWord;
    }
}
