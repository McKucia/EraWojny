using FishNet.Object;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float cameraBound;

    Vector3 cameraFollowPosition = Vector3.zero;
    const float maxSpeed = 80f;
    const float accelSpeed = 10f;
    float scrollSpeed = 0;

    public void Init()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {   
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        transform.rotation = Quaternion.identity;
        float screenEdge = Screen.width / 200f;

        scrollSpeed = Mathf.SmoothStep(scrollSpeed, maxSpeed, Time.deltaTime * accelSpeed);

        if (Input.mousePosition.x > Screen.width - screenEdge)
            cameraFollowPosition.x += scrollSpeed * Time.deltaTime;
        else if (Input.mousePosition.x < screenEdge)
            cameraFollowPosition.x -= scrollSpeed * Time.deltaTime;
        else
            scrollSpeed = 0f;

        cameraFollowPosition.x = Mathf.Clamp(cameraFollowPosition.x, -cameraBound, cameraBound);
        transform.position = cameraFollowPosition;
    }
}
