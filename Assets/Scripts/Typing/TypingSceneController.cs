using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class TypingSceneController : MonoBehaviour
{
    [SerializeField] PlayerTyping playerTyping;
    //[SerializeField] TypingWordFactory wordFactory;
    [SerializeField] NewBehaviourScript library;
    [SerializeField] DefaultTypingWord word1;

    IEnumerator Start()
    {
        yield return null;
        yield return GameLoop_Pattern1();
    }

    IEnumerator GameLoop_Pattern1()
    {
        //var word1 = wordFactory.CreateDefaultTypingWord(library.RandomWord(), Vector3.zero);
        bool defeated = false;

        while (true)
        {
            word1.SetWord(library.RandomWord());
            playerTyping.SetTypingWord(word1);
            word1.WordSpawn();
            word1.OnDefeated += () => defeated = true;

            yield return new WaitUntil(() => defeated);

            defeated = false;
        }
    }

    /// <summary>
    /// プレイヤーのタイピングを止める
    /// </summary>
    public void StopPlayerTyping()
    {
        playerTyping.StopTypingInput();
    }

    /// <summary>
    /// プレイヤーのタイピングをリスタートする
    /// </summary>
    public void ReStartPlayerTyping()
    {
        playerTyping.ReStartTypingInput();
    }
}
