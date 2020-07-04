using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : SingetonMonobehaviour<PlayerControlls>
{
    PlayerBehaviour pb;
    DynamicJoystick dj;
    bool hasPlayeyBehaviour = false;

    private void Start()
    {
        dj = GetComponentInChildren<DynamicJoystick>();
    }

    /// <summary>
    /// Uuh lelijke code?
    /// </summary>
    /// <param name="player"></param>
    public void GivePlayerBehaviour(PlayerBehaviour player)
    {
        pb = player;
        hasPlayeyBehaviour = true;
    }

    private void Update()
    {
        if (hasPlayeyBehaviour)
        {
            pb.GetPlayerMovement.JoyStickControlls(dj);
        }
    }

    public void PlayerPing()
    {
        if (hasPlayeyBehaviour)
        {
            pb.Ping();
        }
    }

    public void PlayerShoot()
    {
        if (hasPlayeyBehaviour)
        {
            pb.Shoot();
        }
    }

    public void PlayerPressedPause()
    {
        GameManager.SP.LeaveGame();
    }
}
