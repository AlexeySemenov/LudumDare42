using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    
    public class PlanetView : MonoBehaviour
    {
        public float Radius = 10;
        public Color Color = Color.white;
        public ParticleSystem DestructionParticles;
        public AudioSource Explosion;

        private Transform m_transform;
        private Material m_material;
        private ParticleSystem.EmissionModule m_destructEmission;
        
        public void UpdatePlanet()
        {
            transform.localScale = new Vector3(Radius, Radius, Radius);
            m_material.color = Color;
        }
        public void StartDesctruction()
        {
            StartCoroutine(Destruct());
        }
        IEnumerator Destruct()
        {
            GetComponent<MeshRenderer>().enabled = false;
            DestructionParticles.Play();
            Explosion.Play();
            yield return new WaitForSeconds(10.0f);
            Destroy(gameObject);
        }
        // Use this for initialization
        void Awake()
        {
            m_transform = transform;
            m_material = GetComponent<MeshRenderer>().material;
            m_destructEmission = DestructionParticles.emission;
            DestructionParticles.Stop();
        }
        private void Start()
        {
            UpdatePlanet();
        }


        
    }
}