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
    [SerializeField] TMPro.TMP_Text scoreUI;
    [SerializeField]TMPro.TMP_Text hpUI;
    [SerializeField] BugWatcher bugWatcher;

    int _score = 0;

    public int Score
    {
        get { return _score; }
        set { 
            _score = value;
            scoreUI.text = "Score:" + _score.ToString();
        }
    }

    public int _hp = 100;

    public int HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            hpUI.text = "HP:" + _hp.ToString();

            if (_hp <= 0)
            {
                OnHPZero();
            }
        }
    }

    public TypingSceneController Main {  get; private set; }

    public GameLevel GameLevel = GameLevel.Normal;
    [SerializeField] bool useGlobalGameLevel = true;

    int bugShowingCount = 0;

    public static GameLevel GlobalGameLevel {  get; set; }=GameLevel.Normal;

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
        bugWatcher.OnDefeatBug += (score) =>
        {
            Score += score;
        };

        hpUI.text = "HP:" + _hp.ToString();
        scoreUI.text = "Score:" + _score.ToString();

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
        word1.OnDefeated += () =>
        {
            Score += word1.GetAllCharacterCount() * 20;
        };
        word1.OnTimeOverCallBack += () => timeOver = true;
        word1.OnTimerChangedCallBack += time => timer.text = "Timer:" + string.Format("{0:F00}",time);
        playerTyping.SetTypingWord(word1);
        playerTyping.OnPlayerMissTypedCallBack += () =>
        {
            HP -= 1;
        };

        playerTyping.OnPlayerCorrectlyTypedCallBack += () =>
        {
            Score += 2;
        };

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
                HP -= 5;

                print("TimeOver");
            }

            defeated = false;
            timeOver = false;
        }
    }

    float GetTimer(string word)
    {
        //return word.Length * 0.75f + 5;

        float timer = 0;

        float mul_wordLength = 1;

        switch (useGlobalGameLevel ? GlobalGameLevel : GameLevel)
        {
            case GameLevel.Easy:
                timer = 9;
                mul_wordLength = 1.2f;
                break;
            case GameLevel.Normal:
                timer = 7;
                mul_wordLength = 0.8f;
                break;
            case GameLevel.Hard:
                timer = 5;
                mul_wordLength = 0.75f;
                break;
        }

        return timer + word.Length * mul_wordLength;
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
    
    public void SetGameLevel(GameLevel level)
    {
        GameLevel = level;
    }

    void OnHPZero()
    {

    }
    
}
public enum GameLevel
    {
        Easy,
        Normal,
        Hard
    }