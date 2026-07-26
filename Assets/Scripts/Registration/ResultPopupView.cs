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

        public virtual void Initialize()
        {
            _closeButton.onClick.AddListener(() => SceneManager.LoadScene("Shop"));
            _messageText.text = "";
            _closeButton.gameObject.SetActive(false);
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
            
            _closeButton.gameObject.SetActive(true);
        }
    }
}