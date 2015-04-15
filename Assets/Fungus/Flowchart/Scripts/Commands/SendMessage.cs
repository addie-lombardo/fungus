using UnityEngine;
using System.Collections;

namespace Fungus
{
	[CommandInfo("Flow", 
	             "Send Message", 
	             "Sends a message to either the owner Flowchart or all Flowcharts in the scene. Blocks can listen for this message to start execution.")]
	[AddComponentMenu("")]
	public class SendMessage : Command
	{
		public enum MessageTarget
		{
			SameFlowchart,
			AllFlowcharts
		}

		public MessageTarget messageTarget;
		public string message = "";

		public override void OnEnter()
		{
			if (message.Length == 0)
			{
				Continue();
			}

			Flowchart flowchart = GetFlowchart();

			MessageReceived[] receivers = null;
			if (messageTarget == MessageTarget.SameFlowchart)
			{
				receivers = flowchart.GetComponentsInChildren<MessageReceived>();
			}
			else
			{
				receivers = GameObject.FindObjectsOfType<MessageReceived>();
			}

			if (receivers != null)
			{
				foreach (MessageReceived receiver in receivers)
				{
					receiver.OnSendFungusMessage(message);
				}
			}

			Continue();
		}

		public override string GetSummary()
		{
			if (message.Length == 0)
			{
				return "Error: No message specified";
			}
			
			return message;
		}
		
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, 255);
		}
	}

}