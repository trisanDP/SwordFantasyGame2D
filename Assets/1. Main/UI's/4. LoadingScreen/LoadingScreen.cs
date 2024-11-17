using UnityEngine;
using UnityEngine.UI;

namespace BrokenLands
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]GameObject LoadingWindow;
        [SerializeField] Slider slider;

        void Start() {
            slider = LoadingWindow.GetComponentInChildren<Slider>();
        }
    }
}
