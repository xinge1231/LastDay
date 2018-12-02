using UnityEngine;
using System.Collections;

public class EnemyKilledEvent : IGameEvent{
    private int m_killCount = 0;

    //设置事件参数
    public override void setEventParam(System.Object eventParam)
    {
        base.setEventParam(eventParam);
        m_killCount = (int)eventParam;
    }
}
