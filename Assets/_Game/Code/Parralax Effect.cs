using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class ParralaxEffect : MonoBehaviour
{

    public Camera mainCamera;
    public Transform followTarget;


    Vector2 startingPosition;  // The initial position of the parallax object

    float startingCameraPositionY;  // The initial Z position of the camera
    float YDistanceFromTarget => transform.position.y - followTarget.position.x; // The distance of the parallax object from the target
    float ClippingPlane => (mainCamera.transform.position.z +(YDistanceFromTarget > 0 ? mainCamera.farClipPlane : mainCamera.nearClipPlane)); // The clipping plane of the camera 


    float ParralaxFactor =>Mathf.Abs(YDistanceFromTarget) / ClippingPlane; // The factor by which the parallax effect is applied
    Vector2 CameraMovementStart // The movement of the camera since the start
    {
        get
        {
            Vector2 cameraMovement = (Vector2)mainCamera.transform.position - startingPosition;
            return cameraMovement;
        }
    }

    void Start()
    {
       startingPosition = transform.position;
       startingCameraPositionY = mainCamera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + CameraMovementStart * ParralaxFactor;
        transform.position = new Vector2(newPosition.x, startingCameraPositionY);
    }
}
