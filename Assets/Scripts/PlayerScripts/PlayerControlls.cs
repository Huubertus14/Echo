using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class PlayerControlls : SingetonMonobehaviour<PlayerControlls>
{
    PlayerBehaviour pb;
    DynamicJoystick dj;
    private bool hasPlayerBehaviour = false;
    private bool gasToggle = false;
    private bool gasLocked = false;
    private bool shooting = false;

    [Header("Ref Values:")]
    [SerializeField] private GameObject gasButton;
    [SerializeField] private Image gasToggleButton;
    [SerializeField] private ImageFade[] fadeImages;

    protected override void Awake()
    {
        base.Awake();
        dj = GetComponentInChildren<DynamicJoystick>();
        fadeImages = GetComponentsInChildren<ImageFade>();
    }

    private void Start()
    {
        gasToggleButton.color = Color.white;
        gasToggleButton.gameObject.SetActive(false);

        foreach (var item in fadeImages)
        {
            item.SetAlpha(0);
        }
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

            if (shooting)
            {
                pb.Shoot();
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
            gasButton.transform.localScale = new Vector3(1.5f, 1.5f, 1);
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
        gasToggleButton.color = Color.white;
        gasToggleButton.gameObject.SetActive(gas);
    }

    public void LockGas()
    {
        gasToggleButton.color = Color.yellow;
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

    public void PlayerShoot(bool _shoot)
    {
        if (hasPlayerBehaviour)
        {
            shooting = _shoot;
        }
    }

    public void PlayerPressedPause()
    {
        GameManager.SP.LeaveGame();
    }

    public void SetControllImages(bool _value)
    {

        foreach (var item in fadeImages)
        {
            if (_value)
            {
                item.FadeIn(Random.Range(0.1f, 0.3f), true);
            }
            else
            {
                item.FadeOut(Random.Range(0.1f, 0.3f), true);
            }
        }

    }
}
