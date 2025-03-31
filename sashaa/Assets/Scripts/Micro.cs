using UnityEngine;

public static class MicrophoneManager
{
    private static AudioClip microphoneInput;
    private static string selectedMicrophone;
    private static bool isInitialized = false;

    public static void InitializeMicrophone()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone detected!");
            return;
        }

        selectedMicrophone = Microphone.devices[0];
        microphoneInput = Microphone.Start(selectedMicrophone, true, 1, 44100);
        isInitialized = true;
    }

    public static float GetVolume(float sensitivity)
    {
        if (!isInitialized) InitializeMicrophone();

        int sampleSize = 128;
        float[] samples = new float[sampleSize];
        int microphonePosition = Microphone.GetPosition(selectedMicrophone) - (sampleSize + 1);

        if (microphonePosition < 0) return 0;

        microphoneInput.GetData(samples, microphonePosition);

        float sum = 0;
        for (int i = 0; i < sampleSize; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        return (sum / sampleSize) * sensitivity;
    }

    public static void StopMicrophone()
    {
        if (isInitialized)
        {
            Microphone.End(selectedMicrophone);
            isInitialized = false;
        }
    }
}