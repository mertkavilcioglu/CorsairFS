
using UnityEngine;
using UnityEngine.InputSystem;

public class CockpitLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 0.15f;

    [Header("Horizontal Limits")]
    [SerializeField] private float minYaw = -120f;
    [SerializeField] private float maxYaw = 120f;

    [Header("Vertical Limits")]
    [SerializeField] private float minPitch = -60f;
    [SerializeField] private float maxPitch = 75f;

    private float yaw;
    private float pitch;

    void Start()
    {
        Vector3 currentRotation = transform.localEulerAngles;

        yaw = currentRotation.y;
        pitch = currentRotation.x;

        if (pitch > 180f)
            pitch -= 360f;

        LockCursor();
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
            LockCursor();

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * sensitivity;
        pitch -= mouseDelta.y * sensitivity;

        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
