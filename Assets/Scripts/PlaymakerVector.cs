﻿using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Store a Vector2 XY components into a Vector3 XY component. The Vector3 z component is also accessible for convenience")]
	public class Vector2toVector3 : FsmStateAction
	{
		
		[UIHint(UIHint.Variable)]
		[Tooltip("the vector2")]
		public FsmVector2 vector2;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("the vector3")]
		public FsmVector3 vector3;
		
		
		[Tooltip("The vector3 z value")]
		public FsmFloat zValue;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			vector2 = null;
			vector3 = null;
			everyFrame = false;
			
		}
		
		public override void OnEnter()
		{
			
			vector3.Value = new Vector3(vector2.Value.x,vector2.Value.y,zValue.Value);
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			vector3.Value = new Vector3(vector2.Value.x,vector2.Value.y,zValue.Value);
		}
		
	}

	[Tooltip("Store a Vector2 XY components into a Vector3 XY component. The Vector3 z component is also accessible for convenience")]
	public class Vector3ToVector2 : FsmStateAction
	{
		
		[UIHint(UIHint.Variable)]
		[Tooltip("the vector3")]
		public FsmVector3 vector3;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("the vector2")]
		public FsmVector2 vector2;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			vector2 = null;
			vector3 = null;
			everyFrame = false;
			
		}
		
		public override void OnEnter()
		{
			
			// vector3.Value = new Vector3(vector2.Value.x,vector2.Value.y,zValue.Value);
			vector2.Value = new Vector2 (vector3.Value.x, vector3.Value.y);
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			vector2.Value = new Vector2 (vector3.Value.x, vector3.Value.y);
		}
		
	}

}