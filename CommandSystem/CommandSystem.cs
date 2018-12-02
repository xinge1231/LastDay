/*
 * 命令系统
 * 功能：管理所有种类的命令
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ENUM_CommandType {
    Null = 0,
    ProduceCharacter = 1,
}

public class CommandSystem : IGameSystem
{
    protected Dictionary<ENUM_CommandType, ICommandManager> m_commandManagers= new Dictionary<ENUM_CommandType, ICommandManager>();
    public CommandSystem(GameManager gameManager) : base(gameManager)
    {
    }

    public override void fixedUpdate()
    {
        base.fixedUpdate();
    }

    public override void initialize()
    {
        base.initialize();
    }

    public override void release()
    {
        base.release();
    }

    public override void update()
    {
        foreach (ICommandManager manager in m_commandManagers.Values) {
            manager.runCommand();
        }
    }

    public void addCommand(ENUM_CommandType commandType,ICommand command) {
        if (m_commandManagers.ContainsKey(commandType))
            m_commandManagers[commandType].addCommand(command);
        else {
            m_commandManagers[commandType] = new ProduceCommandManager();
            m_commandManagers[commandType].addCommand(command);
        }
    }

    public void removeCommand(ENUM_CommandType commandType, ICommand command) {
        m_commandManagers[commandType].removeCommand(command);
    }
}
