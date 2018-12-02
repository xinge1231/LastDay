/*
 * 游戏主程序
 */
using UnityEngine;
using System.Collections;

public class GameManager{
    //是否结束游戏(进入战斗场景表示已经开始游戏)
    private bool m_bGameover; 
    //游戏系统
    private CharacterSystem m_characterSystem = null;
    private CommandSystem m_commandSystem = null;
    private GameEventSystem m_gameEventSystem = null;
    private TriggerSystem m_triggerSystem = null;
    //用户界面
    //private BattleUI m_battleUI = null;
    
    private PlayerInfoUI m_playerInfoUI = null;
    private BattleInfoUI m_battleInfoUI = null;


    //单例模式
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null)
                _instance = new GameManager(); //注意保存！！
            return _instance;
        }
    }
    private GameManager() { }

    public void initial() {
        Debug.Log("InitSystemAndUI");
        m_bGameover = false; 
        m_characterSystem = new CharacterSystem(Instance);
        m_commandSystem = new CommandSystem(Instance);
        m_gameEventSystem = new GameEventSystem(Instance);
        m_triggerSystem = new TriggerSystem(Instance);

        m_battleInfoUI = new BattleInfoUI(Instance);

        m_characterSystem.initialize();
        m_commandSystem.initialize();
        m_battleInfoUI.initialize();
        m_gameEventSystem.initialize();

        testInit();
    }

    public void gameover() {
        m_bGameover = true;
    }
    public bool isGameover() {
        return m_bGameover;
    }

    public void returnToMainMenu() {
        SceneStateController.Instance.setState(new MainMenuState(SceneStateController.Instance),"GameStart");
    }

    //测试初始化
    public void testInit() {
        //GameObject obj = UnityTool.findGameObject("TestTrigger");
        //SightTrigger trigger = new SightTrigger(obj);
        //SoundTrigger trigger = new SoundTrigger(obj);
       // trigger.Radius = 20.0f;
        //m_triggerSystem.registerTrigger(trigger);

    }

    public void release() { }

    //物理相关更新
    public void fixedUpdate() {
        m_characterSystem.fixedUpdate();
    }

    //游戏战斗逻辑更新
    public void update() {
        //玩家输入
        inputProcess();
        //游戏系统更新
        m_characterSystem.update();
        m_commandSystem.update();
        m_gameEventSystem.update();
        m_triggerSystem.update();
        //游戏界面更新
        m_battleInfoUI.update();
    }

    //用户输入处理
    private void inputProcess() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //区别GetButtonDown
        {
            addCommand(ENUM_CommandType.ProduceCharacter, new ProduceEnemyCommand());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            addCommand(ENUM_CommandType.ProduceCharacter, new ProduceSoldierCommand());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (m_characterSystem.getPlayer() == null)
            {
                GameObject mainCamera = UnityTool.findGameObject("Main Camera");
                mainCamera.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                //mainCamera.GetComponent<Camera>().enabled = false;
                addCommand(ENUM_CommandType.ProduceCharacter, new ProducePlayerCommand());

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){
            getPlayer().addMoveSpeed(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            getPlayer().reduceMoveSpeed(2);
        }
    }

    /*系统内部接口:功能*/
    //设置玩家
    public void setPlayer(IPlayer player) {
        m_characterSystem.setPlayer(player);
    }

    //获取玩家
    public IPlayer getPlayer() {
        return m_characterSystem.getPlayer();
    }
    //根据游戏对象查找角色
    public ICharacter findCharacterByGameObject(GameObject gameObj) {
        return m_characterSystem.getCharacterByGameObject(gameObj);
    }

    //根据游戏对象ID查找士兵
    public ICharacter findSoldierByGameObjectID(int gameObjectID){
        return m_characterSystem.getSoldierByGameObjectID(gameObjectID);
       
    }

    //根据游戏对象ID查找敌人
    public ICharacter findEnemyByGameObjectID(int gameObjectID)
    {
        return m_characterSystem.getEnemyByGameObjectID(gameObjectID);
    }

    //获取第一个士兵。测试用。
    public ICharacter getFirstSoldier() {
        return m_characterSystem.getFirstSoldier();
    }

    //添加角色
    public void addCharacter(ICharacter character) {
        m_characterSystem.addCharacter(character);
    }

    //删除角色
    public void removeCharacter(ICharacter character)
    {
        m_characterSystem.removeCharacter(character);
    }

    //添加一条命令
    public void addCommand(ENUM_CommandType commandType, ICommand command) {
        m_commandSystem.addCommand(commandType, command);
    }

    //注册事件监听
    public void registerGameEvent(ENUM_GameEvent gameEvent, IGameEventListener listener) {
        m_gameEventSystem.addGameEventListener(gameEvent,listener);
    }
    //通知事件监听
    public void notifyGameEvent(ENUM_GameEvent gameEvent,System.Object eventParam) {
        m_gameEventSystem.notifyGameEvent(gameEvent,eventParam);
    }

    //注册触发器
    public void registerTrigger(ITrigger trigger) {
        m_triggerSystem.registerTrigger(trigger);
    }

    //注册感知器
    public void registerSensor(ISensor sensor) {
        m_triggerSystem.registerSensor(sensor);
    }

    //获取杀敌数
    public int getEnemyCount() {
        return m_characterSystem.getEnemyCount();
    }

    /*系统内部接口:显示*/
    public void showEnemyCount() {
        m_battleInfoUI.updateEnemyCountInfo();
    }

}
