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
    [SerializeField] TMPro.TMP_Text text;

    [SerializeField] TypingWordBase word;

    public int Score { get; private set; } = 0;

    public event System.Action OnPlayerCorrectlyTypedCallBack;
    public event System.Action OnPlayerMissTypedCallBack;

    Queue<char> inputKeysQueue = new(0);
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

            //var key = ReadKeyBoardInput();   //キーボード入力を読み取り

            EnQueueKeyBoardInput();

            /*
            if (text.text == null || text.text.Length==0)
            {
                continue;
            }
            
            if (key == text.text.ToLower()[text.firstVisibleCharacter])
            {
                text.text = text.text.Remove(text.firstVisibleCharacter, 1);

                PlayerCorrectlyTyped();
            }
            else
            {
                PlayerMissTyped();
            }
            

            static char ReadKeyBoardInput()
            {
                return Input.inputString[0];
            }//InputStringをcharに変換
            */

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

            while(inputKeysQueue.Count > 0)
            {
                yield return null;

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
        yield return new WaitUntil(() => Input.anyKeyDown & Input.inputString != null & Input.inputString.Length > 0);
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
}
