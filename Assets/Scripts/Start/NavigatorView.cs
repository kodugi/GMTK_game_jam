using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StartNameSpace
{
    public class NavigatorView: MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _explanationButton;
        [SerializeField] private Button _creditsButton;

        void Start()
        {
            _startButton.onClick.AddListener(() => SceneManager.LoadScene("ReservationScene"));
        }
    }
}