using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    public class Spiral_PivotPointController : MonoBehaviour
    {

        public Transform pivotPoint_1;
        public Transform pivotPoint_2;


        public void UI_PivotPoint_1_Position_X(float value)
        {
            Vector3 newPosition = pivotPoint_1.position;
            newPosition.x = value;

            pivotPoint_1.position = newPosition;
        }

        public void UI_PivotPoint_1_Position_Y(float value)
        {
            Vector3 newPosition = pivotPoint_1.position;
            newPosition.y = value;

            pivotPoint_1.position = newPosition;
        }

        public void UI_PivotPoint_1_Position_Z(float value)
        {
            Vector3 newPosition = pivotPoint_1.position;
            newPosition.z = value;

            pivotPoint_1.position = newPosition;
        }

        public void UI_PivotPoint_1_Position_Angle(float value)
        {
            CurvedWorld_Controller.current.SetAngle(value);
        }



        public void UI_PivotPoint_2_Position_X(float value)
        {
            Vector3 newPosition = pivotPoint_2.position;
            newPosition.x = value;

            pivotPoint_2.position = newPosition;
        }

        public void UI_PivotPoint_2_Position_Y(float value)
        {
            Vector3 newPosition = pivotPoint_2.position;
            newPosition.y = value;

            pivotPoint_2.position = newPosition;
        }

        public void UI_PivotPoint_2_Position_Z(float value)
        {
            Vector3 newPosition = pivotPoint_2.position;
            newPosition.z = value;

            pivotPoint_2.position = newPosition;
        }

        public void UI_PivotPoint_2_Position_Angle(float value)
        {
            CurvedWorld_Controller.current.SetAngle2(value);
        }
    }
}
