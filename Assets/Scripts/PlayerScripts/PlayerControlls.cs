using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : SingetonMonobehaviour<PlayerControlls>
{
    PlayerBehaviour pb;
    DynamicJoystick dj;


    private void Start()
    {
        dj = GetComponentInChildren<DynamicJoystick>();

        
    }

    public void GivePlayerBehaviour(PlayerBehaviour player)
    {
        pb = player;
    }

    private void FixedUpdate()
    {
        pb.GetPlayerMovement.JoyStickControlls(dj);
    }

    public void PlayerPing()
    {
        pb.Ping();
    }

    public void PlayerShoot()
    {

    }
}
