/*
 * 角色建造者系统
 * 功能：负责装配角色类对象
 */
using UnityEngine;
using System.Collections;

public class CharacterBuilderSystem : IGameSystem
{

    public CharacterBuilderSystem(GameManager gameManager) : base(gameManager)
    {
    }

    //建造流程
    public void construct(ICharacterBuilder characterBuilder) {
        characterBuilder.loadAsset();
        characterBuilder.setCharacterAttr();
        characterBuilder.setWeapon();
        characterBuilder.setAI();
        characterBuilder.setSteerings();
        characterBuilder.setSensors();
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
        base.update();
    }
}
