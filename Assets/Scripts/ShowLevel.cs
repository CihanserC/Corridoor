using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowLevel : MonoBehaviour
{
    public Text LevelText;
    // Start is called before the first frame update
    void Start()
    {
        LevelText.text = SceneManager.GetActiveScene().name;
        Debug.Log(LevelText.text);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
