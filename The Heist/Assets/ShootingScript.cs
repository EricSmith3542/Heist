﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;
    public float damageDealt = 50f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Shoot if the player is holding the pistol or taser and has ammo
        if (Input.GetButtonDown("Fire1") && ((Weapons.currentWeapon == 1 && AmmoScriptTMP.pistolAmmo > 0) || (Weapons.currentWeapon == 2 && AmmoScriptTMP.taserAmmo > 0)))
        {
            Shoot();
            WeaponsGraphics.MuzzleFlash();
        }

        //Toss Grenade
        if (Input.GetButtonDown("Fire1") && Weapons.currentWeapon == 3 && AmmoScriptTMP.grenadeAmount > 0){
            GrenadeToss();
        }
    }

    void Shoot() {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);

            SecurityGuardAIDamage target = hit.transform.GetComponent<SecurityGuardAIDamage>();

            //Take damage if holding pistol
            if (target != null && Weapons.currentWeapon == 1) {
                target.TakeDamage(damageDealt);
            }


            if (target != null && Weapons.currentWeapon == 2)
            {
                target.SetTazed();
            }
        }
    }

    void GrenadeToss() {

    }
}
