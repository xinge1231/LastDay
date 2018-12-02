using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//事件名称
public enum ENUM_GameEvent {
    NUll = 0,
    EnemyKilled = 1,
    EnemyCount = 2,

    SightTrigger = 10,
    SoundTrigger = 11,

}
public class GameEventSystem : IGameSystem {
    //字典存储事件名称以及所对应的事件对象
    private Dictionary<ENUM_GameEvent, IGameEvent> m_gameEvents = new Dictionary<ENUM_GameEvent, IGameEvent>();
    public GameEventSystem(GameManager gameManager) : base(gameManager)
    {
    }

    //获取指定事件对象（如果不存在就进行添加(注册事件)）
    public IGameEvent getGameEvent(ENUM_GameEvent emGameEvent) {
        if (m_gameEvents.ContainsKey(emGameEvent))
            return m_gameEvents[emGameEvent];

        switch (emGameEvent) {
            case ENUM_GameEvent.EnemyKilled:
                m_gameEvents[emGameEvent] = new EnemyKilledEvent();
                break;
            case ENUM_GameEvent.EnemyCount:
                m_gameEvents[emGameEvent] = new EnemyCountEvent();
                break;
            default:
                Debug.Log("没有名为[" + emGameEvent + "]的游戏事件");
                return null;
        }
        return m_gameEvents[emGameEvent];
    }

    //为指定事件添加监听器(添加观察者)
    public void addGameEventListener(ENUM_GameEvent emGameEvent, IGameEventListener listener) {
        IGameEvent gameEvent = getGameEvent(emGameEvent);
        gameEvent.addListener(listener);
        listener.setGameEvent(gameEvent);
    }

    //通知事件监听
    public void notifyGameEvent(ENUM_GameEvent emGameEvent, System.Object eventParam) {
        if (m_gameEvents.ContainsKey(emGameEvent)) {
            m_gameEvents[emGameEvent].setEventParam(eventParam);
            m_gameEvents[emGameEvent].notify();
        }
    }

}
