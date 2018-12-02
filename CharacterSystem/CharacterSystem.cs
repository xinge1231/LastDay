using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterSystem : IGameSystem {
    private Dictionary<int, ICharacter> m_soldiers; //通过游戏对象ID查找角色对象
    private Dictionary<int, ICharacter> m_enemys;
    private IPlayer m_player = null;
    private int m_enemyKilled = 0;

    public CharacterSystem(GameManager gameManager) : base(gameManager)
    {
        m_soldiers = new Dictionary<int, ICharacter>();
        m_enemys = new Dictionary<int, ICharacter>();
    }

    public override void initialize()
    {
        base.initialize();

        //注册角色系统需要用到的监听
        GameManager.Instance.registerGameEvent(ENUM_GameEvent.EnemyKilled, new EnemyKilledListener());
    }

    public override void release()
    {
        base.release();
    }
    public override void fixedUpdate() {
        if(m_player!=null)
            m_player.fixedUpdate();
        foreach (ICharacter enemy in m_enemys.Values)
        {
            enemy.fixedUpdate();
        }

        foreach (ICharacter soldier in m_soldiers.Values)
        {
            soldier.fixedUpdate();
        }
    }
    public override void update()
    {
        if (m_player != null)
        {
            m_player.update();
        }
        foreach (ICharacter enemy in m_enemys.Values) {
            enemy.update();
        }

        foreach (ICharacter soldier in m_soldiers.Values)
        {
            soldier.update();
        }
        deleteRemovableCharacters();

    }

    //删除可移除的角色(销毁游戏对象并移出角色列表)
    public void deleteRemovableCharacters() {
        //必须要缓存才能移除。不能在foreach迭代中移除
        List<int> tmp = new List<int>();
        foreach (KeyValuePair<int,ICharacter> enemy in m_enemys) {
            if (enemy.Value.IsKilled && enemy.Value.CanRemove) {
                tmp.Add(enemy.Key);
            }
        }
        foreach (int key in tmp) {
            m_enemys[key].release();
            m_enemys.Remove(key);
        }
    }

    //根据游戏对象的instanceID获取士兵对象
    public ICharacter getSoldierByGameObjectID(int gameObjectID) {
        if (m_soldiers.ContainsKey(gameObjectID))
            return m_soldiers[gameObjectID];
        return null;
    }

    //根据游戏对象的instanceID获取敌人对象
    public ICharacter getEnemyByGameObjectID(int gameObjectID) {
        if (m_enemys.ContainsKey(gameObjectID))
            return m_enemys[gameObjectID];
        return null;
    }

    //根据游戏对象获取角色
    public ICharacter getCharacterByGameObject(GameObject gameObj) {
        int id = gameObj.GetInstanceID();
        ICharacter character = null;
        switch (gameObj.tag)
        {
            case "Soldier":
                character = getSoldierByGameObjectID(id);
                break;
            case "Enemy":
                character = getEnemyByGameObjectID(id);
                break;
            case "Player":
                character = m_player;
                break;
            default:
                Debug.Log("不存在该类型角色");
                return null;
        }
        return character;
    }

    public void addCharacter(ICharacter character) {
        int ID = character.getGameObject().GetInstanceID();
        if (character is ISoldier)
            m_soldiers.Add(ID, character as ISoldier);
        else if (character is IEnemy)
        {
            m_enemys.Add(ID, character as IEnemy);
        }

    }

    public void removeCharacter(ICharacter character)
    {
        int ID = character.getGameObject().GetInstanceID();
        if (character is ISoldier && m_soldiers.ContainsKey(ID))
            m_soldiers.Remove(ID);
        else if (character is IEnemy && m_enemys.ContainsKey(ID))
            m_enemys.Remove(ID);

    }

    //返回第一个士兵。测试用。
    public ICharacter getFirstSoldier() {
        if (m_soldiers.Count != 0) {
            return m_soldiers.Values.First();
        }
        return null;
    }


    public void setPlayer(IPlayer player) {
        m_player = player;
     
    }

    public IPlayer getPlayer() {
        return m_player;
    }

    public int getEnemyCount() {
        return m_enemys.Count;
    }
}
