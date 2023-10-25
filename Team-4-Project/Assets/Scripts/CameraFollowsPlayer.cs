using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    Transform tf;

    Vector3 cameraOffset = new Vector3(-5, 5, -5);

    private void Awake()
    {
        tf = GetComponent<Transform>();
    }

    void Update()
    {
        tf.position = playerTransform.position + cameraOffset;
    }
}
