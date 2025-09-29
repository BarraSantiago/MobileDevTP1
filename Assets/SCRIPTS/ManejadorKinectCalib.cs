using UnityEngine;

public class ManejadorKinectCalib : MonoBehaviour
{
    public GameObject[] paraAct;

    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < paraAct.Length; i++) paraAct[i].SetActiveRecursively(false);
    }

    // Update is called once per frame
    private void Update()
    {
        //DISTINTAS CAMARAS
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            for (int i = 0; i < paraAct.Length; i++) paraAct[i].SetActiveRecursively(false);

            if (paraAct.Length >= 1)
                paraAct[0].SetActiveRecursively(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            for (int i = 0; i < paraAct.Length; i++) paraAct[i].SetActiveRecursively(false);

            if (paraAct.Length >= 2)
                paraAct[1].SetActiveRecursively(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            for (int i = 0; i < paraAct.Length; i++) paraAct[i].SetActiveRecursively(false);

            if (paraAct.Length >= 3)
                paraAct[2].SetActiveRecursively(true);
        }

        //SALE AL VIDEO DE INTRO
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Backspace) ||
            Input.GetKeyDown(KeyCode.KeypadEnter) ||
            Input.GetKeyDown(KeyCode.Mouse0))
            Application.LoadLevel(0);
        //SALIR
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        //REINICIAR
        if (Input.GetKeyDown(KeyCode.Mouse1) ||
            Input.GetKeyDown(KeyCode.Keypad0))
            Application.LoadLevel(Application.loadedLevel);
    }
}