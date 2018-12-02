/*
 * 监听器(感知器)接口
 * 
 */

using UnityEngine;
using System.Collections;

public abstract class IGameEventListener{
    protected IGameEvent m_gameEvent = null; //用于获取事件参数

    //设置当前监听器监听的事件
    public void setGameEvent(IGameEvent gameEvent) {
        m_gameEvent = gameEvent;
    }

    //事件监听器收到通知后的处理函数。通过“拖”的方式从事件获取事件参数
    public abstract void callBack();
}
