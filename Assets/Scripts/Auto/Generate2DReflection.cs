using UnityEngine;

namespace Auto
{
    public class Generate2DReflection : MonoBehaviour
    {
        public bool useRealtimeReflection;

        public int textureSize = 128;
        public LayerMask mask = 1 << 0;
        public RenderTexture rtex;
        public Material reflectingMaterial;

        public Texture staticCubemap;
        private Camera _cam;

        private void Start()
        {
            reflectingMaterial.SetTexture("_Cube", staticCubemap);
        }

        private void LateUpdate()
        {
            if (!useRealtimeReflection)
                return;

            if (Application.platform != RuntimePlatform.WindowsEditor &&
                Application.platform != RuntimePlatform.WindowsPlayer)
                UpdateReflection();
        }

        private void OnDisable()
        {
            if (rtex)
                Destroy(rtex);

            reflectingMaterial.SetTexture("_Cube", staticCubemap);
        }

        private void UpdateReflection()
        {
            if (!rtex)
            {
                rtex = new RenderTexture(textureSize, textureSize, 16);
                rtex.hideFlags = HideFlags.HideAndDontSave;
                rtex.isPowerOfTwo = true;
                rtex.isCubemap = true;
                rtex.useMipMap = false;
                rtex.Create();

                reflectingMaterial.SetTexture("_Cube", rtex);
            }

            if (!_cam)
            {
                GameObject go = new GameObject("CubemapCamera", typeof(Camera));
                go.hideFlags = HideFlags.HideAndDontSave;
                _cam = go.GetComponent<Camera>();
                // cam.nearClipPlane = 0.05f;
                _cam.farClipPlane = 150f;
                _cam.enabled = false;
                _cam.cullingMask = mask;
            }

            _cam.transform.position = Camera.main.transform.position;
            _cam.transform.rotation = Camera.main.transform.rotation;

            _cam.RenderToCubemap(rtex, 63);
        }
    }
}