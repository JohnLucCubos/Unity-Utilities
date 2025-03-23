using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyUnity.Utilities
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] int sceneNumber;
        public void OnClick() => SceneManager.LoadScene(sceneNumber);
    }
}
