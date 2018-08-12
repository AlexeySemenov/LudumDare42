using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    public class BoundaryController : MonoBehaviour
    {
        public float CrunchFactor = 0.1f;
        private Transform m_transform;
        // Use this for initialization
        private void Awake()
        {
            m_transform = transform;
        }
        void Start()
        {

        }
        private void OnTriggerExit(Collider other)
        {
            PlanetView planet = other.gameObject.GetComponent<PlanetView>();
            if (planet != null)
                planet.StartDesctruction();
        }
        // Update is called once per frame
        void Update()
        {
            m_transform.localScale = m_transform.localScale * (1.0f - CrunchFactor * Time.deltaTime);
        }
    }
}
