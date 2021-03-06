using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UISceneChangerButton : MonoBehaviour
{

    public AsyncOperation operation;
    public Button loadSceneBtn;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.visible = true;
        //operation = SceneManager.LoadSceneAsync(sceneName);
        //operation.allowSceneActivation = false;

        loadSceneBtn.onClick.AddListener(ChangeScene);
    }


    public void ChangeScene() {
        //operation.allowSceneActivation = true;
        //SceneManager.LoadSceneAsync(sceneName); 

        SceneManager.LoadScene(sceneName);
    }

}
