using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public AudioClip mainmenuMusic, gameplayMusic, clickSound, clickAtMainMenu, soundDead1, soundDead2, soundDead3, soundThrow1, soundThrow2, soundThrow3, soundWin1, soundWin2, soundLose,
    soundRoar, soundShuriken1, soundShuriken2, soundShuriken3, soundIceBreak, soundRollingStone, soundHostageRescued, soundHostageRescuedEnd, soundHitMetal, soundLevelStart1, soundLevelStart2, soundBombBox, soundExplosion, soundSelectNinja,
    soundDynamiteBounce, soundCannonShoot, soundWoodenBoxBreak, soundStar;
	AudioSource musicAudioSource,soundAudioSource;
	public static AudioManager instance = null;

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
			musicAudioSource = GetComponents<AudioSource> ()[0];
			soundAudioSource = GetComponents<AudioSource> ()[1];
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start(){

	}


	public void playMainMenuMusic(){
		if (GameConstant.isMusicOn () != 1)
			return;
		pauseMusic ();
	
		musicAudioSource.clip = mainmenuMusic;
		musicAudioSource.Play ();
	}
	public void pauseMusic(){
		
		musicAudioSource.Pause ();
	}
	public void resumeMusic(){
		musicAudioSource.Play ();
	}
	public void playClickSound(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;
		soundAudioSource.PlayOneShot (clickSound);
	}
	public void playClickAtMainMenuSound(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;
		soundAudioSource.PlayOneShot (clickAtMainMenu);
	}
	public void playSound(AudioClip sound){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;
		soundAudioSource.PlayOneShot (sound);
	}


	public  void stopMusic(){
		musicAudioSource.Stop ();
	}
	public void playGamePlayMusic(){
		if (GameConstant.isMusicOn () != 1)
			return;
		stopMusic ();

		musicAudioSource.clip = gameplayMusic;
		musicAudioSource.Play ();
	}
	public void playThrowSound(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;

		int ran = Random.Range(0,2);
		switch (ran){
		case 0:
			soundAudioSource.PlayOneShot (soundThrow1);
			break;
		case 1:
			soundAudioSource.PlayOneShot (soundThrow2);
			break;
		case 2:
			soundAudioSource.PlayOneShot (soundThrow3);
			break;

		}
	}
	public void playDeadSound(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;

		int ran = Random.Range(0,2);
		switch (ran){
		case 0:
			soundAudioSource.PlayOneShot (soundDead1);
			break;
		case 1:
			soundAudioSource.PlayOneShot (soundDead2);
			break;
		case 2:
			soundAudioSource.PlayOneShot (soundDead3);
			break;

		}
	}
	public void playShurikenBounceSound(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;

		int ran = Random.Range(0,2);
		switch (ran){
		case 0:
			soundAudioSource.PlayOneShot (soundShuriken1);
			break;
		case 1:
			soundAudioSource.PlayOneShot (soundShuriken2);
			break;
		case 2:
			soundAudioSource.PlayOneShot (soundShuriken3);
			break;

		}
	}
	public void playSoundWin(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;
		int ran = Random.Range(0,1);
		switch (ran){
		case 0:
			soundAudioSource.PlayOneShot (soundWin1);
			break;
		case 1:
			soundAudioSource.PlayOneShot (soundWin2);
			break;
	

		}
	}
	public void playLevelStartSound(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;
		int ran = Random.Range(0,1);
		switch (ran){
		case 0:
			soundAudioSource.PlayOneShot (soundLevelStart1);
			break;
		case 1:
			soundAudioSource.PlayOneShot (soundLevelStart2);
			break;


		}
	}

	public void playsoundLose(){
		if (GameConstant.isSoundOn () != 1)
			return;
		soundAudioSource.loop = false;
		soundAudioSource.PlayOneShot (soundLose);
	}
}
