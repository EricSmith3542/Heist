using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsGraphics : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    //private ParticleSystem flash;

    public void MuzzleFlash() {
        //hitEffectPrefab = Instantiate(muzzleFlash);
        //ParticleSystem flash = Instantiate(MuzzleFlash, firePoint.transform, Quaternion.identity);
        //flash = new 
        //muzzleFlash = getComponent<ParticleSystem>();
        Debug.Log("Flashed");
        muzzleFlash.Emit(1);
    }

    private void Update()
    {
        
    }
}
