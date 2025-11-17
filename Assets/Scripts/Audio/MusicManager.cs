using AK.Wwise;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public State stageStart;
    public State stage1;
    public State stage2;
    public State stage3;
    public State stage4;
    public State stage5;
    public State stageCalm;
    public State stageCollapse;
    public State stageDefeat;
    public State stageWin;

    private void Start()
    {
        PlayStageStartMusic();

        EggInteraction.OnStartWaves += PlayStage1Music;
        WaveManager.OnWinningAllWaves += PlayStageCalmMusic;
        LevelController.OnWin += PlayWinMusic;
        LevelController.OnDefeat += PlayDefeatMusic;
        WaveManager.OnWaveEasy += PlayStage1Music;
        WaveManager.OnWaveMedium += PlayStage2Music;
        WaveManager.OnWaveHard += PlayStage3Music;
    }

    private void OnDisable()
    {
        EggInteraction.OnStartWaves -= PlayStage1Music;
        WaveManager.OnWinningAllWaves -= PlayStageCalmMusic;
        LevelController.OnWin -= PlayWinMusic;
        LevelController.OnDefeat -= PlayDefeatMusic;
        WaveManager.OnWaveEasy -= PlayStage1Music;
        WaveManager.OnWaveMedium -= PlayStage2Music;
        WaveManager.OnWaveHard -= PlayStage3Music;
    }

    private void PlayStage1Music()
    {
        stage1.SetValue();
    }

    private void PlayStage2Music()
    {
        stage2.SetValue();
    }

    private void PlayStage3Music()
    {
        stage3.SetValue();
    }

    private void PlayStage4Music()
    {
        stage4.SetValue();
    }

    private void PlayStage5Music()
    {
        stage5.SetValue();
    }

    private void PlayStageStartMusic()
    {
        stageStart.SetValue();
    }

    private void PlayStageCalmMusic()
    {
        stageCalm.SetValue();
    }

    private void PlayStageCollapseMusic()
    {
        stageCollapse.SetValue();
    }

    private void PlayDefeatMusic()
    {
        stageDefeat.SetValue();
    }

    private void PlayWinMusic()
    {
        stageWin.SetValue();
    }
}
