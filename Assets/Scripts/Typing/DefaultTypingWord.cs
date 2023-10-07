using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTypingWord : TypingWordBase
{
    [SerializeField] TMPro.TMP_Text text;

    string defaultText = "";
    int textCount;

    IEnumerator timerCoroutine;
    float timer = 10;

    private void Start()
    {
        if(text == null)
        {
            Destroy(gameObject);
        }
    }

    public event System.Action<float> OnTimerChangedCallBack;
    public event System.Action OnTimeOverCallBack;
    public override int GetAllCharacterCount()
    {
        return defaultText.Length;
    }

    public override char GetNextCharacter()
    {
        if (text == null || text.text == null || text.text.Length == 0)
        {
            return default;
        }
        return text.text[text.firstVisibleCharacter];
    }

    public override int GetRemainingCharacterCount()
    {
        if (text == null || text.text == null || text.text.Length == 0)
        {
            return -1;
        }
        return text.maxVisibleCharacters;
    }

    public override string GetWord()
    {
        return defaultText;
    }

    public override void SendTypingCharacter(char character, out bool isMatched)
    {
        if (text == null || text.text == null || text.text.Length == 0)
        {
            isMatched = false;
            return;
        }

        isMatched = character == text.text.ToLower()[text.firstVisibleCharacter];

        if (isMatched)
        {
            text.text = text.text.Remove(text.firstVisibleCharacter, 1);
            textCount--;

            if (textCount == 0)
            {
                OnDefeated?.Invoke();
            }
        }
    }

    public override void SetWord(string word)
    {
        text.text = word;
        textCount = word.Length;
        defaultText = word;
    }

    public void WordSpawn()
    {
        text.maxVisibleCharacters = 0;

        StartCoroutine(WordSpawnAnimation());

        IEnumerator WordSpawnAnimation()
        {
            for(int i=0;i<text.text.Length;i++)
            {
                text.maxVisibleCharacters++;

                yield return new WaitForSeconds(0.05f);
            }
        }

        timerCoroutine = Timer();
        StartCoroutine(timerCoroutine);
    }

    IEnumerator Timer()
    {
        while (timer>0)
        {
            yield return null;

            timer -= Time.deltaTime;

            OnTimerChangedCallBack?.Invoke(timer);
        }

        OnTimerChangedCallBack?.Invoke(timer);
        OnTimeOverCallBack?.Invoke();

        yield break;
    }

    public void SetTimer(float timer)
    {
        this.timer = timer;
    }

    public override void Stop()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }

    public override void Restart()
    {
        if (timerCoroutine != null)
        {
            StartCoroutine(timerCoroutine);
        }
    }
}
