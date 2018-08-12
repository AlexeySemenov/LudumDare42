using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeView : MonoBehaviour {

    public ParticleSystem DestructionParticles;
    public Texture2D Texture;
    public Color Color = Color.white;
    public float RotationSpeed = 1;
    public AudioSource DestructSound;

    private Material m_material;
    private Transform m_transform;
	void Awake () {
        m_material = GetComponentInChildren<MeshRenderer>().material;
        m_transform = transform;
        DestructionParticles.Stop();
        UpdateWormhole();

    }
    public void StartDesctruction()
    {
        StartCoroutine(Destruct());
    }
    IEnumerator Destruct()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        DestructionParticles.Play();
        DestructSound.Play();
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
    public void UpdateWormhole()
    {
        m_material.mainTexture = Texture;
        m_material.color = Color;
    }
	void Update () {
        m_transform.RotateAround(transform.up, RotationSpeed * Time.deltaTime);
	}
}
