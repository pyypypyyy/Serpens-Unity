using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //enter state
    void OnEnter();


    //update state
    void OnUpdate();


    //fixed update state
    void OnFixedUpdate();



    //exit state
    void OnExit();

}
