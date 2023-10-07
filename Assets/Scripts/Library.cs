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

    //Listという配列を作成＆箱アドレス指定
    private JsonArray Wordlist = new JsonArray();

    private System.Random r1 = new System.Random();


    void Start()
    {
        //次の箱に行く方法を調べる

        //jsonファイルの位置(パス)を入れる
        string dataPath = "Assets\\Dictionary.json";

        // 念のためファイルの存在チェック
        if (!File.Exists(dataPath)) return;

        // JSONデータとしてデータを読み込む
        string file = File.ReadAllText(dataPath);

        // JSON形式からオブジェクトにデシリアライズ
        Wordlist = JsonUtility.FromJson<JsonArray>(file);
    }

    //ランダムな文字列を出力する関数
    public string RandomWord()
    {
        string word = Wordlist.words[r1.Next(0, Wordlist.words.Length)];

        Debug.Log("今回のワード：" + word);

        return word;
    }
}
