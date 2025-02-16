using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    public Animator move;


    public void moveIn()
    {
        move.SetTrigger("MoveIn");
    }

    public void moveOut()
    {
        move.SetTrigger("MoveOut");
    }

}
