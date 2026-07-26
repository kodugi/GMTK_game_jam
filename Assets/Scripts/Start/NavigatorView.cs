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

        [SerializeField] private GameObject _explanationPanel;
        [SerializeField] private GameObject _creditsPanel;

        void Start()
        {
            _startButton.onClick.AddListener(() => SceneManager.LoadScene("ReservationScene"));
            _explanationButton.onClick.AddListener(HandleExplanationButtonClick);
            _creditsButton.onClick.AddListener(HandleCreditsButtonClick);
            _creditsPanel.SetActive(false);
            _explanationPanel.SetActive(true);
        }

        private void HandleExplanationButtonClick()
        {
            _creditsPanel.SetActive(false);
            _explanationPanel.SetActive(true);
        }

        private void HandleCreditsButtonClick()
        {
            _creditsPanel.SetActive(true);
            _explanationPanel.SetActive(false);
        }
    }
}