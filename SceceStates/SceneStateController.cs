/*
 *  场景状态控制器类(状态拥有者)
 *  功能：负责场景切换和更新。
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneStateController{
    private ISceneState m_state;
    private bool m_bRunBegin = false;
    private static SceneStateController _instance;

    public static SceneStateController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SceneStateController();
            return _instance;
        }

    }

    private SceneStateController() { }

    //场景切换
    public void setState(ISceneState state, string sceneName) {
        m_bRunBegin = false;
        loadScene(sceneName);
        if (m_state!=null) {
            m_state.stateEnd();
        }
        m_state = state;
    }

    //场景加载
    public void loadScene(string sceneName) {
        if (sceneName == null || sceneName.Length == 0)
            return;
        //Application.LoadLevel(sceneName);
        SceneManager.LoadScene(sceneName);
        
    }

    //场景物理更新
    public void stateFixedUpdate() {
        if (Application.isLoadingLevel)
            return;

        if (m_state != null && m_bRunBegin == false)
        {

            m_state.stateBegin(); //执行状态(场景)初始化
            m_bRunBegin = true;
            Debug.Log("StateBeginToRun " + m_bRunBegin.ToString());
        }

        m_state.stateFixedUpdate();
    }
    //场景更新
    public void stateUpdate() {
        /*
        if (Application.isLoadingLevel)
            return;

        if (m_state != null && m_bRunBegin == false)
        {
            
            m_state.stateBegin(); //执行状态(场景)初始化
            m_bRunBegin = true;
            Debug.Log("StateBeginToRun " + m_bRunBegin.ToString());
        }
        */
        if (m_state != null && m_bRunBegin == true)
            m_state.stateUpdate();
    }
}
