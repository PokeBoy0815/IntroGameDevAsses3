using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickLoadLevel1()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync(1);
        
    }
    
    //only aplicable if HD section gets finished
    public void OnclickLoadLevel2()
    {
        Debug.Log("LEVEL 2 START");
        /*
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync(2);
        */
    }
    
}
