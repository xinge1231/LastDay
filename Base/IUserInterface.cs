using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class IUserInterface
{
    protected GameManager m_gameManager = null; //指向中介类
    protected GameObject m_rootUI = null;
    private bool m_bActive = true;

    public IUserInterface(GameManager gameManager) {
        m_gameManager = gameManager;
    }

    public bool isVisible() {
        return m_bActive;
    }

    public virtual void show() {
        m_rootUI.SetActive(true);
        m_bActive = true;
    }

    public virtual void hide() {
        m_rootUI.SetActive(false);
        m_bActive = false;
    }

    public virtual void initialize() { }
    public virtual void release() { }
    public virtual void update() { }
}

