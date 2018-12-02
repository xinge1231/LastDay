/*
 * 事件接口
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IGameEvent{
    
    protected List<IGameEventListener> m_listeners = new List<IGameEventListener>();//当前事件的监听器列表(观察者列表)
    protected System.Object m_eventParam = null;//事件参数

    //添加监听器(注册事件)
    public void addListener(IGameEventListener listener) {
        m_listeners.Add(listener);
    }

    //移除监听器(取消注册)
    public void removeListener(IGameEventListener listener) {
        m_listeners.Remove(listener);
    }

    //设置事件参数(子类可以直接在此调用notify,设置完事件参数一般就是用于通知的)
    public virtual void setEventParam(System.Object eventParam) {
        m_eventParam = eventParam;
    }

    //分发事件通知（用于触发事件）
    public void notify() {
        //本质是遍历调用
        foreach (IGameEventListener listener in m_listeners) {
            listener.callBack();
        }
    }
}
