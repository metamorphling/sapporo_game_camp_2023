using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�̃^�C�s���O�̓��͂��󂯕t����
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

   �@IEnumerator PlayerLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);

            var key = Input.inputString;    //�L�[�{�[�h���͂�ǂݎ�� 
        }
    }
}
