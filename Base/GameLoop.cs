/*
 * 主游戏循环
 */
using UnityEngine;
using System.Collections;
using System;

public class GameLoop : MonoBehaviour {
    SceneStateController m_sceneStateController = SceneStateController.Instance;

    void Awake() {
        GameObject.DontDestroyOnLoad(this.gameObject);
        UnityEngine.Random.seed = (int)DateTime.Now.Ticks;
    }

	// Use this for initialization
	void Start () {
        m_sceneStateController.setState(new StartState(m_sceneStateController),"");

    }
    void FixedUpdate() {
        m_sceneStateController.stateFixedUpdate();
    }
	
	// Update is called once per frame
	void Update () {
        m_sceneStateController.stateUpdate();
	}
}
