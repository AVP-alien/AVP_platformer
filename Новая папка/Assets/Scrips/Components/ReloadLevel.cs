using AVPplatformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AVPplatformer.Components
{
    public class ReloadLevel : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            Destroy (session.gameObject);

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }

}


