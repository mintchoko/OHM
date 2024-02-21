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
    //오디오 소스 (스피커)를 담을 배열 선언
    AudioSource[] audioSources = new AudioSource[(int)Sound.Count]; 

    //오디오 클립 (음원) 캐싱
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
                audioSources[i] = go.AddComponent<AudioSource>(); // 위에서 만든 audioSources에 넣어준다.
                go.transform.parent = root.transform;
                UpdateVolume((Sound)i); //초기화 시, 가져온 세팅값을 스피커의 볼륨으로 사용하도록 지정.
            }
            // soundName을 돌면서 새로운 GameObject를 만들어준다.

            audioSources[(int)Sound.Bgm].loop = true; // Bgm같은 경우에는 루프로 계속 사운드가 나도록 해준다.
        }
    }

    //사운드들 데이터 청소
    public void Clear()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        audioClips.Clear();
    }

    public void UpdateVolume(Sound type, int delta = 0) //볼륨을 세팅 데이터에서 가져와 적용.
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


    //사운드 재생
    public void Play(string path, Sound type = Sound.Effect, float pitch = 1.0f) // path로 경로를 받아주고 pitch = 소리 속도 조절
    {
        AudioClip audioclip = GetOrAddAudioClip(path, type);
        Play(audioclip, type, pitch);
    }

    //사운드 재생
    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f) // path로 경로를 받아주고 pitch = 소리 속도 조절
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
            audioSource.Play(); //loop 설정에 따라 반복 플레이
        }

        else // (type == Define.Sound.Effect)
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];

            audioSource.volume = SettingData.Instance.Setting.effect / 100f;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            if(!audioSource.isPlaying) audioSource.PlayOneShot(audioClip); //한번 플레이
        }
    }

    AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect) // audioClip 반환하는 함수(위에 Dictionary만든 부분에서)
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
            if (audioClips.TryGetValue(path, out audioClip) == false) // 있으면 이렇게 값을 뱉어주고 
            {
                audioClip = AssetLoader.Instance.Load<AudioClip>(path);
                audioClips.Add(path, audioClip); //효과음은 자주 나오므로 캐싱
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }

        return audioClip;
    }

}

