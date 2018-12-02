using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class IGameSystem
{
    protected GameManager m_gameManager = null;//指向中介类

    public IGameSystem(GameManager gameManager) {
        m_gameManager = gameManager;
    }


    public virtual void initialize() { } //用于初始化，注册事件等
    public virtual void release() { }
    public virtual void fixedUpdate() { }
    public virtual void update() { }
}

