using Godot;
using System;
using System.Collections.Generic;

public partial class Jumpscare : Sprite3D
{	
	[Export]
	public string ScarePhrase;

	[Export]
	public int PhraseSize, PhraseCount;

	[Export]
	public float PhraseSpawnSpeed;

	[Export]
	public float PhraseLifeTime, PhraseFlashSpeed;

	[Export]
	public Color PhraseColor1, PhraseColor2;

	[Export]
	public Vector2 SpawnRegionMin = new Vector2(-4f, 4.7f), SpawnRegionMax = new Vector2(1f, 10.8f);

	GameManager gm;
	AnimationPlayer anim;
	double t;
	List<LabelData> labels = new List<LabelData>();
	Queue<LabelData> cleanUpQueue = new Queue<LabelData>();
	RandomNumberGenerator rand;
	int phraseCount;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		gm.InvokeScare();
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("Scare");
		rand = new RandomNumberGenerator();
		rand.Randomize();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		//Phrase Update
		for (int i = 0; i < labels.Count; i++) {
			if (labels[i].Time >= PhraseLifeTime) {
				cleanUpQueue.Enqueue(labels[i]);
			}

			if (labels[i].ColorTime >= PhraseFlashSpeed) {
				labels[i].Text.Modulate = labels[i].lastColor ? PhraseColor1 : PhraseColor2;
				labels[i].lastColor = !labels[i].lastColor;
				labels[i].ColorTime = 0d;
			}

			labels[i].Time += delta;
			labels[i].ColorTime += delta;
		}

		//Freeing old phrases
		while(cleanUpQueue.Count > 0) {
			LabelData dta = cleanUpQueue.Dequeue();
			labels.Remove(dta);
			dta.Text.QueueFree();
		}

		if (phraseCount >= PhraseCount) {
			if (labels.Count == 0 && cleanUpQueue.Count == 0) {
				QueueFree();
			}
			return;
		}

		//Timer
		if (t < PhraseSpawnSpeed) {
			t += delta;
			return;
		}

		GD.Print(phraseCount);

		//new phrase creation
		Label3D label = new Label3D() {
			Text = ScarePhrase,
			Position = new Vector3(
				Mathf.Lerp(-4f, 1f, rand.Randf()),
				Mathf.Lerp(4.7f, 10.8f, rand.Randf()),
				9f
			),
			PixelSize = 1f / PhraseSize,
		};
		label.Modulate = PhraseColor1;
		label.RotationDegrees = Vector3.Left * 21f;

		GetParent().GetParent().GetParent().AddChild(label);

		labels.Add(new LabelData(){
			Text = label,
			Time = 0d,
			ColorTime = 0d,
		});

		t = 0d;
		phraseCount++;
	}

	class LabelData {
		public Label3D Text;
		public double Time;
		public double ColorTime;
		public bool lastColor;
	}
}
