using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UISceneChangerButton : MonoBehaviour
{
    public Button loadSceneBtn;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        loadSceneBtn.onClick.AddListener(ChangeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        SceneManager.LoadScene("Trench", LoadSceneMode.Single);
    }

    public void ChangeScene() { SceneManager.LoadSceneAsync(sceneName); }


}
