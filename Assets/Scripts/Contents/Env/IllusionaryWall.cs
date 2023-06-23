using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionaryWall : MonoBehaviour
{
    public bool wallHasBennHit;
    public Material illusionaryWallMaterial;
    public float alpha;
    public float fadetimer = 2.5f;
    BoxCollider wallCollider;

    AudioSource audioSource;
    public AudioClip illusionarWallSound;

    private void Awake()
    {
        //illusionaryWallMaterial = GetComponent<Material>();
        wallCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = illusionarWallSound;
        illusionaryWallMaterial.color = new Color(1, 1, 1, 255);
    }

    private void Update()
    {
        if(wallHasBennHit)
        {
            FadeIllusionaryWall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void FadeIllusionaryWall()
    {
        alpha = illusionaryWallMaterial.color.a;
        alpha = alpha - Time.deltaTime / fadetimer;
        Color fadedWallColor = new Color(1, 1, 1, alpha);
        illusionaryWallMaterial.color = fadedWallColor;

        if(wallCollider.enabled)
        {
            wallCollider.enabled = false;
            audioSource.PlayOneShot(illusionarWallSound);
        }

        if(alpha <= 0)
        {
            Destroy(this);
        }
    }
}
