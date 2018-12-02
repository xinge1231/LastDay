using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleInfoUI : IUserInterface
{
    private Button m_btnEnemy = null;
    private Button m_btnSoldier = null;
    private Button m_btnGroup = null;
    private Text m_txtKilled = null;

    public BattleInfoUI(GameManager gameManager) : base(gameManager)
    {
       
    }

    public override void initialize()
    {
        m_rootUI = UITool.findUIGameObject("BattleUI");
        m_btnEnemy = UITool.getUIComponent<Button>(m_rootUI,"EnemyButton");
        m_btnSoldier = UITool.getUIComponent<Button>(m_rootUI, "SoldierButton");
        m_btnGroup = UITool.getUIComponent<Button>(m_rootUI, "GroupButton");
        m_txtKilled = UITool.getUIComponent<Text>(m_rootUI, "KilledText");
        m_btnEnemy.onClick.AddListener(onClickEnemy);
        m_btnSoldier.onClick.AddListener(onClickSoldier);
        m_btnGroup.onClick.AddListener(onClickGroup);

        //注册事件监听(用于点击生产敌人时显示敌人数量)
        m_gameManager.registerGameEvent(ENUM_GameEvent.EnemyCount, new EnemyCountListener());
    }

    public override void release()
    {
        base.release();
    }

    public override void update()
    {
        base.update();
    }

    private void onClickEnemy() {
        m_gameManager.addCommand(ENUM_CommandType.ProduceCharacter, new ProduceEnemyCommand());
    }
    private void onClickSoldier() { }
    private void onClickGroup() { }

    public void updateEnemyCountInfo() {
        m_txtKilled.text = GameManager.Instance.getEnemyCount().ToString();
    }
}
