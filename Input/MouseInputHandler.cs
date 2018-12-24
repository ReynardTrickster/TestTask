using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Cell")
            {
                CellClickHandler handler = hit.transform.GetComponent<CellClickHandler>();
                if (Input.GetMouseButtonDown(0))
                    handler.LeftClick();
                else
                    handler.RightClick();
            }
        }
        if (Input.GetMouseButton(2))
        {
            Camera.main.GetComponent<CameraController>().Rotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            Camera.main.GetComponent<CameraController>().Scroll(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}
