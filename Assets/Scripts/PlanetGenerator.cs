using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    public class PlanetGenerator : MonoBehaviour
    {
        public bool IsLastUniverse = false;
        public GameObject PlanetPrefab;
        public GameObject WormholePrefab;
        public Texture2D[] HoleTex;

        public float PositionRadius = 100.0f;

        private List<GameObject> m_planets;
        private List<GameObject> m_holes;

        public Vector3 GetHolePosition()
        {
            for(int i = 0; i < m_holes.Count; ++i)
            {
                if (m_holes[i] != null)
                {
                    {
                        Vector3 position = m_holes[i].transform.position;
                        DestroyImmediate(m_holes[i]);
                        return position;
                    }
                }
            }
            return Vector3.zero;
        }
        private void Awake()
        {
            m_planets = new List<GameObject>();
            m_holes = new List<GameObject>();
        }
        void Start()
        {
            for (int i = 0; i < 10; i++)
                GenerateNextPlanet();

            int holesNum = 4;
            if (IsLastUniverse)
                holesNum *= 4;
            for (int i = 0; i < holesNum; i++)
                GenerateNextWormhole();
        }

        public void GenerateNextPlanet()
        {
            Vector2 position = Random.insideUnitCircle * PositionRadius;
            float radius = 10;
            Color color = Random.ColorHSV(0.0f, 0.1f, 0.7f, 0.9f, 0.8f, 1, 1, 1);

            GameObject planet = Instantiate<GameObject>(PlanetPrefab,
                new Vector3(position.x, 0, position.y), Random.rotation);

            bool notFit = true;
            int numPlanets = m_planets.Count;
            int maxIter = 1000;
            while (notFit)
            {
                radius = Random.Range(5, 100);
                position = Random.insideUnitCircle * PositionRadius;
                notFit = false;
                for (int i = 0; i < numPlanets; ++i)
                {
                    float radDist = m_planets[i].GetComponent<PlanetView>().Radius * 2 + radius * 2;
                    float dist = Vector3.Distance(position, m_planets[i].transform.localPosition);
                    notFit = dist <= radDist;
                    if (notFit)
                        break;
                }
                maxIter--;
                if (maxIter == 0)
                    break;
            }
            planet.transform.localPosition = new Vector3(position.x, 0, position.y);
            planet.GetComponent<PlanetView>().Radius = radius;
            planet.GetComponent<PlanetView>().Color = color;
            planet.GetComponent<PlanetView>().UpdatePlanet();
            planet.transform.SetParent(transform.parent, false);
            m_planets.Add(planet);
        }
        public void GenerateNextWormhole()
        {
            int texNum = HoleTex.Length;
            int texIndex = Mathf.RoundToInt(Random.Range(0, texNum));
            Texture2D hTex = HoleTex[texIndex];
            Vector2 position = Random.insideUnitCircle * PositionRadius / 4;
            float radius = 5;
            Color color = Random.ColorHSV(0.0f, 0.1f, 0.7f, 0.9f, 0.8f, 1, 1, 1);

            GameObject hole = Instantiate<GameObject>(WormholePrefab,
                new Vector3(position.x, 0, position.y), Quaternion.identity);

            bool notFit = true;
            int numPlanets = m_planets.Count;
            int maxIter = 1000;
            while (notFit)
            {
                position = Random.insideUnitCircle * PositionRadius;
                notFit = false;
                for (int i = 0; i < numPlanets; ++i)
                {
                    float radDist = m_planets[i].GetComponent<PlanetView>().Radius * 2 + radius * 2;
                    float dist = Vector3.Distance(position, m_planets[i].transform.localPosition);
                    notFit = dist <= radDist;
                    if (notFit)
                        break;
                }
                maxIter--;
                if (maxIter == 0)
                    break;
            }
            hole.transform.localPosition = new Vector3(position.x, 0, position.y);
            hole.GetComponent<WormholeView>().Texture = hTex;
            hole.GetComponent<WormholeView>().UpdateWormhole();
           hole.transform.SetParent(transform.parent, false);
            m_holes.Add(hole);
        }
    }
}