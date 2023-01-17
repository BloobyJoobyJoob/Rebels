using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerBody : MonoBehaviour
{
    public AudioSource shootSFX;
    public AudioClip[] shootClips;
    bool shooting = false;
    bool inputFire;

    Animator anm;

    int oldBodyState;

    int bodyState;

    int fire = Animator.StringToHash("BodyFire");
    int pistel = Animator.StringToHash("BodyPistel");
    void Awake()
    {
        anm = GetComponent<Animator>();
    }
    private void Update()
    {
        bodyState = GetBodyState();
        if (bodyState != oldBodyState)
        {
            anm.CrossFade(bodyState, 0);
            oldBodyState = bodyState;
        }

        transform.rotation = Quaternion.AngleAxis(PlayerController.ClientSingleton.angleToMouse + 90, Vector3.forward);
    }

    public void ShootFinished()
    {
        shooting = false;

        if (inputFire)
        {
            FireShot();
        }
    }

    private int GetBodyState()
    {
        if (inputFire || shooting) return fire;

        return pistel;
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        inputFire = context.action.IsPressed();
        FireShot();
    }

    private void FireShot()
    {
        if (shooting == false)
        {
            shootSFX.PlayOneShot(shootClips[Random.Range(0, shootClips.Length)]);
            shooting = true;
        }
    }
}