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
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        loadSceneBtn.onClick.AddListener(ChangeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        //operation.allowSceneActivation = true;
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene() {
        operation.allowSceneActivation = true;
        //SceneManager.LoadSceneAsync(sceneName); 
    }

}
