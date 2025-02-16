using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    public Animator move;
    public bool menuOpen;

    private void Start()
    {
        menuOpen = false;
    }

    public void moveIn()
    {
        move.SetTrigger("MoveIn");

        menuOpen = true;
    }

    public void moveOut()
    {
        move.SetTrigger("MoveOut");
        menuOpen = false;
    }

}
