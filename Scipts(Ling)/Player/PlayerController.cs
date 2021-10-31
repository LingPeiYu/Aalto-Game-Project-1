using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Transform normalPosition;
    [SerializeField]
    private float normalPerspectiveAngle = 60f;
    [SerializeField]
    [Range(0, 5)]
    private float normalXSensitivity = 2.4f;
    [SerializeField]
    [Range(0, 5)]
    private float normalYSensitivity = 2.4f;
    [SerializeField]
    private Transform aimPosition;
    [SerializeField]
    private float aimPerspectiveAngle = 20f;
    [SerializeField]
    [Range(0, 5)]
    private float aimXSensitivity = 1f;
    [SerializeField]
    [Range(0, 5)]
    private float aimYSensitivity = 1f;
    [SerializeField]
    private float positionChangeSpeed = 10f;
    [SerializeField]
    private float perspectiveChangeSpeed = 10f;


    [SerializeField]
    private float lookUpLimit = 60f;
    [SerializeField]
    private float lookDownLimit = 60f;

    private float mouseXSensitivity;
    private float mouseYSensitivity;

    [Header("Weapon")]
    [SerializeField]
    private Weapon mainWeapon;
    [SerializeField]
    private Weapon superWeapon;

    private float xRotation;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        mouseXSensitivity = normalXSensitivity;
        mouseYSensitivity = normalYSensitivity;

        xRotation = 0f;
        yRotation = 0f;
    }

    void Update()
    {
        //Aim
        if (Input.GetButton("Fire2")) AimOn();
        else AimOff();
        //Fire
        if (Input.GetButton("Fire1") && mainWeapon.IsCool()) mainWeapon.Fire();
        if (Input.GetKeyDown(KeyCode.M)&& superWeapon.IsCool()) superWeapon.Fire();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        yRotation += Input.GetAxis("Mouse X") * mouseXSensitivity;
        xRotation += Input.GetAxis("Mouse Y") * mouseYSensitivity;

        //Debug.Log(xRotation);
        xRotation = ClampAngle(xRotation,-1*lookUpLimit,lookDownLimit);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    private void AimOn()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, aimPosition.position, positionChangeSpeed * Time.deltaTime);
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, aimPerspectiveAngle, perspectiveChangeSpeed * Time.deltaTime);
        mouseXSensitivity = aimXSensitivity;
        mouseYSensitivity = aimYSensitivity;
    }

    private void AimOff()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, normalPosition.position, positionChangeSpeed * Time.deltaTime);
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalPerspectiveAngle, perspectiveChangeSpeed * Time.deltaTime);
        mouseXSensitivity = normalXSensitivity;
        mouseYSensitivity = normalYSensitivity;
    }

}
