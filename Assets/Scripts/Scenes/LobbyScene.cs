using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
        Managers.Sound.Play("Premonition", 1, Define.Sound.Bgm);
        Managers.Cursor.PowerOn();

    }

    public override void Clear()
    {

    }
}
