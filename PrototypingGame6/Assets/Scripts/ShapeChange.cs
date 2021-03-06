using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChange : DrawingButtons
{
    public Shape shape;
    [SerializeField]
    private Paint paint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                ShapeChange shapeChange = hit.collider.GetComponent<ShapeChange>();
                if (shapeChange != null && shapeChange.shape == shape)
                {
                    Paint.brushShape = shape;
                    Paint.isEraser = false;
                    Paint.action = Actions.Painting;
                    buttonGraying.GrayButtons(type, id);
                    paint.ChangeHintShape(shape);
                }
            }
        }
    }
}
