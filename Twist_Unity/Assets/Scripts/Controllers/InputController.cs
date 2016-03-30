using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {   

    //Register Player Input
    public void OnClick(string _direction)
    {
        //Send To Player Object
        PlayerObjectBehaviour.instance.InitiateTurning(_direction);
        Debug.Log(_direction);
    }
}
