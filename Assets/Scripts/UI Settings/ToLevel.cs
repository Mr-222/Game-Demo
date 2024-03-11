using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel : MonoBehaviour
{
    public float toleveltimer = 3f;

    string levelstr;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToLevelfun(string levelname)
    {
        Time.timeScale = 1;
        levelstr = levelname;
        Invoke("Levelback", toleveltimer);
    }

    void Levelback()
    {
        SceneManager.LoadScene(levelstr);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Stopfun()
    {
        Time.timeScale = 0;
    }
    public void Startfun()
    {
        Time.timeScale = 1;
    }
}
