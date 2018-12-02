/*
 * 定义建造系统中可能用到的配件安装方法
 * (安装主体在建造参数中定义，在建造系统中初始化)
 */

using UnityEngine;
using System.Collections;

public abstract class ICharacterBuilder{
    protected ICharacterBuildParam m_characterBuildParam = null;
    //设置建造参数(用于各个步骤)
    public abstract void setBuildParam(ICharacterBuildParam characterBuildParam);
    //设置模型
    public abstract void loadAsset();
    //设置武器
    public abstract void setWeapon();
    //设置AI
    public abstract void setAI();
    //设置角色属性
    public abstract void setCharacterAttr();

    //设置操控行为
    public abstract void setSteerings();

    //设置感知系统
    public abstract void setSensors();
}
