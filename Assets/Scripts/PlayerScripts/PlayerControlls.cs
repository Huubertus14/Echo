using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControlls : SingetonMonobehaviour<PlayerControlls>
{
    PlayerBehaviour pb;
    DynamicJoystick dj;
    private bool hasPlayerBehaviour = false;
    private bool gasToggle = false;

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
        hasPlayerBehaviour = true;
    }

    private void Update()
    {
        gasToggle = Input.GetKey(KeyCode.Z);

        if (hasPlayerBehaviour)
        {
            pb.GetPlayerMovement.JoyStickControlls(dj);
            if (gasToggle)
            {
                pb.GetPlayerMovement.Accelerate();
            }
            else
            {
                pb.GetPlayerMovement.WaterResistance();
            }
        }
    }

    private void OnDestroy()
    {
        hasPlayerBehaviour = false;
    }

    public void Gas(bool gas)
    {
        gasToggle = gas;
    }

    public void PlayerPing()
    {
        if (hasPlayerBehaviour)
        {
            pb.Ping();
        }
    }

    public void PlayerShoot()
    {
        if (hasPlayerBehaviour)
        {
            pb.Shoot();
        }
    }

    public void PlayerPressedPause()
    {
        GameManager.SP.LeaveGame();
    }
}
