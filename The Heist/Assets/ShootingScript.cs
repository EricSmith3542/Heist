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

    public WeaponsGraphics flash;
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
            flash.MuzzleFlash();
        }

        //Knife
        if(Input.GetButtonDown("Fire1") && (Weapons.currentWeapon == 3))
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
            }

            //Taze if holding taser
            if (target != null && Weapons.currentWeapon == 2)
            {
                target.SetTazed();
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

            if (target != null && Weapons.currentWeapon == 3)
            {
                target.TakeDamage(damageDealt);
            }

        }
    }

    void GrenadeToss() {
        grenade1.toss(gameObject.Transform.forward);
    }
}
