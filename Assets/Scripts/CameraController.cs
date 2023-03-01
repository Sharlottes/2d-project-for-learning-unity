using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        public static CameraController Instance => _instance;

        private new Camera camera;
        private float _zoomAmount = 3;
        public float ZoomAmount
        {
            get => _zoomAmount;
            set
            {
                _zoomAmount = value;
                UpdateCameraZoom();
            }
        }

        private void Awake()
        {
            _instance = this;
            camera = GetComponent<Camera>();

            ZoomOutAnimation(10); // 엄
        }

        private IEnumerator ZoomOutCoroutine(float amount, float duration = 1)
        {
            float t = 0;

            while (true)
            {
                t += Time.deltaTime;
                ZoomAmount += amount / t * Time.deltaTime;
                if (t > duration) break;
                yield return null;
            }
        }
        public void ZoomOutAnimation(float amount, float duration = 1)
        {
            StartCoroutine(ZoomOutCoroutine(amount, duration));
        }
        public void UpdateCameraZoom()
        {
            camera.transform.position = new(camera.transform.position.x, camera.transform.position.y, -_zoomAmount);
        }
    }
}