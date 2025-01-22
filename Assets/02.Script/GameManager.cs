using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("StartGame called");
        SceneManager.LoadScene("CarRacing");
    }
}