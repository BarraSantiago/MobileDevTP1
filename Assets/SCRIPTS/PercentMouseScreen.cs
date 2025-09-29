using UnityEngine;

public class MousePos : MonoBehaviour
{
    public enum AxisRelation
    {
        Horizontal,
        Vertical
    }

    public static float RelCalibration()
    {
        return 0.5f; //devuelve el centro de la pantalla, el mouse siempre deberia arrancar en el medio
    }

    public static float Relation(AxisRelation axisR)
    {
        float res;
        switch (axisR)
        {
            case AxisRelation.Horizontal:
                res = Input.mousePosition.x / Screen.width * 2 - 1;
                return res;
                break;


            case AxisRelation.Vertical:
                res = Input.mousePosition.y / Screen.height * 2 - 1;
                return res;
                break;
        }

        return -1;
    }
}