using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugWatcher : MonoBehaviour
{
    public List<Transform>Bugs = new List<Transform>();

    public List<string> names = new List<string>();

    public Dictionary<Transform,Bug> _Bugs=new ();

    public bool IsReady { get; private set; } = false;

    public int bugShowingCount = 0;

    //public event System.Action<int> OnDefeatBug;

    IEnumerator Start()
    {
        Scene? gimmicScene = null;

        while (gimmicScene == null)
        {
            foreach (var scene in GameManager.scenes)
            {
                if (scene.name == "Gimmick")
                {
                    gimmicScene = scene;
                }
            }
            yield return null;
        }

        foreach(var obj in gimmicScene?.GetRootGameObjects())
        {
            if (obj.name == "Canvas")
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {

                    var ch = obj.transform.GetChild(i);

                    if (names.Contains(ch.name))
                    {
                        Bugs.Add(ch);
                        _Bugs.Add(ch,ch.GetComponent<Bug>());
                    }
                }
            }
        }

        

        IsReady = true;

        yield return null;
        yield return null;

        yield return Watching();
    }

    IEnumerator Watching()
    {
        Dictionary<Transform, bool> tmp_activeSelf = new();

        foreach(var obj in Bugs)
        {

            if (obj.gameObject.activeSelf)
            {
                bugShowingCount++;
            }

            tmp_activeSelf.Add(obj, obj.gameObject.activeSelf);
        }

        while (true)
        {
            yield return null;

            foreach(var obj in Bugs)
            {
                if (tmp_activeSelf[obj] != obj.gameObject.activeSelf)
                {
                    bugShowingCount += obj.gameObject.activeSelf ? 1 : -1;
                    if (!obj.gameObject.activeSelf)
                    {
                        //OnDefeatBug?.Invoke(_Bugs[obj].score);
                    }
                }

                tmp_activeSelf[obj] = obj.gameObject.activeSelf;
            }
        }
    }
}
