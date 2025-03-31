using UnityEngine;

public class SoundActivatedPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float volumeThreshold = 0.05f;
    public float sensitivity = 2.0f;
    public float moveSpeed = 3f;
    public float maxOffset = 3f; // Насколько далеко двигается (вверх или вниз)

    private Vector3 initialPosition;
    private bool isMoving = false;

    void Start()
    {
        initialPosition = transform.position;
        MicrophoneManager.InitializeMicrophone(); // Инициализация микрофона один раз
    }

    void Update()
    {
        float currentVolume = MicrophoneManager.GetVolume(sensitivity);
        bool shouldMove = currentVolume > volumeThreshold;

        // Движение вниз (можно изменить на Vector3.up для подъема)
        Vector3 targetDirection = shouldMove ? Vector3.down : Vector3.up;
        float targetY = initialPosition.y + (shouldMove ? -maxOffset : 0);

        // Плавное перемещение
        float newY = Mathf.MoveTowards(
            transform.position.y,
            targetY,
            moveSpeed * Time.deltaTime
        );

        transform.position = new Vector3(
            initialPosition.x,
            newY,
            initialPosition.z
        );
    }

    void OnDestroy()
    {
        // Если это последняя платформа, можно остановить микрофон
        // MicrophoneManager.StopMicrophone(); // (опционально)
    }
}