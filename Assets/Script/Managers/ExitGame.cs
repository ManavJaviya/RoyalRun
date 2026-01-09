using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("EXIT");
            Exit();
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
