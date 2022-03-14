using System;
using UnityEngine;

public class ToolSwitch : MonoBehaviour

{
    private int selectedWeapon = 0;
    public Vector3 endPosition = new Vector3(0.7f, -1.2f, 0.9f);
    public Vector3 startPosition = new Vector3(0.7f, -0.7f, 0.9f);
    public float duration = 0.4f;
    private float elapsedTime;
    private bool weaponDown = false;

    private int previousWeapon = 1;
    
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        if (previousWeapon != selectedWeapon || weaponDown)
        {
            if (!weaponDown)
            {
                StartSwitch();
            }
            if (weaponDown)
            {
                EndSwitch();
            }
        }


    }
    void SelectWeapon()
    {
        previousWeapon = selectedWeapon;
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
    void StartSwitch()
    {
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / duration;

        transform.localPosition = Vector3.Lerp(startPosition, endPosition, percentage);

        if(percentage >= 1)
        {
            elapsedTime = 0;
            weaponDown = true;
            SelectWeapon();
        }
    }
    void EndSwitch()
    {
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / duration;

        transform.localPosition = Vector3.Lerp(endPosition, startPosition, percentage);

        if (percentage >= 1)
        {
            elapsedTime = 0;
            weaponDown = false;
        }
    }
}
