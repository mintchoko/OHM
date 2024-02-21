using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Sound
{
    Bgm,
    Effect,
    Count
}

//음원, 효과음 재생/정지를 담당
public class SoundManager : Singleton<SoundManager>
{
    //����� �ҽ� (����Ŀ)�� ���� �迭 ����
    AudioSource[] audioSources = new AudioSource[(int)Sound.Count];

    //����� Ŭ�� (����) ĳ��
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        GameObject root = GameObject.Find("SoundRoot");
        if (root == null)
        {
            root = new GameObject { name = "SoundRoot" };
            DontDestroyOnLoad(root);

            string[] soundNames = { "Bgm", "Effect" };

            for (int i = 0; i < soundNames.Length; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>(); // ������ ���� audioSources�� �־��ش�.
                go.transform.parent = root.transform;
                UpdateVolume((Sound)i); //�ʱ�ȭ ��, ������ ���ð��� ����Ŀ�� �������� ����ϵ��� ����.
            }
            // soundName�� ���鼭 ���ο� GameObject�� ������ش�.

            audioSources[(int)Sound.Bgm].loop = true; // Bgm���� ��쿡�� ������ ��� ���尡 ������ ���ش�.
        }
    }

    //����� ������ û��
    public void Clear()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        audioClips.Clear();
    }

    public void UpdateVolume(Sound type, int delta = 0) //������ ���� �����Ϳ��� ������ ����.
    {
        if (type == Sound.Bgm)
        {
            AudioSource audioSource = audioSources[(int)Sound.Bgm];
            SettingData.Instance.Setting.bgm += delta;
            audioSource.volume = (SettingData.Instance.Setting.bgm / 100f);
        }

        else // (type == Define.Sound.Effect)
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];
            SettingData.Instance.Setting.effect += delta;
            audioSource.volume = (SettingData.Instance.Setting.effect / 100f);
        }
    }


    //���� ���
    public void Play(string path, Sound type = Sound.Effect, float pitch = 1.0f) // path�� ��θ� �޾��ְ� pitch = �Ҹ� �ӵ� ����
    {
        AudioClip audioclip = GetOrAddAudioClip(path, type);
        Play(audioclip, type, pitch);
    }

    //���� ���
    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f) // path�� ��θ� �޾��ְ� pitch = �Ҹ� �ӵ� ����
    {
        if (audioClip == null)
        {
            return;
        }

        if (type == Sound.Bgm)
        {
            AudioSource audioSource = audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.volume = SettingData.Instance.Setting.bgm / 100f;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play(); //loop ������ ���� �ݺ� �÷���
        }

        else // (type == Define.Sound.Effect)
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];

            audioSource.volume = SettingData.Instance.Setting.effect / 100f;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            if (!audioSource.isPlaying) audioSource.PlayOneShot(audioClip); //�ѹ� �÷���
        }
    }

    AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect) // audioClip ��ȯ�ϴ� �Լ�(���� Dictionary���� �κп���)
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }

        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = AssetLoader.Instance.Load<AudioClip>(path);
        }
        else
        {
            if (audioClips.TryGetValue(path, out audioClip) == false) // ������ �̷��� ���� ����ְ� 
            {
                audioClip = AssetLoader.Instance.Load<AudioClip>(path);
                audioClips.Add(path, audioClip); //ȿ������ ���� �����Ƿ� ĳ��
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }

        return audioClip;
    }

}

