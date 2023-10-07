using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickScene : AdditiveSceme
{
    public BugClick bug;
    void Start()
    {
        Debug.Log("ギミックシーン起動");
        bug.Initialize(5);
    }

}
