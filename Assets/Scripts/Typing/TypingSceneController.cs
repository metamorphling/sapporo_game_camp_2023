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

    [SerializeField] TMPro.TMP_Text timer;

    IEnumerator Start()
    {
        yield return null;
        yield return GameLoop_Pattern1();
    }

    IEnumerator GameLoop_Pattern1()
    {
        //var word1 = wordFactory.CreateDefaultTypingWord(library.RandomWord(), Vector3.zero);
        bool defeated = false;
        bool timeOver = false;
        word1.OnDefeated += () => defeated = true;
        word1.OnTimeOverCallBack += () => timeOver = true;
        word1.OnTimerChangedCallBack += time => timer.text = string.Format("{0:F0}",time);
        playerTyping.SetTypingWord(word1);

        while (true)
        {
            word1.SetWord(library.RandomWord());
            word1.SetTimer(2);

            word1.WordSpawn();

            yield return new WaitUntil(() => defeated|timeOver);

            if (timeOver)
            {
                //ダメージ処理
                print("TimeOver");
            }

            defeated = false;
            timeOver = false;
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
