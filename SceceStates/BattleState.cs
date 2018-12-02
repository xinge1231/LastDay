/*
 * 战斗场景(状态)
 */
using UnityEngine;
using System.Collections;

public class BattleState : ISceneState
{
    public BattleState(SceneStateController controller) : base(controller)
    {
        this.stateName = "BattleState";
        this.sceneName = "GamePlay";
    }

    public override void stateBegin()
    {
        Debug.Log("BattleStateInit");
        GameManager.Instance.initial();
    }

    public override void stateEnd()
    {
        GameManager.Instance.release();
    }

    public override void stateFixedUpdate()
    {
        GameManager.Instance.fixedUpdate();
    }
    public override void stateUpdate()
    {
        GameManager.Instance.update();
    }
}
