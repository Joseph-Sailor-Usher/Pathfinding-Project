using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyphonicSinewave : MonoBehaviour
{
    [Range(1, 20000)]
    public float frequency1;

    [Range(1, 20000)]
    public float frequency2;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;

    private AudioSource[] audioSources;
    private int timeIndex = 0;

    void Start()
    {
        audioSources = new AudioSource[2];
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = false;
            audioSources[i].spatialBlend = 0; // Force 2D sound
            audioSources[i].Stop(); // Avoid auto play
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayTone(frequency1, 0);
            PlayTone(frequency2, 1);
        }
    }

    public void PlayTone(float frequency, int audioSourceIndex)
    {
        if (audioSources[audioSourceIndex].isPlaying)
        {
            audioSources[audioSourceIndex].Stop();
            audioSources[audioSourceIndex].time = 0;
        }

        timeIndex = 0;
        audioSources[audioSourceIndex].clip = CreateSineWaveClip(frequency);
        audioSources[audioSourceIndex].Play();
    }

    AudioClip CreateSineWaveClip(float frequency)
    {
        int numSamples = (int)(sampleRate * waveLengthInSeconds);
        float[] samples = new float[numSamples];

        for (int i = 0; i < numSamples; i++)
        {
            samples[i] = CreateSine(timeIndex, frequency, sampleRate);
            timeIndex++;

            if (timeIndex >= numSamples)
            {
                timeIndex = 0;
            }
        }

        AudioClip clip = AudioClip.Create("SineWave", numSamples, 1, (int)sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }

    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }
}

