using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    public class PlanetGenerator : MonoBehaviour
    {

        public GameObject PlanetPrefab;

        public float PositionRadius = 100.0f;

        private List<GameObject> m_planets;
        private void Awake()
        {
            m_planets = new List<GameObject>();
        }
        void Start()
        {
            for(int i = 0; i < 10; i++)
                GenerateNext();
        }

        public void GenerateNext()
        {
            Vector2 position = Random.insideUnitCircle * PositionRadius;
            float radius = 10; ; 
            Color color = Random.ColorHSV(0.0f, 0.1f, 0.7f, 0.9f, 0.8f, 1, 1, 1);

            GameObject planet = Instantiate<GameObject>(PlanetPrefab, 
                new Vector3(position.x, 0, position.y), Random.rotation);

            bool notFit = true;
            int numPlanets = m_planets.Count;
            while (notFit)
            {
                radius = Random.Range(5, 100);
                position = Random.insideUnitCircle * PositionRadius;
                notFit = false;
                for(int i = 0; i < numPlanets; ++i)
                {
                    float radDist = m_planets[i].GetComponent<PlanetView>().Radius + radius;
                    float dist = Vector3.Distance(position, m_planets[i].transform.localPosition);
                    notFit = dist <= radDist;
                    if (notFit)
                        break;
                }
            }
            planet.transform.localPosition = new Vector3(position.x, 0, position.y);
            planet.GetComponent<PlanetView>().Radius = radius;
            planet.GetComponent<PlanetView>().Color = color;
            planet.GetComponent<PlanetView>().UpdatePlanet();
            planet.transform.SetParent(transform.parent, false);
            m_planets.Add(planet);
        }
    }
}