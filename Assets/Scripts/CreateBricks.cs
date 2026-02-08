using UnityEngine;

public class CreateBricks : MonoBehaviour
{
    public GameObject brick;
    private int _counter;

    void Awake()
    {
        GameObject[] bricks = new GameObject[10];
        float startPosX = -7.75f;

        int rows = 6;
        int columns = 15;

        float x = startPosX;
        float y = 4.0f;

        SetColor(Color.softRed);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Instantiate(brick, new Vector3(x, y, 0), new Quaternion().normalized);
                x += 1.10f;

                _counter++;

                if (_counter % 2 == 0) SetColor(Color.softRed);
                if (_counter % 4 == 0) SetColor(Color.softBlue);
                if (_counter % 6 == 0) SetColor(Color.softGreen);
            }

            x = startPosX;
            y -= 0.35f;
        }
    }

    private void SetColor(Color color)
    {
        brick.GetComponent<SpriteRenderer>().color = color;
    }

    public int GetCounter()
    {
        return _counter;
    }
}
