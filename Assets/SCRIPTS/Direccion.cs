using UnityEngine;

public class Direccion : MonoBehaviour
{
    public Transform manoDer;
    public Transform manoIzq;

    public float difMin;
    public float difMax;
    public float sensibilidad = 1;

    public Transform camion; //lo que va a conducir
    public Transform volante;
    private Vector3 _aux;
    private float _diferencia;

    private Sentido _dirAct;

    //---------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (manoIzq.position.y > manoDer.position.y)
        {
            _dirAct = Sentido.Der;
            _diferencia = manoIzq.position.y - manoDer.position.y;
        }
        else
        {
            _dirAct = Sentido.Izq;
            _diferencia = manoDer.position.y - manoIzq.position.y;
        }

        if (_diferencia > difMin && _diferencia < difMax)
            switch (_dirAct)
            {
                case Sentido.Der:
                    camion.rotation *= Quaternion.AngleAxis(_diferencia * sensibilidad * Time.deltaTime, camion.up);
                    volante.localRotation *=
                        Quaternion.AngleAxis(_diferencia * sensibilidad * Time.deltaTime, Vector3.up);
                    //Aux = Volante.localEulerAngles;
                    //Aux.x += Diferencia * Sensibilidad;
                    //Volante.localEulerAngles = Aux;
                    break;

                case Sentido.Izq:
                    camion.rotation *= Quaternion.AngleAxis(-1 * _diferencia * sensibilidad * Time.deltaTime, camion.up);
                    volante.localRotation *=
                        Quaternion.AngleAxis(-1 * _diferencia * sensibilidad * Time.deltaTime, Vector3.up);
                    //Aux = Volante.localEulerAngles;
                    //Aux.x -= Diferencia * Sensibilidad;
                    //Volante.localEulerAngles = Aux;
                    break;
            }
    }


    private enum Sentido
    {
        Der,
        Izq
    }
}