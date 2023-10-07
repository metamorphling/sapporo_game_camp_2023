using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class TypingSceneController : MonoBehaviour
{
    public PlayerTyping playerTyping;
    [SerializeField] NewBehaviourScript library;
    public DefaultTypingWord word1;

    [SerializeField] TMPro.TMP_Text timer;
    [SerializeField] BugWatcher bugWatcher;

    public TypingSceneController Main {  get; private set; }

    int bugShowingCount = 0;

    IEnumerator Start()
    {
        if(Main == null)
        {
            Main = this;
        }
        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(Bug());

        yield return null;
        yield return GameLoop_Pattern1();

        IEnumerator Bug()
        {
            int tmp = bugWatcher.bugShowingCount;
            while(true)
            {
                yield return null;

                if (bugWatcher.bugShowingCount == 0 & tmp > 0)
                {
                    ReStartPlayerTyping();
                    //word1.Restart();
                }

                if (bugWatcher.bugShowingCount > 0 & tmp == 0)
                {
                    StopPlayerTyping();
                    //word1.Stop();
                }

                tmp = bugWatcher.bugShowingCount;
            }
        }
    }

    IEnumerator GameLoop_Pattern1()
    {
        //var word1 = wordFactory.CreateDefaultTypingWord(library.RandomWord(), Vector3.zero);
        bool defeated = false;
        bool timeOver = false;
        word1.OnDefeated += () => defeated = true;
        word1.OnTimeOverCallBack += () => timeOver = true;
        word1.OnTimerChangedCallBack += time => timer.text = string.Format("{0:F00}",time);
        playerTyping.SetTypingWord(word1);

        while (true)
        {
            var word = library.RandomWord();

            word1.SetWord(word);
            word1.SetTimer(GetTimer(word));

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

    float GetTimer(string word)
    {
        return word.Length * 0.75f + 5;
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

    public string GetRamdamWord()
    {
        return library.RandomWord();    
    }
}
