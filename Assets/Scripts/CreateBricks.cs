using UnityEngine;

public class CreateBricks : MonoBehaviour
{
    public GameObject brick;

    void Awake()
    {
        GameObject[] bricks = new GameObject[10];

        int rows = 3;
        int columns = 10;

        float x = -6.5f;
        float y = 4.0f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Instantiate(brick, new Vector3(x, y, 0), new Quaternion().normalized);
                x += 1.46f;
            }

            x = -6.5f;
            y -= 0.75f;
        }
    }
}
