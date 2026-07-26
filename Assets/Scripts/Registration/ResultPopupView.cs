using System.Collections;
using CourseNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class ResultPopupView: MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _messageText;
        [SerializeField] protected Button _closeButton;
        [SerializeField] private TextMeshProUGUI _resultText;
        
        private SceneTransitionManager _sceneTransitionManager;
        
        public virtual void Initialize(SceneTransitionManager sceneTransitionManager)
        {
            _messageText.text = "";
            _resultText.text = "";
            _closeButton.gameObject.SetActive(false);
            _sceneTransitionManager = sceneTransitionManager;
        }

        public void ShowResult(RegistrationResult result)
        {
            gameObject.SetActive(true);
            StartCoroutine(ShowResultCoroutine(result));
        }

        private IEnumerator ShowResultCoroutine(RegistrationResult result)
        {
            float interval = 0.5f;
            foreach ((Course course, float points) in result.PointsPerCourse)
            {
                _messageText.text += course.CourseName + ": ";
                yield return new WaitForSeconds(interval);
                _messageText.text += points.ToString("F2") + " points\n";
                yield return new WaitForSeconds(interval);
            }

            _messageText.text += "Extra points: ";
            yield return new WaitForSeconds(interval);
            _messageText.text += result.ExtraPoint.ToString("F2") + " points\n";
            yield return new WaitForSeconds(interval);
            
            _messageText.text += "Total: ";
            yield return new WaitForSeconds(interval);
            _messageText.text += result.Total + " points\n";
            yield return new WaitForSeconds(interval);

            if (result.Success)
            {
                _messageText.text += "Semester Complete";
                _closeButton.onClick.AddListener(() => HandleCloseButtonClick(true));
            }
            else
            {
                _messageText.text += "Game Over";
                _closeButton.onClick.AddListener(() => HandleCloseButtonClick(false));
            }
            _closeButton.gameObject.SetActive(true);
        }

        private void HandleCloseButtonClick(bool result)
        {
            if (result)
            {
                _sceneTransitionManager.NextRound();
            }
            else
            {
                _sceneTransitionManager.GameOver();
            }
        }
    }
}