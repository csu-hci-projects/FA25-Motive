using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerCameraSetup : NetworkBehaviour
{
    [Header("Camera target (usually a child on the player)")]
    public Transform cameraTarget;

    [Header("Top-down settings")]
    public float height = 10f;       // how high above the target
    public float yaw = 0f;           // rotate around Y if you want
    public float followLerp = 15f;   // how quickly camera follows

    private CinemachineCamera vcam;

    public override void OnNetworkSpawn()
    {
        var input = GetComponent<PlayerInput>();
        if (!IsOwner)
        {
            if (input != null) input.enabled = false;
            return;
        }

        if (input != null) input.enabled = true;

        vcam = FindFirstObjectByType<CinemachineCamera>();
        if (vcam == null)
        {
            Debug.LogWarning("PlayerCameraSetup: No CinemachineCamera found in scene.");
            return;
        }

        // Initialize immediately
        SnapCamera();
    }

    void LateUpdate()
    {
        if (!IsOwner || vcam == null) return;
        SmoothFollow();
    }

    private Transform Target => cameraTarget != null ? cameraTarget : transform;

    private void SnapCamera()
    {
        var t = Target;
        Vector3 desiredPos = t.position + Vector3.up * height;
        Quaternion desiredRot = Quaternion.Euler(90f, yaw, 0f);

        vcam.transform.SetPositionAndRotation(desiredPos, desiredRot);
    }

    private void SmoothFollow()
    {
        var t = Target;
        Vector3 desiredPos = t.position + Vector3.up * height;
        Quaternion desiredRot = Quaternion.Euler(90f, yaw, 0f);

        // Smoothly move and rotate the vcam
        vcam.transform.position = Vector3.Lerp(
            vcam.transform.position, desiredPos, followLerp * Time.deltaTime);

        vcam.transform.rotation = Quaternion.Slerp(
            vcam.transform.rotation, desiredRot, followLerp * Time.deltaTime);
    }
}
