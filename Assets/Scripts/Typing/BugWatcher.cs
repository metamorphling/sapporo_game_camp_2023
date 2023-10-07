using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugWatcher : MonoBehaviour
{
    public BugTyphoon bugTyphoon;
    public BugClick bugClick;

    public event System.Action OnTyphoonShown;
    public event System.Action OnTyphoonHide;
    public event System.Action OnClickShown;
    public event System.Action OnClickHide;

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

                    if (ch.GetComponent<BugClick>() != null)
                    {
                        bugClick=ch.GetComponent<BugClick>();   
                    }

                    if (ch.GetComponent<BugTyphoon>() != null)
                    {
                        bugTyphoon = ch.GetComponent<BugTyphoon>();
                    }
                }
            }
        }

        yield return Watching();
    }

    IEnumerator Watching()
    {
        var ty_active = bugTyphoon.gameObject.activeSelf;
        var tc_active=bugClick.gameObject.activeSelf;

        if (ty_active)
        {
            OnTyphoonShown?.Invoke();
        }
        else
        {
            OnTyphoonHide?.Invoke();
        }

        if (tc_active)
        {
            OnClickShown?.Invoke();
        }
        else
        {
            OnClickHide?.Invoke();
        }

        while (true)
        {
            yield return null;

            if(ty_active& !bugTyphoon.gameObject.activeSelf)
            {
                OnTyphoonHide?.Invoke();
                print("t_h");
            }

            if(!ty_active& bugTyphoon.gameObject.activeSelf)
            {
                OnTyphoonShown?.Invoke();
                print("t_s");
            }

            if (tc_active& !bugClick.gameObject.activeSelf)
            {
                OnClickHide?.Invoke();
                print("c_h");
            }

            if (!tc_active & bugClick.gameObject.activeSelf)
            {
                OnClickShown?.Invoke();
                print("c_s");
            }

            ty_active = bugTyphoon.gameObject.activeSelf;
            tc_active = bugClick.gameObject.activeSelf;
        }
    }
}
