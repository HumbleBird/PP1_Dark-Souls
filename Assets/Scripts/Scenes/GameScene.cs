using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Define;


public class GameScene : BaseScene
{
    public bool m_isNewGame = true;
    public bool m_isDeveling = true;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.Game.m_isNewGame = m_isNewGame;
        Managers.Game.m_Develing = m_isDeveling;

        // 게임을 처음 시작했다면
        if (Managers.Game.m_isNewGame)
        {
            // 인트로씬 처음으로 보여주기
            Managers.GameUI.ShowIntro("StartOpening");
        }

        Managers.Cursor.PowerOff();
    }

    public void Start()
    {
        Managers.Game.GameStart();
    }

    public override void Clear()
    {
        
    }
}
