using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private class JsonArray
    {
        public string[] words;
    }

    //private List<string> Wordlist = new List<string>();

    //List�Ƃ����z����쐬�����A�h���X�w��
    private JsonArray Wordlist = new JsonArray();

    private System.Random r1 = new System.Random();


    void Start()
    {
        //���̔��ɍs�����@�𒲂ׂ�

        //json�t�@�C���̈ʒu(�p�X)������
        string dataPath = "Assets\\Dictionary.json";

        // �O�̂��߃t�@�C���̑��݃`�F�b�N
        if (!File.Exists(dataPath)) return;

        // JSON�f�[�^�Ƃ��ăf�[�^��ǂݍ���
        string file = File.ReadAllText(dataPath);

        // JSON�`������I�u�W�F�N�g�Ƀf�V���A���C�Y
        Wordlist = JsonUtility.FromJson<JsonArray>(file);
    }

    //�����_���ȕ�������o�͂���֐�
    public string RandomWord()
    {
        string word = Wordlist.words[r1.Next(0, Wordlist.words.Length)];

        Debug.Log("����̃��[�h�F" + word);

        return word;
    }
}
