using UnityEngine;

public class CollContraObst : MonoBehaviour
{
    public float tiempEsp = 1;
    public float tiempNoColl = 2;

    private Colisiones _colisiono = Colisiones.ConTodo;
    private float _tempo1;
    private float _tempo2;

    // Use this for initialization
    private void Start()
    {
        Physics.IgnoreLayerCollision(8, 10, false);
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_colisiono)
        {
            case Colisiones.ConTodo:
                break;

            case Colisiones.EspDesact:
                _tempo1 += T.GetDT();
                if (_tempo1 >= tiempEsp)
                {
                    _tempo1 = 0;
                    IgnorarColls(true);
                }

                break;

            case Colisiones.SinObst:
                _tempo2 += T.GetDT();
                if (_tempo2 >= tiempNoColl)
                {
                    _tempo2 = 0;
                    IgnorarColls(false);
                }

                break;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Obstaculo") ColisionConObst();
    }

    //-------------------------//

    private void ColisionConObst()
    {
        switch (_colisiono)
        {
            case Colisiones.ConTodo:
                _colisiono = Colisiones.EspDesact;
                break;

            case Colisiones.EspDesact:
                break;

            case Colisiones.SinObst:
                break;
        }
    }

    private void IgnorarColls(bool b)
    {
        print("IgnorarColls() / b = " + b);

        if (name == "Camion1")
            Physics.IgnoreLayerCollision(8, 10, b);
        else
            Physics.IgnoreLayerCollision(9, 10, b);

        if (b)
            _colisiono = Colisiones.SinObst;
        else
            _colisiono = Colisiones.ConTodo;
    }

    private enum Colisiones
    {
        ConTodo,
        EspDesact,
        SinObst
    }
}