using FishNet.Object;
using UnityEngine;

public class CameraMovement : NetworkBehaviour
{
    Vector3 cameraFollowPosition = Vector3.zero;
    const float cameraBound = 32f;
    const float maxSpeed = 50f;
    const float accelSpeed = 7f;
    float scrollSpeed = 0;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner)
            gameObject.SetActive(true);
    }

    void Update()
    {
        if (!base.IsClient)
            return;
        
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
