using UnityEngine;

public class CreateBricks : MonoBehaviour
{
    public GameObject brick;

    void Awake()
    {
        GameObject[] bricks = new GameObject[10];
        int counter = 0;

        int rows = 3;
        int columns = 10;

        float x = -6.5f;
        float y = 4.0f;

        SetColor(Color.softRed);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Instantiate(brick, new Vector3(x, y, 0), new Quaternion().normalized);
                x += 1.46f;

                counter++;

                if (counter < 10) SetColor(Color.softRed);
                if (counter >= 10) SetColor(Color.softBlue);
                if (counter >= 20) SetColor(Color.softGreen);
            }

            x = -6.5f;
            y -= 0.75f;
        }
    }

    private void SetColor(Color color)
    {
        brick.GetComponent<SpriteRenderer>().color = color;
    }
}
