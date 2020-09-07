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
    private bool gasLocked = false;

    [Header("Ref Values:")]
    [SerializeField] private GameObject gasButton;
    [SerializeField] private GameObject gasToggleButton;

    private void Start()
    {
        dj = GetComponentInChildren<DynamicJoystick>();
        gasToggleButton.SetActive(false);
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
        if (gas)
        {
            gasButton.transform.localScale = new Vector3(1.5f,1.5f,1);
            gasLocked = false;
            gasToggle = gas;
        }
        else
        {
            if (!gasLocked)
            {
                gasToggle = gas;
            }
            gasButton.transform.localScale = new Vector3(1, 1, 1);
        }
        gasToggleButton.SetActive(gas);
    }

    public void LockGas()
    {
        gasToggle = true;
        gasLocked = true;
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
