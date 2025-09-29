using UnityEngine;

public class ControlDireccion : MonoBehaviour
{
    public enum Sentido
    {
        Der,
        Izq
    }

    public enum TipoInput
    {
        Mouse,
        Kinect,
        Awsd,
        Arrows
    }

    public TipoInput inputAct = TipoInput.Mouse;

    public Transform manoDer;
    public Transform manoIzq;

    public float maxAng = 90;
    public float desSencibilidad = 90;

    public bool habilitado = true;

    private Sentido _dirAct;

    private float _giro;
    //float Diferencia;

    //---------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        switch (inputAct)
        {
            case TipoInput.Mouse:
                if (habilitado)
                    gameObject.GetComponent<CarController>()
                        .SetGiro(MousePos.Relation(MousePos.AxisRelation.Horizontal));

                break;

            case TipoInput.Kinect:

                //print("Angulo: "+Angulo());
                /*
                if(ManoIzq.position.y > ManoDer.position.y)
                {
                    DirAct = Sentido.Der;
                    Diferencia = ManoIzq.position.y - ManoDer.position.y;
                }
                else
                {
                    DirAct = Sentido.Izq;
                    Diferencia = ManoDer.position.y - ManoIzq.position.y;
                }
                */

                if (manoIzq.position.y > manoDer.position.y)
                    _dirAct = Sentido.Der;
                else
                    _dirAct = Sentido.Izq;

                switch (_dirAct)
                {
                    case Sentido.Der:
                        if (Angulo() <= maxAng)
                            _giro = Angulo() / (maxAng + desSencibilidad);
                        else
                            _giro = 1;

                        if (habilitado)
                            gameObject.GetComponent<CarController>().SetGiro(_giro);

                        break;

                    case Sentido.Izq:
                        if (Angulo() <= maxAng)
                            _giro = Angulo() / (maxAng + desSencibilidad) * -1;
                        else
                            _giro = -1;

                        if (habilitado)
                            gameObject.GetComponent<CarController>().SetGiro(_giro);

                        break;
                }

                break;
            case TipoInput.Awsd:
                if (habilitado)
                {
                    if (Input.GetKey(KeyCode.A)) gameObject.GetComponent<CarController>().SetGiro(-1);
                    if (Input.GetKey(KeyCode.D)) gameObject.GetComponent<CarController>().SetGiro(1);
                }

                break;
            case TipoInput.Arrows:
                if (habilitado)
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) gameObject.GetComponent<CarController>().SetGiro(-1);
                    if (Input.GetKey(KeyCode.RightArrow)) gameObject.GetComponent<CarController>().SetGiro(1);
                }

                break;
        }
    }

    public float GetGiro()
    {
        /*
        switch(DirAct)
            {
            case Sentido.Der:
                if(Angulo() <= MaxAng)
                    return Angulo() / MaxAng;
                else
                    return 1;
                break;

            case Sentido.Izq:
                if(Angulo() <= MaxAng)
                    return (Angulo() / MaxAng) * (-1);
                else
                    return (-1);
                break;
            }
        */

        return _giro;
    }

    private float Angulo()
    {
        Vector2 diferencia = new Vector2(manoDer.localPosition.x, manoDer.localPosition.y)
                             - new Vector2(manoIzq.localPosition.x, manoIzq.localPosition.y);

        return Vector2.Angle(diferencia, new Vector2(1, 0));
    }
}