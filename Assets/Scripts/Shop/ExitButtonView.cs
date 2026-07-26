using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShopNameSpace
{
    public class ExitButtonView: MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("ReservationScene"));
        }
    }
}