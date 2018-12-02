/*
 * 主菜单场景(状态)
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuState : ISceneState
{
    private MainMenuUI m_mainMenuUI = null;
    //GameObject domino;
    public MainMenuState(SceneStateController controller) : base(controller)
    {
        stateName = "MainMenuState";
        sceneName = "GameStart";
        
    }

    public override void stateBegin()
    {
        m_mainMenuUI = new MainMenuUI(null);
        m_mainMenuUI.initialize();
        m_mainMenuUI.show();
    }

    public override void stateEnd()
    {
        base.stateEnd();
    }

    public override void stateUpdate()
    {
        //m_controller.setState(new BattleState(m_controller), "GamePlay");
    }

}
