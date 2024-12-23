using UnityEngine;
using UnityEngine.UI;

public class FoxController : MonoBehaviour
{
    public bool isGrounded;

    private Vector3 m_down; // Начальная точка свайпа
    private Vector3 m_up;   // Конечная точка свайпа

    private Plane swipePlane; // Плоскость, на которой происходит свайп

    private Rigidbody rb;

    private float[] positions = { -0.8f, 0f, 0.8f }; // Доступные позиции по оси X
    private int currentIndex = 1; // Текущая позиция персонажа (индекс в массиве)

    private float moveSpeed = 10f; // Скорость плавного перемещения
    public float groundCheckDistance = 0.1f; // Длина луча для проверки земли
    public LayerMask groundLayer; // Слой земли для проверки

    private float score;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Создаём плоскость на уровне z = 0
        swipePlane = new Plane(Vector3.forward, Vector3.one * transform.position.z);

        // Устанавливаем начальную позицию персонажа
        transform.position = new Vector3(positions[currentIndex], transform.position.y, transform.position.z);
    }

    private void Update()
    {
        score += Time.deltaTime * 10.0f;
        scoreText.text = $"{Mathf.RoundToInt(score)} m.";

        if (Input.GetMouseButtonDown(0))
        {
            m_down = GetMouseWorldPosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_up = GetMouseWorldPosition();
            ProcessSwipe();
        }

        // Плавное движение персонажа к текущему индексу
        SmoothMove();

        // Проверка, находится ли персонаж на земле
        CheckGrounded();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (swipePlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance); // Возвращаем точку пересечения луча с плоскостью
        }
        return Vector3.zero;
    }

    private void ProcessSwipe()
    {
        Vector3 direction = m_up - m_down;

        // Если свайп слишком короткий, игнорируем
        if (direction.magnitude < 0.1f)
        {
            Debug.Log("Swipe too short!");
            return;
        }

        // Вычисляем абсолютные значения
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        if (absX > absY)
        {
            // Горизонтальный свайп
            if (direction.x > 0)
            {
                Debug.Log("Swipe Right");
                MoveRight();
            }
            else
            {
                Debug.Log("Swipe Left");
                MoveLeft();
            }
        }
        else
        {
            // Вертикальный свайп
            if (direction.y > 0)
            {
                Debug.Log("Swipe Up");
                Jump();
            }
            else
            {
                Debug.Log("Swipe Down");
            }
        }
    }

    private void MoveRight()
    {
        if (currentIndex < positions.Length - 1)
        {
            currentIndex++;
            Debug.Log("Moved Right");
        }
        else
        {
            Debug.Log("Already at the rightmost position");
        }
    }

    private void MoveLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Debug.Log("Moved Left");
        }
        else
        {
            Debug.Log("Already at the leftmost position");
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            JumpSfx.Instant();
            rb.AddForce(Vector3.up * 15, ForceMode.Impulse);
        }
    }

    private void SmoothMove()
    {
        // Целевая позиция по X из массива positions
        float targetX = positions[currentIndex];
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Плавное движение к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void CheckGrounded()
    {
        // Пускаем луч вниз от центра объекта
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);

        // Отладочная линия (видна в сцене)
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}