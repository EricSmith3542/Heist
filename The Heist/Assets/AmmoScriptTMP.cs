using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoScriptTMP : MonoBehaviour
{
    TextMeshProUGUI ammoText;
    public static int pistolAmmo = 6;
    public static int taserAmmo = 12;
    public static int grenadeAmount = 3;

    // Start is called before the first frame update
    void Start()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Weapons.currentWeapon == 0) //Fist
        {
            ammoText.text = "";
        }

        if (Weapons.currentWeapon == 1) //Pistol
        {
            ammoText.text = "Ammo: " + pistolAmmo.ToString();

            if (Input.GetButtonDown("Fire1") && pistolAmmo > 0)
            {
                pistolAmmo--;
            }
        }

        if (Weapons.currentWeapon == 2) //Taser
        {
            ammoText.text = "Ammo: " + taserAmmo.ToString();

            if (Input.GetButtonDown("Fire1") && taserAmmo > 0)
            {
                taserAmmo--;
            }
        }

        if (Weapons.currentWeapon == 3) //Pistol
        {
            ammoText.text = "Ammo: " + grenadeAmount.ToString();

            if (Input.GetButtonDown("Fire1") && grenadeAmount > 0)
            {
                grenadeAmount--;
            }
        }

        if (Weapons.currentWeapon == 4) //Knife
        {
            ammoText.text = "";
        }

    }
}