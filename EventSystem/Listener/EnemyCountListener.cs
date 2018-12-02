using UnityEngine;
using System.Collections;

public class EnemyCountListener : IGameEventListener
{
    public override void callBack()
    {
        GameManager.Instance.showEnemyCount();
    }
}
