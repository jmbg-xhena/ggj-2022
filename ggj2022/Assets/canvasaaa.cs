using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasaaa : MonoBehaviour
{
    // Start is called before the first frame update
    public void Over()
    {
        SceneManager.LoadScene("GameOver");
    }
}
