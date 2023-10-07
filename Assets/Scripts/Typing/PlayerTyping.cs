using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのタイピングの入力を受け付ける
/// </summary>
public class PlayerTyping : MonoBehaviour
{
    [SerializeField] TypingWordBase word;

    public int Score { get; private set; } = 0;

    public event System.Action OnPlayerCorrectlyTypedCallBack;
    public event System.Action OnPlayerMissTypedCallBack;

    Queue<char> inputKeysQueue = new(0);

    bool IsPlayerStopped = false;
    IEnumerator Start()
    {
        StartCoroutine(KeyMatchingLoop());
        yield return PlayerInputLoop();
    }

    /// <summary>
    /// プレイヤーの入力を処理するループ
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerInputLoop()
    {
        while (true)
        {
            yield return null;
            yield return WaitForKeyBoardInput(); //キーボード入力を待つ

            EnQueueKeyBoardInput();

            void EnQueueKeyBoardInput()
            {
                var array = Input.inputString.ToCharArray();
                foreach(var c in array)
                {
                    print(c);
                    inputKeysQueue.Enqueue(c);
                }
            }
        }
    }

    IEnumerator KeyMatchingLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => inputKeysQueue.Count != 0);

            while (!IsPlayerStopped && inputKeysQueue.Count > 0)
            {
                yield return null;

                if (IsPlayerStopped)
                {
                    break;
                }

                var key = inputKeysQueue.Dequeue();

                print(key);

                word.SendTypingCharacter(key, out bool matched);

                if (matched)
                {
                    PlayerCorrectlyTyped();
                }
                else
                {
                    PlayerMissTyped();
                }
            }
        }
    }

    /// <summary>
    /// キーボード入力を待つ
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForKeyBoardInput()
    {
        yield return new WaitUntil(() => !IsPlayerStopped & Input.anyKeyDown & Input.inputString != null & Input.inputString.Length > 0);
    }

    /// <summary>
    /// スコアをリセット
    /// </summary>
    public void ResetScore() => Score = 0;

    /// <summary>
    /// プレイヤーが正しいキーを押したとき
    /// </summary>
    void PlayerCorrectlyTyped()
    {
        OnPlayerCorrectlyTypedCallBack?.Invoke();
        //print("correct");
    }

    /// <summary>
    /// プレイヤーが間違えたキーを押したとき
    /// </summary>
    void PlayerMissTyped()
    {
        OnPlayerMissTypedCallBack?.Invoke();
        //print("miss");
    }

    public void SetTypingWord(TypingWordBase word)
    {
        this.word = word;
    }

    public void StopTypingInput()
    {
        IsPlayerStopped = true;
        inputKeysQueue.Clear();
    }

    public void ReStartTypingInput()
    {
        IsPlayerStopped = false;
    }
}
