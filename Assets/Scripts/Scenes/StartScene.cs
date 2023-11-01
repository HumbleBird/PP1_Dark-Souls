using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Start;
        Managers.Cursor.PowerOn();

    }

    public override void Clear()
    {
        Managers.Object.Clear();
    }
}
