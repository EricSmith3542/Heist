using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;
    public float knifeRange = .3f;
    public float damageDealt = 50f;
    public Grenade grenade1;
    

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
           Weapons.currentMuzzleFlash.MuzzleFlash();
        }

        //Knife
        if(Input.GetButtonDown("Fire1") && (Weapons.currentWeapon == 4))
        {
            Knife();
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

            SecurityGuardAI target = hit.transform.GetComponent<SecurityGuardAI>();

            //Take damage if holding pistol
            if (target != null && Weapons.currentWeapon == 1) {
                target.TakeDamage(damageDealt);
                return;
            }

            //Taze if holding taser
            if (target != null && Weapons.currentWeapon == 2)
            {
                target.SetTazed();
                return;
            }

            DogAI targetDog = hit.transform.GetComponent<DogAI>();

            if(targetDog != null && Weapons.currentWeapon == 1)
            {
                targetDog.TakeDamage(damageDealt);
                return;
            }

            if (targetDog != null && Weapons.currentWeapon == 2)
            {
                targetDog.SetTazed();
                return;
            }

            scr_camera cam = hit.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<scr_camera>();

            if (cam != null && (Weapons.currentWeapon == 1 || Weapons.currentWeapon == 2))
            {
                cam.TakeDamage(damageDealt);
            }
        }
    }

    void Knife()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, knifeRange))
        {
            Debug.Log(hit.transform.name);

            SecurityGuardAI target = hit.transform.GetComponent<SecurityGuardAI>();

            if (target != null && Weapons.currentWeapon == 4)
            {
                target.TakeDamage(damageDealt);
            }

            DogAI targetDog = hit.transform.GetComponent<DogAI>();

            {
                targetDog.TakeDamage(damageDealt);
            }

            scr_camera cam = hit.transform.GetComponent<scr_camera>();

            if (cam != null && Weapons.currentWeapon == 4)
            {
                cam.TakeDamage(damageDealt);
            }
        }
    }

    void GrenadeToss() {
        //grenade1.toss(gameObject.Transform.forward);
    }
}
