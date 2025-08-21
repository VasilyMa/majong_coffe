using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float waitTime = 10f; // ������� ��� ������

    private Enums.DishType _wantedDish;
    private ServingWindow _window;
    private float _timer;
    private System.Action<Client> _onLeave;

    public void Init(Enums.DishType dish, ServingWindow window, System.Action<Client> onLeave)
    {
        _timer = waitTime;
        _wantedDish = dish;
        _window = window;
        _onLeave = onLeave;

    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            TakeDish();
        }
    }
    void TakeDish()
    {
        if (_window.TryTakeDish(_wantedDish, out Dish dish))
        {
            if (_wantedDish == dish.Type)
            {
                // ������ ������� ������ ��, ��� �����
                Leave(dish, success: true);
            }
            else
            {
                // ������ ������� ������ �����
                Leave(dish, success: false);
            }
        }
        else
        {
            // ������ �� ���������� ����� �����
            Leave(null, success: false);
        }
    }

    void Leave(Dish dish, bool success)
    {
        // TODO: �������� ����� + ������� �����
        if (!success)
        {
            if (dish == null)
                Debug.Log("������ ���� �����������! (�� �������� ����)");
            else
            {
                if (PlayerEntity.Instance.TryAddResourceValue(5))
                { 
                    Debug.Log($"������ ���� �����������! ����� {_wantedDish}, � ������� {dish.Type}");
                }
            }
        }
        else
        {
            if (PlayerEntity.Instance.TryAddResourceValue(10))
            {
                Debug.Log($"������ ���� ���������! ������� {_wantedDish}"); 
            }
        }

        _onLeave?.Invoke(this);
        Destroy(gameObject);
    }
}
