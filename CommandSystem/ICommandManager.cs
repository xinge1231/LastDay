/*
 * 命令管理者接口(管理某一类命令，例如网络命令)
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ICommandManager {
    protected List<ICommand> m_commands = new List<ICommand>();
    protected float m_leftTime = 0;
    protected float m_coolDownTime = 1.0f;

    public void addCommand(ICommand command) {
        m_commands.Add(command);
    }

    public void removeCommand(ICommand command) {
        m_commands.Remove(command);
    }

    public void runCommand() {
        if (m_commands.Count == 0)
            return;
        m_leftTime -= Time.deltaTime;
        if (m_leftTime <= 0) {
            m_commands[0].execute();
            m_commands.RemoveAt(0);
            m_leftTime = m_coolDownTime;
        }
    }

}
