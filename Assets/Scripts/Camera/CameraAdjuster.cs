using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _gameBorder;
    
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        AdjustCameraSizeToFitLevel();
    }

    private void AdjustCameraSizeToFitLevel()
    {
        var levelBounds = _gameBorder.bounds;

        var levelHeight = levelBounds.size.y;
        var levelWidth = levelBounds.size.x;

        var screenAspect = (float)Screen.width / (float)Screen.height;
        var levelAspect = levelWidth / levelHeight;

        if (levelAspect > screenAspect)
        {
            _mainCamera.orthographicSize = levelWidth / screenAspect / 2f;
        }
        else
        {
            _mainCamera.orthographicSize = levelHeight / 2f;
        }

        var levelCenter = levelBounds.center;
        _mainCamera.transform.position = new Vector3(levelCenter.x, levelCenter.y, _mainCamera.transform.position.z);
    }
}