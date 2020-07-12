using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerBehaviour pb;
    private Slider sl;
    private float maxValue;
    private float sliderValue;
    private float goalSliderValue;
    private List<PlayerBehaviour> playersDoneDamage;

    [SerializeField] private float health;
    [Space]
    [SerializeField] [Tooltip("The image of the hp bar slider filler")]private ImageFade healthBarFiller;
    [SerializeField] [Tooltip("The image of the hp bar slider")] private ImageFade healthBarImage;


    private void Awake()
    {
        sl = GetComponentInChildren<Slider>();
        pb = GetComponent<PlayerBehaviour>();
        playersDoneDamage = new List<PlayerBehaviour>();
    }


    public void SetInitValues(float maxPlayerHealth)
    {
        health = maxPlayerHealth;
        maxValue = maxPlayerHealth;
        sliderValue = maxValue;
        sl.maxValue = maxValue;
        sl.value = maxValue;
        UpdatePlayerHealthBar(maxValue);
    }

    private void UpdatePlayerHealthBar(float _newHealth)
    {
        goalSliderValue = _newHealth;

        //Show slider
    }

    public void PlayerHit(PlayerBehaviour _damageDealer, float _damage)
    {
        if (!playersDoneDamage.Contains(_damageDealer) && _damageDealer != pb)
        {
            playersDoneDamage.Add(_damageDealer);
        }

        health -= _damage;
        goalSliderValue = health;
        UpdatePlayerHealthBar(health);

        healthBarFiller.StartFade(0.9f);
        healthBarImage.StartFade(0.9f);

        pb.Ping(40, 0.2f);

        if (health <= 0)
        {
            if (pb.IsAlive)
            {
                foreach (PlayerBehaviour play in playersDoneDamage)
                {
                    if (play != _damageDealer)
                    {
                        //Give the damagedealer a kill
                        play.AssistOnPlayer(pb);
                    }
                }

                _damageDealer.KilledPlayer(pb);
                if (pb.photonView.IsMine)
                {
                    pb.PlayerDie();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        sliderValue = Mathf.Lerp(sliderValue, goalSliderValue, Time.deltaTime * 5);
        sl.value = sliderValue;
    }

    public PlayerBehaviour GetPlayer => pb;

}
