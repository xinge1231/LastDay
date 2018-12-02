using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class EnemyZombie : IEnemy
{
    public EnemyZombie() {
        m_enemyType = ENUM_Enemy.Zombie;
        m_assetName = "Zombie1";
    }

    public override void attack()
    {
        //僵尸空手攻击
    }
}

