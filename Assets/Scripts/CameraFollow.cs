using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float m_speed = 0.5f;
    Camera mycam;
    private static bool cameraExists;

    // Use this for initialization
    void Start()
    {

        mycam = GetComponent<Camera>();
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            transform.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        mycam.orthographicSize = (Screen.height / 100f) / 4f;

        if (target)
        {

            transform.position = Vector3.Lerp(transform.position, target.position, m_speed) + new Vector3(0, 0, -10);

        }

    }
}
