using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour //Weapons
{
    /*Weapons:
     * 0. Empty
     * 1. Pistol
     * 2. Taser
     * 3. Grenade
     * 4. Knife
     */

    public static int currentWeapon = 0;
    public static WeaponsGraphics currentMuzzleFlash;
    int numOfWeapons = 5;
    public GameObject[] weaponsObjects;
    public WeaponsGraphics[] graphics;

    void Start()
    {
        int numOfWeapons = getNumOfWeapons();
        SwitchWeapons(currentWeapon);
        Debug.Log("Start");
    }

    int getNumOfWeapons() {

        return weaponsObjects.Length;
    }

    void Update()
    {
        for (int i = 1; i <= numOfWeapons; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
                Debug.Log("Input Recieved");
                currentWeapon = i - 1;
                SwitchWeapons(currentWeapon);
            }
        }
    }


    public void SwitchWeapons(int newWeapon)
    {
        foreach(GameObject weapon in weaponsObjects)
        {
            weapon.SetActive(false);
        }
        weaponsObjects[newWeapon].SetActive(true);
        currentMuzzleFlash = graphics[newWeapon];

        //for (int i = 0; i < numOfWeapons-1; i++)
        //{
        //    if (i == newWeapon)
        //    {
        //        Debug.Log("Activated");
        //        weaponsObjects[i].SetActive(true);
        //        if(graphics[i] != null)
        //            currentMuzzleFlash = graphics[i];
        //    }
        //    else
        //    {
        //        Debug.Log("Deactivated");
        //        weaponsObjects[i].SetActive(false);
        //    }
        //}
    }
}