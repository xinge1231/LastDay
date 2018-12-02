using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuUI : IUserInterface
{
    //界面组件
    private Image m_windowPanels = null;

    private Button m_startButton = null;
    private Button m_optionButton = null;
    private Button m_exitButton = null;

    private InputField m_usernameInputField = null;
    private Button m_confirmButton = null;
    private Button m_backButton1 = null;

    private Toggle m_soundTaggle = null;
    private Button m_backButton2 = null;



    
    public MainMenuUI(GameManager gameManager) : base(gameManager)
    {
    }

    public override void initialize()
    {
        m_rootUI = UITool.findUIGameObject("MainMenuUI");
        m_startButton = UITool.getUIComponent<Button>(m_rootUI, "StartButton");
        m_startButton.onClick.AddListener(onClickStartButton);//添加点击事件
    }

    public override void release()
    {
        base.release();
    }

    public override void update()
    {
        base.update();
    }

    void onClickStartButton() {
        SceneStateController.Instance.setState(new BattleState(SceneStateController.Instance), "GamePlay");
    }
}
