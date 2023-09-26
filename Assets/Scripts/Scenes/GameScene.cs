using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Define;


public class GameScene : BaseScene
{
    public bool m_isNewGame = true;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        //Cursor.lockState = CursorLockMode.Locked;

    }

    public void Start()
    {
        Managers.Game.GameStart(m_isNewGame);
    }

    public override void Clear()
    {
        
    }
}
