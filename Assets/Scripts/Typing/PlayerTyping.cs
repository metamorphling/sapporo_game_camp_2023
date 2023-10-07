using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのタイピングの入力を受け付ける
/// </summary>
public class PlayerTyping : MonoBehaviour
{
    //TMPro.TMP_Text text;
    TextMesh text;
    
    void Start()
    {
        StartCoroutine(PlayerLoop());
        print(text.text[0]);
    }

    Enumerator PlayerLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);

            var key = Input.inputString;    //キーボード入力を読み取り 
        }
    }
}
