/*
 * 初始状态场景
 */
using UnityEngine;
using System.Collections;

public class StartState : ISceneState
{
    public StartState(SceneStateController controller) : base(controller)
    {
        stateName = "StartState";
        sceneName = "StartScene";
    }

    public override void stateBegin()
    {
        //游戏初始化
    }

    public override void stateUpdate()
    {
        m_controller.setState(new MainMenuState(m_controller),"GameStart");
    }
}
