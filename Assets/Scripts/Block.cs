using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    public bool IsAlive;
    public Transform BlockTransform;
    public float ScaleDuration;
    public Tween currentTween;

    //Menu Objects
    public GameObject DrawCntrl;
    public GameObject WinMenu;
    public GameObject GameOverMenu;

    // Particle System Variables
    public ParticleSystem Confetti;
    public ParticleSystem Electric;


    // Audios and Effects
    public AudioSource CubeSpawn;
    public AudioSource FailedSquare;
    public AudioSource FullSquare;
    public AudioSource FinishedLevel;


    private void Awake()
    {
        //WinMenu = GameObject.Find("WinMenu");
        DrawCntrl = GameObject.Find("Grid");
        BlockTransform = transform.GetChild(0);
        //Confetti = GameObject.Find("Confetti");
    }

    public void MakeBlock()
    {
        if(currentTween.IsPlaying())
            currentTween.Kill();
        IsAlive = true;
        currentTween = BlockTransform.DOScale(0.98f, ScaleDuration).SetEase(Ease.InOutElastic);
        CubeSpawn.Play();
    }

    public void KillBlock()
    {
        IsAlive = false;
        currentTween = BlockTransform.DOScale(0, ScaleDuration / 2f).SetEase(Ease.InSine);
    }

    // Change Material script when grid enters the obstacle
    public void StopParticle()
    {
        Electric.Stop();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            if (IsAlive != other.transform.GetComponent<Obstacle>().IsAlive)    // If alive then; block shouldn't be alive since we are passing from empty zones.
            {

                // TODO: Spawn success particle
                Electric.Play();
            }
            else
            {
                //Debug.LogError(other.gameObject.GetInstanceID());
                FailedSquare.Play();
                DrawCntrl.SetActive(false); // Stop the movement of the Grid (Player)
                GameOverMenu.SetActive(true);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (other.transform.CompareTag("DrawResetter"))
        {
            FullSquare.Play();
            Invoke("StopParticle",0.3f);
            transform.parent.GetComponent<DrawController>().ClearGrid();

        }
        else if (other.transform.CompareTag("FinishLine"))
        {
            FinishedLevel.Play(); // Sound Effect
            DrawCntrl.SetActive(false); // Stop the movement of the Grid (Player)
            WinMenu.SetActive(true);
            Confetti.Play();
            transform.parent.GetComponent<DrawController>().ClearGrid();
        }

    }
}
