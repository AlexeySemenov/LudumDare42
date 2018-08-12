using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Antisystems.BigCrunch
{
    public class LoadMainLevel : MonoBehaviour
    {

        // Use this for initialization
        public void LoadLevel(int level)
        {
            SceneManager.LoadScene(level);
        }
    }
}
