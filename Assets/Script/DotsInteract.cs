using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsInteract : MonoBehaviour, IInteractiveWithLight
{
    public void InteractWithLight()
    {
        Debug.Log("Flashlight interaction executed");
    }
}
