using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float horizontalSpeed = 5f;
    public float roadWidth = 1.5f;
    public float gas = 100f;
    public float gasUsageRate = 10f;
    public float maxGasGauge = 100f;
    public Slider gasGaugeSlider;
    public GameObject gameOverUI;
    
    private Vector3 moveDirection;
    private bool isGameOver = false;
    
    private void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        
        if (gasGaugeSlider != null)
        {
            gasGaugeSlider.maxValue = maxGasGauge;
            gasGaugeSlider.value = gas;
        }
    }

    void Update()
    {
        if (isGameOver) return;
        
        transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);
        
        TouchInput();

        gas -= gasUsageRate * Time.deltaTime;

        if (gasGaugeSlider != null)
        {
            gasGaugeSlider.value = gas;
        }
        
        if (gas <= 0)
        {
            GameOver();
        }
    }

    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    moveDirection = Vector3.left * horizontalSpeed;
                }
                else
                {
                    moveDirection = Vector3.right * horizontalSpeed;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            if (mousePosition.x < Screen.width / 2)
            {
                moveDirection = Vector3.forward * horizontalSpeed;
            }
            else
            {
                moveDirection = -Vector3.forward * horizontalSpeed;
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        
        transform.Translate(moveDirection * Time.deltaTime);
        float clampedZ = Mathf.Clamp(transform.position.z, -roadWidth, roadWidth);
        transform.position = new Vector3(transform.position.x, transform.position.y, clampedZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gas"))
        {
            gas += 30f;

            if (gas > gasGaugeSlider.maxValue)
            {
                gas = gasGaugeSlider.maxValue;
            }

            if (gasGaugeSlider != null)
            {
                gasGaugeSlider.value = gas;
            }
            
            Destroy(other.gameObject);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;
        
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }
    
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Title()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Title");
    }
}
