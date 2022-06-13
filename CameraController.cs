using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;  // the object to look at (typically the player)
    // how far the looked at object can go before snapping the camera to it
    public float boundX = 0.05f;
    public float boundY = 0.05f;

    // is called after FixedUpdate()
    private void LateUpdate() {
        Vector2 delta = Vector2.zero;  // movement difference of lookAt between current frame and the last frame

        // check if LookAt is out of bounds on X axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (boundX < deltaX || deltaX < -boundX) {
            // players is out of bounds
            if (transform.position.x < lookAt.position.x) {
                // player is on the right
                delta.x = deltaX - boundX;
            } else {
                // player is on the left
                delta.x = deltaX + boundX;
            }
        }

        // check if LookAt is out of bounds on Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (boundY < deltaY || deltaY < -boundY) {
            // players is out of bounds
            if (transform.position.y < lookAt.position.y) {
                // player is on the right
                delta.y = deltaY - boundY;
            } else {
                // player is on the left
                delta.y = deltaY + boundY;
            }
        }

        // move the camera
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
