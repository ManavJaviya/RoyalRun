using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpSYS; 
    [SerializeField] float minFOV = 35f;
    [SerializeField] float maxFOV = 85f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;

    CinemachineCamera cinemachineCamera;
    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void changeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0)
        {
            speedUpSYS.Play();
        }
    }

    IEnumerator ChangeFOVRoutine (float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp( startFOV + speedAmount * zoomSpeedModifier , minFOV, maxFOV);

        float elaspTime = 0f;
        while (elaspTime < zoomDuration)
        {
            float t = elaspTime / zoomDuration;
            elaspTime += Time.deltaTime;

            cinemachineCamera.Lens.FieldOfView = math.lerp(startFOV , targetFOV , t);
            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }

}
