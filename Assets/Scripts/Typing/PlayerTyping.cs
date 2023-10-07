using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのタイピングの入力を受け付ける
/// </summary>
public class PlayerTyping : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;

    public int Score {  get; private set; }

    void Start()
    {
        StartCoroutine(PlayerLoop());
        print(text.text.ToLower()[text.firstVisibleCharacter]);
    }

    IEnumerator PlayerLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.anyKeyDown & Input.inputString.Length > 0);

            var key = Input.inputString;    //キーボード入力を読み取り

            print(key.Length);

            if (key[0] == text.text.ToLower()[text.firstVisibleCharacter])
            {
                text.text = text.text.Remove(text.firstVisibleCharacter, 1);
            }
            else
            {
                print("miss");
                //Score--;
            }

            yield return null;
        }
    }
}
