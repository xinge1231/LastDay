using UnityEngine;
using System.Collections;

public class WeaponAK : IRangedWeapon
{
    public WeaponAK() {
        m_assetName = "WeaponAK";
    }
    public override void fire()
    {
        base.fire();
    }

    public override void disableEffect()
    {
        base.disableEffect();
    }

    public override void setupEffect()
    {
        base.setupEffect();
    }

    public override void showAtkEffect(Vector3 targetPosition)
    {
        base.showAtkEffect(targetPosition);
    }

    public override void showHitEffect(Vector3 targetPosition)
    {
        base.showHitEffect(targetPosition);
    }
}
