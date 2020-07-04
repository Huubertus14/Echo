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

    /// <summary>
    /// Uuh lelijke code?
    /// </summary>
    /// <param name="player"></param>
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
        pb.Shoot();
    }

    public void PlayerPressedPause()
    {
        GameManager.SP.LeaveGame();
    }
}
