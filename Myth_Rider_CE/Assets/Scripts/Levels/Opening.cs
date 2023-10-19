using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    [Header("Config")]
    [Tooltip("Config")]
    [SerializeField] private float _endTime = 60f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(VideoEnd(_endTime));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene(1);
        }

    }
    private IEnumerator VideoEnd(float endTime)
    {
        yield return new WaitForSeconds(endTime);

        SceneManager.LoadScene(1);
    }


}
