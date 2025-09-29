using UnityEngine;

namespace Escenas.VideoIntro
{
    public class VidIntrMgr : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            Screen.lockCursor = true;
            Cursor.visible = false;
        }

        // Update is called once per frame
        private void Update()
        {
            //PARA JUGAR
            if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
                Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.Mouse0))
                Application.LoadLevel(1); //el juego

            //REINICIAR
            if (Input.GetKeyDown(KeyCode.Mouse1) ||
                Input.GetKeyDown(KeyCode.Keypad0))
                Application.LoadLevel(Application.loadedLevel);

            //CIERRA LA APLICACION
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            //CALIBRACION DEL KINECT
            if (Input.GetKeyDown(KeyCode.Backspace)) Application.LoadLevel(3);
        }
    }
}