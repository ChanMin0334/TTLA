using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem upgradeEffect;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayUpgradeEffect();
        }
    }
    public void PlayUpgradeEffect()
    {
        if(upgradeEffect != null)
        {
            upgradeEffect.Play();
        }
    }
}
