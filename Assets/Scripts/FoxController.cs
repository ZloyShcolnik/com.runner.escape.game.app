using UnityEngine;
using UnityEngine.UI;

public class FoxController : MonoBehaviour
{
    public bool isGrounded;

    private Vector3 m_down; // ��������� ����� ������
    private Vector3 m_up;   // �������� ����� ������

    private Plane swipePlane; // ���������, �� ������� ���������� �����

    private Rigidbody rb;

    private float[] positions = { -0.8f, 0f, 0.8f }; // ��������� ������� �� ��� X
    private int currentIndex = 1; // ������� ������� ��������� (������ � �������)

    private float moveSpeed = 10f; // �������� �������� �����������
    public float groundCheckDistance = 0.1f; // ����� ���� ��� �������� �����
    public LayerMask groundLayer; // ���� ����� ��� ��������

    private float score;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // ������ ��������� �� ������ z = 0
        swipePlane = new Plane(Vector3.forward, Vector3.one * transform.position.z);

        // ������������� ��������� ������� ���������
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

        // ������� �������� ��������� � �������� �������
        SmoothMove();

        // ��������, ��������� �� �������� �� �����
        CheckGrounded();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (swipePlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance); // ���������� ����� ����������� ���� � ����������
        }
        return Vector3.zero;
    }

    private void ProcessSwipe()
    {
        Vector3 direction = m_up - m_down;

        // ���� ����� ������� ��������, ����������
        if (direction.magnitude < 0.1f)
        {
            Debug.Log("Swipe too short!");
            return;
        }

        // ��������� ���������� ��������
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        if (absX > absY)
        {
            // �������������� �����
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
            // ������������ �����
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
        // ������� ������� �� X �� ������� positions
        float targetX = positions[currentIndex];
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // ������� �������� � ������� �������
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void CheckGrounded()
    {
        // ������� ��� ���� �� ������ �������
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);

        // ���������� ����� (����� � �����)
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}