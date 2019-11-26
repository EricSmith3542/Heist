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
    int numOfWeapons = 5;
    public GameObject[] weaponsObjects;

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
        for (int i = 0; i < numOfWeapons; i++)
        {
            if (i == newWeapon)
            {
                Debug.Log("Activated");
                weaponsObjects[i].SetActive(true);
            }
            else
            {
                Debug.Log("Deactivated");
                weaponsObjects[i].SetActive(false);
            }
        }
    }
}