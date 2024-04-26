using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Singleton
{

    #region singleton section
    private Singleton() { }


    public static int mode { get; set;}
    public static int score{get;set;}
    public static int clicks{get;set;}
    public static int lives { get; set; } = 3;

    private static Singleton instance = null;
    
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
    
    public static void UpdateLivesText()
    {
        if (lives <= 0) {
            if (SceneManager.GetActiveScene().name != "Scene 2")
            {
                SceneManager.LoadScene("Scene 2");
            }
            else //this _feels_ jank
            {
                GameObject.Find("Score").GetComponent<Text>().text = "Score:" + score;
                GameObject.Find("Clicks").GetComponent<Text>().text = "Clicks:" + clicks;
            }
		}
        else
        {
			GameObject.Find("Lives").GetComponent<Text>().text = "Lives:" + lives;
		}
	}

    #endregion 



    #region Events

    // Declare event for notifying observers
    public static event Action<int> OnEventOccurred;
	public static event Action OnShoot;
	public static event Action OnLifeDown;

	// Notifies all observers when an event occurs
	public static void RaiseEvent(int eventData)
    {
        OnEventOccurred?.Invoke(eventData);
    }

	public static void Shoot()
	{
        clicks++;
		OnShoot?.Invoke();
	}
    public static void TakeLife()
	{
		OnLifeDown?.Invoke();
	}



	#endregion
}
