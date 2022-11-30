using DG.Tweening;
using UnityEngine;

namespace ClimbingCat.Level
{
	public class ClimberView : MonoBehaviour
	{
		[SerializeField] private MeshRenderer climberMaterial1;
		[SerializeField] private MeshRenderer climberMaterial2;
		[SerializeField] private AnimationCurve customEase;

		// TODO : quick and dirty -> improve
		public void WrongAnswerAnimation()
		{
			var position = transform.position;
			var rotation = transform.rotation;
			var scale = transform.localScale;

			Color c1, c2;
			var material1Color = c1 = climberMaterial1.material.color;
			var material2Color = c2 = climberMaterial2.material.color;
			c1.a = 0;
			c2.a = 0;
			var duration = 0.3f;


			climberMaterial1.material.DOColor(c1, duration);
			climberMaterial2.material.DOColor(c2, duration);
			transform.DOLocalMoveX(position.x + 3, duration).SetEase(Ease.OutExpo);
			transform.DORotate(new Vector3(0, 0, -180), duration / 2).SetEase(Ease.OutSine);
			transform.DOScale(0.3f, duration).SetEase(Ease.OutSine);
			transform.DOMoveY(position.y - 5, duration).SetEase(customEase).OnComplete(() =>
			{
				SetVisible(false);
				climberMaterial1.material.color = material1Color;
				climberMaterial2.material.color = material2Color;
				transform.position = position;
				transform.rotation = rotation;
				transform.localScale = scale;
				var sequence = DOTween.Sequence();
				sequence.AppendCallback(() => SetVisible(false));
				sequence.AppendInterval(0.2f);
				sequence.AppendCallback(() => SetVisible(true));
				sequence.AppendInterval(0.2f);
				sequence.SetLoops(3);
				sequence.Play();
			});
		}

		private void SetVisible(bool value)
		{
			climberMaterial1.enabled = value;
			climberMaterial2.enabled = value;
		}
	}
}