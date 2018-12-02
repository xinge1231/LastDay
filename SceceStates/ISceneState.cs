/*
 * 场景状态接口类
 */
public class ISceneState {
    private string m_stateName = "ISceneState";
    private string m_sceneName = "";
    public string stateName {
        get { return m_stateName; }
        set { m_stateName = value; }
    }

    public string sceneName
    {
        get { return m_sceneName; }
        set { m_sceneName = value; }
    }

    protected SceneStateController m_controller = null;

    public ISceneState(SceneStateController controller) {
        m_controller = controller;
    }
    public virtual void stateBegin() { }
    public virtual void stateEnd() { }
    public virtual void stateFixedUpdate() { }
    public virtual void stateUpdate() { }

    public override string ToString()
    {
        return string.Format("StateName={0}:" + stateName);
    }
}
