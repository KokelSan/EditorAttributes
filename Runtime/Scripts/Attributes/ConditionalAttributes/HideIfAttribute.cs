using UnityEngine;

namespace EditorAttributes
{
	public class HideIfAttribute : PropertyAttribute, IConditionalAttribute
    {
        public string ConditionName { get; private set; }
		public int EnumValue { get; private set; }

		/// <summary>
		/// Attribute to hide fields based on a condition
		/// </summary>
		/// <param name="conditionName">The name of the condition to evaluate</param>
		public HideIfAttribute(string conditionName)
#if UNITY_2023_3_OR_NEWER
        : base(true) 
#endif
			=> ConditionName = conditionName;

		/// <summary>
		/// Attribute to hide fields based on a condition
		/// </summary>
		/// <param name="conditionName">The name of the condition to evaluate</param>
		/// <param name="enumValue">The value of the enum</param>
		public HideIfAttribute(string conditionName, object enumValue)
#if UNITY_2023_3_OR_NEWER
        : base(true) 
#endif
		{
			ConditionName = conditionName;
			EnumValue = (int)enumValue;
		}
	}
}
